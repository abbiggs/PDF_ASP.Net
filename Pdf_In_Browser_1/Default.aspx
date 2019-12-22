<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pdf_In_Browser_1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div ID="viewer" class="jumbotron" draggable="true">
        <asp:Button ID="btnLoadPdf" runat="server" OnClick="btnLoadPdf_Click" Text="Load Pdf" />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <br />
        <br />
        <img runat="server" id="testImage" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>

    </asp:Content>
