using System;
using System.Web.Hosting;
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
            var charNum = PDFium.FPDFText_CountChars(pageText);
            var rectNum = PDFium.FPDFText_CountRects(pageText, 0, charNum);

            div.ID = "pageText" + pageNum;

            for (int count = 0; count < rectNum; count++)
            {
                HtmlGenericControl p = new HtmlGenericControl("p");

                string text;

                double leftPos;
                double rightPos;
                double botPos;
                double topPos;
                double fontSize;

                try
                {
                    PDFium.FPDFText_GetRect(pageText, count, out var left, out var top, out var right, out var bottom);
                    text = PDFium.FPDFText_GetBoundedText(pageText, left, top, right, bottom);

                    leftPos = getModPos(left, page.Width);
                    rightPos = getModPos(right, page.Width);
                    botPos = getModPos(bottom, page.Height);
                    topPos = getModPos(top, page.Height);

                    fontSize = ((rightPos - leftPos) + (topPos - botPos)) / text.Length;

                    p = getP(leftPos, botPos, fontSize, text);
                }
                catch (IndexOutOfRangeException e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }

                div.Controls.Add(p);
            }

            return div;
        }

        public double getModPos(double num, double total)
        {
            return (num / total) * 100;
        }

        public HtmlGenericControl getP(double leftPos, double botPos, double fontSize, string text)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");

            string baseStyle = "display: inline; position: absolute; z-index: 2; color: red; opacity: 0.5;";

            p.InnerHtml = text;
            p.Attributes["style"] = baseStyle + " left: " + (leftPos).ToString() + "%; bottom: " + botPos.ToString() + "%; font-size: " + fontSize.ToString() + "vw;";

            return p;
        }

        public String[,] getRawText(int pageNum, PdfDocument document)
        {
            PdfPage page = document.Pages[pageNum];
            HtmlGenericControl div = new HtmlGenericControl("div");

            var pageText = PDFium.FPDFText_LoadPage(page.Handle);
            var charNum = PDFium.FPDFText_CountChars(pageText);
            var rectNum = PDFium.FPDFText_CountRects(pageText, 0, charNum);

            div.ID = "pageText" + pageNum;

            String[,] textData = new string[2, rectNum];

            for (int count = 0; count < rectNum; count++)
            {
                HtmlGenericControl p = new HtmlGenericControl("p");

                string text;

                double leftPos;
                double rightPos;
                double botPos;
                double topPos;
                double fontSize;

                try
                {
                    PDFium.FPDFText_GetRect(pageText, count, out var left, out var top, out var right, out var bottom);
                    text = PDFium.FPDFText_GetBoundedText(pageText, left, top, right, bottom);

                    leftPos = getModPos(left, page.Width);
                    rightPos = getModPos(right, page.Width);
                    botPos = getModPos(bottom, page.Height);
                    topPos = getModPos(top, page.Height);

                    fontSize = ((rightPos - leftPos) + (topPos - botPos)) / text.Length;

                    p = getP(leftPos, botPos, fontSize, text);
                }
                catch (IndexOutOfRangeException e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }

                textData[0, count] = p.InnerHtml;
                textData[1, count] = p.Attributes["style"];
            }
            return textData;
        }
    }
}