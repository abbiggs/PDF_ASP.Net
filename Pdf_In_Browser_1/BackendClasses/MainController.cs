﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using Pdf_In_Browser_1.TextExtraction;
using PDFiumSharp;

namespace Pdf_In_Browser_1.BackendClasses
{
    public class MainController
    {

        PathFinder finder = new PathFinder();
        string fileFromUI;
        int pageCount;

        public MainController(string UIinfo)
        {
            fileFromUI = UIinfo;
        }

        public HtmlGenericControl GetDiv(int pageNum, string path, PdfDocument document)
        {
            PdfTextExtractor textExtractor = new PdfTextExtractor();
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlImage img = new HtmlImage();

            HtmlGenericControl textDiv = textExtractor.getPageText(pageNum, document);

            img.Src = path;
            img.ID = "img" + pageNum;
            div.Controls.Add(img);
            div.Controls.Add(textDiv);
            div.ID = "div" + pageNum;
            div.Attributes["style"] = "position: relative;";

            return div;
        }

        public PdfDocument GetDocument()
        {

            string documentPath = finder.GetPdfFilePath(fileFromUI);
            PdfDocument document = null;

            try
            {

                document = new PdfDocument(documentPath);

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            
            return document;
        }

        public HtmlGenericControl DisplayPage(int pageNum)
        {

            HtmlGenericControl div = new HtmlGenericControl("div");
            PdfToImageConverter converter = new PdfToImageConverter();
            Image pageImg = null;

            string imgPath = finder.GetImgFilePath(fileFromUI, pageNum);

            PdfDocument document = GetDocument();

            pageImg = converter.pdfToImageByPage(pageNum, document);
            pageImg.Save(HostingEnvironment.MapPath(imgPath));

            div = GetDiv(pageNum, imgPath, document);

            return div;
        }

        public HtmlGenericControl DisplayAllPages()
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            PdfDocument document = GetDocument();
            PdfPageCollection pages = document.Pages;

            pageCount = pages.Count;

            for (int pageNum = 0; pageNum < pageCount; pageNum++)
            {

                div.Controls.Add(DisplayPage(pageNum));
            }

            return div;
        }

        public int GetPageCount()
        {
            return pageCount;
        }
    }
}