using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MultiTop_AlvaroLaveriano
{
    public class Service
    {
        Connection sql = new Connection();

        public T RegistrarEmpresa<T>(Datos eDatos) where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "num_ruc", eDatos.num_ruc},
                { "dsc_raz_social", eDatos.dsc_raz_social},
                { "flg_estado", eDatos.flg_estado }
            };

            obj = sql.ConsultarEntidad<T>("SP_RegistrarEmpresa", oDictionary);
            return obj;
        }

        public T RegistrarDireccionEmpresa<T>(Datos eDatos) where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eDatos.cod_empresa},
                { "dsc_direccion", eDatos.dsc_direccion}
            };

            obj = sql.ConsultarEntidad<T>("SP_RegistrarDireccionEmpresa", oDictionary);
            return obj;
        }


        public DataTable ListarDireccionesxEmpresa(int cod_empresa)
        {
            DataTable tabla = new DataTable();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", cod_empresa}
            };

            tabla = sql.ListaDatatable("SP_ListarDireccionesxEmpresa", oDictionary);
            return tabla;
        }
        public string Eliminar_Empresa_Direcciones(string tabla, int cod_empresa = 0, int cod_direccion = 0)
        {
            string respuesta = "";
            string procedure = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", cod_empresa}
            };

            switch (tabla)
            {
                case "Empresa":
                    procedure = "SP_EliminarEmpresa";
                    break;
                case "DireccionesEmpresa":
                    oDictionary.Add("cod_direccion", cod_direccion);
                    procedure = "SP_EliminarDireccionEmpresa";
                    break;
            }

            respuesta = sql.ExecuteSPRetornoValor(procedure, oDictionary);
            return respuesta;
        }
    }
}
