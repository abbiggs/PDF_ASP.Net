using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Http;
using PDFiumSharp;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Pdf_In_Browser_1.TextExtraction;

namespace Pdf_In_Browser_1.AsyncLoading
{
    public class AsyncPageLoader
    {
        PdfToImageConverter imageConverter = new PdfToImageConverter();

        public Image loadPage(PdfDocument document, int pageNum)
        {
            return imageConverter.pdfToImageByPage(pageNum, document);
        }




    }
}