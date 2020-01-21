using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PDFiumSharp;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;

namespace Pdf_In_Browser_1.TextExtraction
{
    public class PdfTextExtractor
    {
        //Prints the text from the pdfdocument to the console.
        public void getPdfText()
        {
            try
            {
                PdfDocument document = new PdfDocument("C:/Users/ab716/Desktop/PDF_InBrowserRendering/PDF's/CS 369 REGISTRATION.pdf");
                PdfPage page = document.Pages[0];

                var textPage = PDFium.FPDFText_LoadPage(page.Handle);
                var rectCount = PDFium.FPDFText_CountRects(textPage, 0, 1000000);
                
                for (int i = 0; i < rectCount; i++)
                {
                    PDFium.FPDFText_GetRect(textPage, i, out var l, out var t, out var r, out var b);
                    var text = PDFium.FPDFText_GetBoundedText(textPage, l, t, r, b);
                    
                    //System.Diagnostics.Debug.WriteLine(text.ToString());
                    System.Diagnostics.Debug.WriteLine($"{(int)l}, {(int)t}, {(int)r}, {(int)b} - {text}");
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public HtmlGenericControl getPageText(int pageNum, PdfDocument document)
        {
            //Get text data
            //Create html obj from data
            //Add html obj to div
            //return div object
            PdfPage page = document.Pages[pageNum];
            HtmlGenericControl div = new HtmlGenericControl("div");

            var pageText = PDFium.FPDFText_LoadPage(page.Handle);
            var rectNum = PDFium.FPDFText_CountRects(pageText, 0, 1000000);

            div.ID = "pageText" + pageNum;

            for(int count = 0; count < rectNum; count++)
            {
                HtmlGenericControl p = new HtmlGenericControl("p");

                PDFium.FPDFText_GetRect(pageText, count, out var left, out var top, out var right, out var bottom);
                var text = PDFium.FPDFText_GetBoundedText(pageText, left, top, right, bottom);

                p.InnerHtml = text;
                //p.Attributes["display"] = "inline";
                //p.Attributes["position"] = "absolute";
                //p.Attributes["left"] = Convert.ToInt32(left).ToString() + "px";
                //p.Attributes["top"] = Convert.ToInt32(top).ToString() + "px";
                //p.Attributes["z-index"] = "2";
                p.Attributes["style"] = "display: inline; position: absolute; z-index: 2; left: " + Convert.ToInt32(left).ToString() + "px; bottom: " + Convert.ToInt32(top).ToString() + "px;";

                div.Controls.Add(p);
            }


            return div;
        }
    }
}