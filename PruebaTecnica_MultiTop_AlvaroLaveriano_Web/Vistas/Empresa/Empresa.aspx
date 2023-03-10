<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Empresa.aspx.cs" Inherits="PruebaTecnica_MultiTop_AlvaroLaveriano_Web.Vistas.Empresa.Empresa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../../Content/bootstrap.min.css" crossorigin="anonymous" />
    <link rel="stylesheet" href="../../Content/bootstrap-theme.min.css" crossorigin="anonymous" />
    <script src="../../Scripts/bootstrap.min.js" crossorigin="anonymous"></script>

    <title></title>
</head>
<body>

    <form id="formEmpleado" runat="server">

        <div class="navbar navbar-inverse">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse" title="more options">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/">Mantenimiento de Empresas</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li>
                            <asp:LinkButton ID="lbNuevo" runat="server" OnClick="lbNuevo_Click">Nuevo</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbGuardar" runat="server" OnClick="lbGuardar_Click">Guardar</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbEliminar" runat="server" OnClick="lbEliminar_Click">Eliminar</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbConsultar" runat="server" OnClick="lbConsultar_Click">Consultar</asp:LinkButton></li>
                        <li><a runat="server" href="~/">Salir</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="container body-content">

            <div class="form-group">
                <label class="control-label">Código</label>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="True"></asp:TextBox>
            </div>

            <div class="form-group">
                <label class="control-label">RUC</label>
                <asp:TextBox ID="txtRUC" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
            </div>

            <div class="form-group">
                <label class="control-label">Razón Social</label>
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label class="control-label">Estado</label>
                <div>
                    <asp:RadioButton ID="rbActivo" runat="server" />
                    <label class="form-check-label">Activo</label>
                    <asp:RadioButton ID="rbInactivo" runat="server" />
                    <label class="form-check-label">Inactivo</label>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label">Dirección</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Button ID="btnAgregarDireccion" runat="server" class="btn btn-primary" Text="Agregar Dirección" OnClick="btnAgregarDireccion_Click"></asp:Button>
            </div>

            <br />

            <div class="form-group">
                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvDatos_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Item" HeaderText="#" ItemStyle-Width="10"></asp:BoundField>
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" ItemStyle-Width="250"></asp:BoundField>
                        <asp:ButtonField CommandName="Delete" Text="Eliminar"></asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
