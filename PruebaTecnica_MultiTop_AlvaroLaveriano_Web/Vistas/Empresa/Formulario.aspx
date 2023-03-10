<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="PruebaTecnica_MultiTop_AlvaroLaveriano_Web.Vistas.Empresa.Listado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblCodigo" runat="server" Text="Código"></asp:Label>
            <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblRUC" runat="server" Text="RUC"></asp:Label>
            <asp:TextBox ID="txtRuc" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblRazSocial" runat="server" Text="Razón Social"></asp:Label>
            <asp:TextBox ID="txtRazSocial" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:RadioButton ID="rbActivo" runat="server" Text="Activo"/>
            <asp:RadioButton ID="rbInactivo" runat="server" Text="Inactivo" />
        </div>
        <div>
            <asp:Label ID="lblDireccion" runat="server" Text="Dirección"></asp:Label>
            <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnAgregarDir" runat="server" Text="Button" OnClick="btnAgregarDir_Click" />
        </div>
        <div>
            <asp:GridView ID="gvDirecciones" runat="server">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="#" SortExpression="Codigo"></asp:BoundField>
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" SortExpression="Codigo"></asp:BoundField>
                    <asp:ButtonField CommandName="Delete" Text="Eliminar" ShowHeader="True" HeaderText="Eliminar"></asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
