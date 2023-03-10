using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MultiTop_AlvaroLaveriano
{
    public class Connection
    {
        SqlConnection dbConn = new SqlConnection();

        public void GetConnection()
        {
            string Servidor = ".";
            string BBDD = "MULTITOP";
            string UserID = "sa";
            string Password = "sql2023";

            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            builder.DataSource = Servidor;
            builder.InitialCatalog = BBDD;
            builder.UserID = UserID;
            builder.Password = Password;
            builder.IntegratedSecurity = false;

            ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings("dbConn", builder.ConnectionString);
            dbConn = new SqlConnection(connectionStringSettings.ConnectionString);
            try
            {
                dbConn.Open();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T ConsultarEntidad<T>(string procedure, Dictionary<string, object> dictionary) where T : class, new()
        {
            if (dbConn.State == ConnectionState.Closed) { GetConnection(); }
            T entidad = new T();
            DataTable table = ObtenerTabla(procedure, dictionary);
            try
            {
                DataRow row = table.Rows[0];
                entidad = ToObject<T>(row);

                return entidad;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
            finally
            {
                dbConn.Close();
            }
        }

        public DataTable ListaDatatable(string procedure, Dictionary<string, object> dictionary)
        {
            if (dbConn.State == ConnectionState.Closed) { GetConnection(); };
            DataTable table = ObtenerTabla(procedure, dictionary);
            try
            {
                return table;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                dbConn.Close();
            }
        }

        public string ExecuteSPRetornoValor(string procedure, Dictionary<string, object> dictionary)
        {
            try
            {
                if (dbConn.State == ConnectionState.Closed) { GetConnection(); }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = dbConn;
                agregarParametros(cmd, dictionary);

                SqlParameter pvNewId = new SqlParameter("@ValorDeSalida", SqlDbType.VarChar, 100);
                pvNewId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvNewId);
                var val = cmd.ExecuteNonQuery();

                if (val >= 1)
                {
                    return "OK" + cmd.Parameters["@ValorDeSalida"].Value.ToString();
                }
                else
                {
                    return "ERROR";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                throw;
            }
            finally
            {
                dbConn.Close();
            }
        }

        public DataTable ObtenerTabla(string procedure, Dictionary<string, object> dictionary)
        {
            try
            {
                DataTable tabla = new DataTable();
                SqlDataAdapter reader = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(procedure, dbConn);

                cmd.CommandType = CommandType.StoredProcedure;

                agregarParametros(cmd, dictionary);

                reader.SelectCommand = cmd;
                reader.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                return new DataTable();
                throw;
            }
            finally
            {
                dbConn.Close();
            }
        }

        private void agregarParametros(SqlCommand cmd, Dictionary<string, object> dictionary)
        {
            foreach (KeyValuePair<string, object> k in dictionary)
            {
                cmd.Parameters.AddWithValue(k.Key, k.Value);
            }
        }

        public const string sNullable = "Nullable`1";
        public static T ToObject<T>(DataRow row) where T : class, new()
        {
            T obj = new T();

            foreach (DataColumn col in row.Table.Columns)
            {
                PropertyInfo prop = obj.GetType().GetProperty(col.ColumnName);
                if (prop != null)
                {
                    string propName = prop.PropertyType.Name;
                    if (propName == sNullable)
                    {
                        propName = Nullable.GetUnderlyingType(prop.PropertyType).Name;
                    }

                    if (prop.CanWrite & !object.ReferenceEquals(row[col], DBNull.Value) & col.DataType.Name == propName)
                    {
                        prop.SetValue(obj, row[col], null);
                    }
                }
            }

            return obj;
        }
    }
}
