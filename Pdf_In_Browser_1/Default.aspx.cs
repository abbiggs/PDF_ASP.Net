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

namespace Pdf_In_Browser_1
{
    public partial class _Default : Page
    {
        //public List<HtmlImage> currentImages = new List<HtmlImage>();
        public Image[] currentImages = null;
        public string currentFileName = null;
        public PdfDocument currentPdf = null;
        public PdfPageCollection currentPages = null;

        public int pagesLoaded = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.QueryString.HasKeys() || string.IsNullOrEmpty(Request.QueryString["m"]))
            {
                System.Diagnostics.Debug.WriteLine("Test Failed");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Test Passed");
                System.Diagnostics.Debug.WriteLine(Request.QueryString["m"]);
                displayNextPages();
            }   
        }

        public void DisplayPage(int pageNum)
        {
            PdfToImageConverter converter = new PdfToImageConverter();
            Image pageImg = null;

            string fileName = GetFileNameFromUI();
            string imgPath = GetImgFilePath(fileName, pageNum);
            System.Diagnostics.Debug.WriteLine(imgPath);

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

        public void initialDisplay()
        {
            string fileName = GetFileNameFromUI();
            currentFileName = fileName;

            PdfDocument document = GetDocument(fileName);
            currentPdf = document;
            
            PdfPageCollection pages = document.Pages;
            currentPages = pages;

            UpdatePageTotalUI(pages.Count, fileName);

            for (int pageNum = 0; pageNum < 2; pageNum++)
            {
                DisplayPage(pageNum);
            }
            pagesLoaded = 2;
        }

        public void testDisplayPage(int pageNum)
        {
            PdfToImageConverter converter = new PdfToImageConverter();
            Image pageImg = null;

            string fileName = currentFileName;
            string imgPath = GetImgFilePath(fileName, pageNum);
            System.Diagnostics.Debug.WriteLine(imgPath);
            PdfDocument document = currentPdf;
            if(currentPdf is null)
            {
                System.Diagnostics.Debug.WriteLine("currentPdf is null");
            }
            pageImg = converter.pdfToImageByPage(pageNum, document);
            if(pageImg is null)
            {
                System.Diagnostics.Debug.WriteLine("image is null");
            }
            pageImg.Save(Server.MapPath(imgPath));

            AddImgToHtml(pageNum, imgPath);
        }


        public void displayNextPages()
        {
            string fileName = currentFileName;
            PdfDocument document = currentPdf;
            PdfPageCollection pages = currentPages;

            for(int pageNum = pagesLoaded + 1; pageNum < pagesLoaded + 3; pageNum++)
            {
                testDisplayPage(pageNum);
            }
            pagesLoaded += 2;
        }






        protected void btnLoadPdf_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                initialDisplay();

                PdfTextExtractor textExtractor = new PdfTextExtractor();
                textExtractor.getPdfText();
            }
        }
    }
}