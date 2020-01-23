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

        public HtmlGenericControl getPageText(int pageNum, PdfDocument document)
        {
            PdfPage page = document.Pages[pageNum];
            HtmlGenericControl div = new HtmlGenericControl("div");

            var pageText = PDFium.FPDFText_LoadPage(page.Handle);
            var rectNum = PDFium.FPDFText_CountRects(pageText, 0, 1000000);

            double pageHeight = page.Height;
            double pageWidth = page.Width;

            div.ID = "pageText" + pageNum;

            for (int count = 0; count < rectNum; count++)
            {
                HtmlGenericControl p = new HtmlGenericControl("p");

                PDFium.FPDFText_GetRect(pageText, count, out var left, out var top, out var right, out var bottom);
                var text = PDFium.FPDFText_GetBoundedText(pageText, left, top, right, bottom);
                
                double leftPos = (left / pageWidth) * 100; //0.9499 due to the difference in the div and img width
                double rightPos = (right / pageWidth) * 100;
                double botPos = (bottom / pageHeight) * 100; //1.0052 or 0.9948 due to the difference in the div and img height
                double topPos = (top / pageHeight) * 100;
                //double fontSize = (rightPos - leftPos) / (text.Length);
                double fontSize = ((rightPos - leftPos) + (topPos - botPos)) / (text.Length); //One of these two lines will work, just need to figureout once we get the fontStyles

                //The reason the text is offset is because the text's position is relative to the 'positioned' div that contains it and the img
                //That div's position is leaking outside of the bounds of the image, so the text isn't lining up perfectly
                //YOU NEED TO FIX THIS
                if(count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("WIDTH: " + pageWidth + "   LEFT: " + left + "   RIGHT: " + right);
                }

                p.InnerHtml = text;
                p.Attributes["style"] = "display: inline; position: absolute; z-index: 2; color: red; opacity: 0.5; left: " + (leftPos * 0.9499).ToString() + "%; bottom: " + botPos.ToString() + "%; font-size: " + fontSize.ToString() + "vw;";

                div.Controls.Add(p);
            }


            return div;
        }
    }
}