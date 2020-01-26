﻿using System;
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
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public System.Web.UI.WebControls.FileUpload fileUpload
        {
            get { return FileUpload1; }
        }


        public void DisplayPage(int pageNum)
        {
            PdfToImageConverter converter = new PdfToImageConverter();
            Image pageImg = null;

            string fileName = GetFileNameFromUI();
            string imgPath = GetImgFilePath(fileName, pageNum);

            PdfDocument document = GetDocument(fileName);

            pageImg = converter.pdfToImageByPage(pageNum, document);
            pageImg.Save(Server.MapPath(imgPath));

            AddImgToHtml(pageNum, imgPath);
        }

        public void DisplayAllPages()
        {
            string fileName = GetFileNameFromUI();

            PdfDocument document = GetDocument(fileName);
            PdfPageCollection pages = document.Pages;

            UpdatePageTotalUI(pages.Count, fileName);

            for (int pageNum = 0; pageNum < pages.Count; pageNum++)
            {
                DisplayPage(pageNum);
            }
        }

        public string GetFileNameFromUI()
        {
            return Path.GetFileName(FileUpload1.FileName);
        }

        public string GetPdfFilePath(string fileName)
        {
            return Server.MapPath("~/Pdf's/") + fileName;
        }

        public string GetImgFilePath(string fileName, int i)
        {
            return "~/TestImages/" + fileName + i.ToString() + ".png";
        }

        public PdfDocument GetDocument(string fileName)
        {
            string documentPath = GetPdfFilePath(fileName);
            PdfDocument document = null;

            try
            {
                FileUpload1.SaveAs(documentPath);
                document = new PdfDocument(documentPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return document;
        }

        public void AddImgToHtml(int pageNum, string path)
        {

            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlImage img = new HtmlImage();

            img.Src = path;
            img.ID = "img" + pageNum;
            div.Controls.Add(img);
            div.ID = "div" + pageNum;

            if (pageNum % 2 == 0)
            {
                customViewerL.Controls.Add(div);
            }
            else
            {
                customViewerR.Controls.Add(div);
            }
        }

        public void UpdatePageTotalUI(int total, string fileName)
        {
            pageCount.Text = "/" + total.ToString() + "   " + fileName;
        }

        protected void btnLoadPdf_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)
            {
            DisplayAllPages();

            PdfTextExtractor textExtractor = new PdfTextExtractor();
            textExtractor.getPdfText();
            }
        }
    }
}