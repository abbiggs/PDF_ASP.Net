<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pdf_In_Browser_1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="styles.css" rel="stylesheet" type="text/css" />
    
    <div class="actionButtonsDiv" runat="server">
        <asp:Button class="actionButton" ID="btnLoadPdf" runat="server" OnClick="btnLoadPdf_Click" Text="Load Pdf" />
        <asp:FileUpload class="actionButton" ID="FileUpload1" accept=".pdf" runat="server" />
        <%-- <asp:TextBox class="manualPageInput" ID="pageNum" runat="server" OnClick="return false;"></asp:TextBox> --%>
        <button class="actionButton" id="btnPageUp" onclick="pageUp()">▲</button>
        <button class="actionButton" id="btnPageDown" onclick="pageDown()">▼</button>
        <input type="text" class="manualPageInput" name="pageNum" id="manualPageInput" onkeydown="return jumpToPage(event)"/> 
        <asp:Label class="pageCount" ID="pageCount" runat="server" Text=""></asp:Label>
    </div>

    <div class="mainContainer" id="customContainer" runat="server">
        <div class="leftImageContainer" ID="customViewerL" runat="server">
        <%-- Dynamically generated images (even numbered pages) are placed here. --%>
        </div>

        <div class="rightImageContainer" ID="customViewerR" runat="server">
        <%-- Dynamically generated images (odd numbered pages) are placed here. --%>
        </div>
    </div>
    
    <script>

        function jumpToPage(event) {
            //Executes on enter key press event
            if (event.which == 13 || event.keyCode == 13) {
                try {
                    var pageNum = document.getElementById("manualPageInput").value;
                    var actualPageNum = pageNum - 1;
                    document.getElementById("MainContent_img" + actualPageNum).scrollIntoView();
                    return false;
                } catch (error) {
                    alert("Page Number Out Of Bounds.");
                    return false;
                }
            }
        }

        function pageUp(){
            return false;
        }

        function pageDown() {
            //document.getElementById("customViewer1").child
            return false;
        }

    </script>


    </asp:Content>