using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Http;
using PDFiumSharp;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Pdf_In_Browser_1.TextExtraction;

namespace Pdf_In_Browser_1
{
    public partial class PageDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void testMethod()
        {
            System.Diagnostics.Debug.WriteLine("Test");
        }
    }
}