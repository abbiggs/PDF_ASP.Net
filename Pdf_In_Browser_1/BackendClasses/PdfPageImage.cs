using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using Pdf_In_Browser_1.TextExtraction;
using PDFiumSharp;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pdf_In_Browser_1
{
    public class PdfPageImage
    {
        public String imgPath { get; set; }
        public String[,] textData { get; set; }

    }
}