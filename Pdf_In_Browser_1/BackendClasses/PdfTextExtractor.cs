﻿using System;
using PDFiumSharp;
using System.Web.UI.HtmlControls;

namespace Pdf_In_Browser_1.TextExtraction
{
    public class PdfTextExtractor
    {

        public HtmlGenericControl GetPageText(int pageNum, PdfDocument document)
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

                    leftPos = GetModPos(left, page.Width);
                    rightPos = GetModPos(right, page.Width);
                    botPos = GetModPos(bottom, page.Height);
                    topPos = GetModPos(top, page.Height);

                    fontSize = ((rightPos - leftPos) + (topPos - botPos)) / text.Length;

                    p = GetP(leftPos, botPos, fontSize, text);
                }
                catch (IndexOutOfRangeException e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }

                div.Controls.Add(p);
            }

            return div;
        }

        public double GetModPos(double num, double total)
        {
            return (num / total) * 100;
        }

        public HtmlGenericControl GetP(double leftPos, double botPos, double fontSize, string text)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");

            string baseStyle = "display: inline; position: absolute; z-index: 2; color: red; opacity: 0.5;";

            p.InnerHtml = text;
            p.Attributes["style"] = baseStyle + " left: " + (leftPos).ToString() + "%; bottom: " + botPos.ToString() + "%; font-size: " + fontSize.ToString() + "vw;";

            return p;
        }

        public String[,] GetRawText(int pageNum, PdfDocument document)
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

                    leftPos = GetModPos(left, page.Width);
                    rightPos = GetModPos(right, page.Width);
                    botPos = GetModPos(bottom, page.Height);
                    topPos = GetModPos(top, page.Height);

                    fontSize = ((rightPos - leftPos) + (topPos - botPos)) / text.Length;

                    p = GetP(leftPos, botPos, fontSize, text);
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