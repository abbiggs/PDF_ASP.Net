<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pdf_In_Browser_1._Default" %>
 

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="styles.css" rel="stylesheet" type="text/css" />
    
    <div class="actionButtonsDiv" runat="server">
        <asp:Button class="actionButton" ID="btnLoadPdf" runat="server" OnClick="btnLoadPdf_Click" Text="Load Pdf" />
        <asp:FileUpload class="actionButton" ID="FileUpload1" accept=".pdf" runat="server" />
        <%-- <asp:TextBox class="manualPageInput" ID="pageNum" runat="server" OnClick="return false;"></asp:TextBox> --%>
        <input type="text" class="manualPageInput" name="pageNum" id="manualPageInput" onkeydown="return jumpToPage(event)"/> 
        <asp:Label class="pageCount" ID="pageCount" runat="server" Text=""></asp:Label>
    </div>

    <div class="mainContainer" id="customContainer" runat="server">
        <div class="leftImageContainer" ID="customViewer1" runat="server">
        <%-- Dynamically generated images (even numbered pages) are placed here. --%>
        </div>

        <div class="rightImageContainer" ID="customViewer2" runat="server">
        <%-- Dynamically generated images (odd numbered pages) are placed here. --%>
        </div>
    </div>
    
    <script>

        function jumpToPage(event) {
            //Executes on enter key press event
            if (event.which == 13 || event.keyCode == 13) {
                var pageNum = document.getElementById("manualPageInput").value;
                document.getElementById("MainContent_img" + pageNum).scrollIntoView();
                return false;
            }
        }

    </script>


    </asp:Content>