<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pdf_In_Browser_1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="styles.css" rel="stylesheet" type="text/css" />
    
    <div id="errorMsgDiv">Failed To Load Page</div>

    <div class="actionButtonsDiv" id="actionButtonsContainer" runat="server">
        <asp:Button CSSClass="actionButton" ID="btnLoadPdf" runat="server" OnClick="BtnLoadPdf_Click" Text="Load PDF" />
        <%-- %> <button class="actionButton" id="btnTestAPI" onclick="return loadFirstPages()">Test API</button> --%>
        <asp:FileUpload CSSClass="actionButton" ID="FileUpload1" runat="server" AllowMultiple="true" />
        <%-- <asp:TextBox class="manualPageInput" ID="pageNum" runat="server" OnClick="return false;"></asp:TextBox> --%>
        <input type="text" class="manualPageInput" name="pageNum" id="manualPageInput" onkeydown="return jumpToPage(event)"/> 
        <asp:Label CSSClass="pageCount" ID="pageCount" runat="server" Text=""></asp:Label>

        <input type="button" class="actionButton" id="btnZoomIn" value="+" onclick="return zoomIn()"/>
        <input type="button" class="actionButton" id="btnZoomOut" value="-" onclick="return zoomOut()"/>
        
        <input type="button" class="actionButton" id="btnRedact" value="Redact" onclick="return activateRedaction()"/>
    </div>

    <div class="mainContainer" id="customContainer" runat="server">
        <div class="leftImageContainer" id="customViewerL" runat="server">
        <%-- Dynamically generated images (even numbered pages) are placed here. --%>
        </div>
    </div>
    
    <script src="Scripts/Frontend/FrontendControls.js"></script>
    <script src="Scripts/Frontend/AnnotationControls.js"></script>
    <script src="Scripts/Frontend/ZoomingControls.js"></script>
    

    </asp:Content>