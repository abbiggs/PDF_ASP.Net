﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pdf_In_Browser_1._Default" %>
 

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" width="100%">
    <link href="styles.css" rel="stylesheet" type="text/css" />
    <%-- <div ID="customViewer" class="jumbotron" draggable="true" runat="server">
        
        
        &nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>--%>



    <div class="actionButtonsDiv" runat="server">
        <asp:Button class="actionButton" ID="btnLoadPdf" runat="server" OnClick="btnLoadPdf_Click" Text="Load Pdf" />
        <asp:FileUpload class="actionButton" ID="FileUpload1" runat="server" />
    </div>

    <div class="viewer" ID="customViewer1" runat="server">
        <%-- Dynamically generated images are placed in this DIV. --%>

    </div>


    </asp:Content>