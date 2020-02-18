using System;
using System.IO;
using System.Drawing;
using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using Pdf_In_Browser_1.TextExtraction;
using PDFiumSharp;

namespace Pdf_In_Browser_1.BackendClasses
{
    public class MainController
    {
        readonly PathFinder finder = new PathFinder();
        private readonly string fileFromUI;
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

            HtmlGenericControl textDiv = textExtractor.GetPageText(pageNum, document);

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
            pageCount = document.Pages.Count;
            return document;
        }

        public HtmlGenericControl DisplayPage(int pageNum)
        {
            _ = new HtmlGenericControl("div");
            PdfToImageConverter converter = new PdfToImageConverter();
            string imgPath = finder.GetImgFilePath(fileFromUI, pageNum);

            PdfDocument document = GetDocument();

            Image pageImg = converter.PdfToImageByPage(pageNum, document);
            pageImg.Save(HostingEnvironment.MapPath(imgPath));

            HtmlGenericControl div = GetDiv(pageNum, imgPath, document);

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

        public void SaveImage(int pageNum)
        {
            _ = new HtmlGenericControl("div");
            PdfToImageConverter converter = new PdfToImageConverter();
            string imgPath = finder.GetImgFilePath(fileFromUI, pageNum);

            PdfDocument document = GetDocument();

            Image pageImg = converter.PdfToImageByPage(pageNum, document);
            pageImg.Save(HostingEnvironment.MapPath(imgPath));
        }


        public void SaveFirstImages()
        {
            PdfDocument document = GetDocument();
            PdfPageCollection pages = document.Pages;

            pageCount = pages.Count;

            try
            {
                for (int pageNum = 0; pageNum < 2; pageNum++)
                {
                    if (pageNum < pages.Count)
                    {
                        SaveImage(pageNum);
                    }
                }
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            
        }

        public void SaveAllImages()
        {
            PdfDocument document = GetDocument();
            PdfPageCollection pages = document.Pages;

            pageCount = pages.Count;
            if(pageCount < 2)
            {
                return;
            }
            else
            {
                for (int pageNum = 2; pageNum < pageCount; pageNum++)
                {
                    SaveImage(pageNum);
                }
            }
        }

        public void SaveAllText()
        {
            PdfDocument document = GetDocument();
            PdfPageCollection pages = document.Pages;

            pageCount = pages.Count;

            for(int i = 0; i < pageCount; i++)
            {
                File.Create(HostingEnvironment.MapPath("~/TestImages/" + i + ".txt"));
                //File.Write
            }

        }

        public HtmlGenericControl InitialPageDisplay()
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            PdfDocument document = GetDocument();
            PdfPageCollection pages = document.Pages;

            pageCount = pages.Count;

            for (int pageNum = 0; pageNum < 2; pageNum++)
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