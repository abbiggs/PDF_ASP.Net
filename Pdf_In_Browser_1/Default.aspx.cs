using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using PDFiumSharp;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Pdf_In_Browser_1.TextExtraction;

namespace Pdf_In_Browser_1
{
    public partial class _Default : Page
    {
        //public System.Drawing.Image[] currentImages = null;
        //public HtmlImage[] currentImages = null;
        public List<HtmlImage> currentImages = new List<HtmlImage>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //pageNum.Visible = false;
            
        }

        public void displayFirstPage() 
        {
            System.Drawing.Image firstPage = pdfToImageByPage(0);
            firstPage.Save(Server.MapPath("~/TestImages/firstpage.jpg"));

            HtmlImage img = new HtmlImage();
            img.Src = "~/TestImages/firstpage.jpg";

            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Controls.Add(img);
            customViewer1.Controls.Add(div);
        }

        public void displayAllPages() 
        {
            try
            {
                System.Drawing.Image[] array = pdfToImageArray();
                //Need to make sure first page is included
                for (int i = 0; i < array.Length; i++)
                {
                    System.Drawing.Image image = array[i];
                    image.Save(Server.MapPath("~/TestImages/image" + i + ".jpg"));
                    HtmlImage img = new HtmlImage();
                    img.Src = "~/TestImages/image" + i + ".jpg";
                    img.ID = "img" + i;
                    currentImages.Add(img);

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.ID = "div" + i;
                    div.Controls.Add(img);
                    if (i % 2 == 0)
                    {
                        customViewer1.Controls.Add(div);
                    }
                    else
                    {
                        customViewer2.Controls.Add(div);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        //Loads a specific page, passed as a parameter, of the uploaded pdf.
        public System.Drawing.Image pdfToImageByPage(int pageNum) 
        {
            PdfDocument document = null;
            PdfPage page = null;
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Image image = null;
            try
            {
                string filename = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/Pdf's/") + filename);

                document = new PdfDocument(Server.MapPath("~/Pdf's/") + filename);
                page = document.Pages[pageNum];
                
                bitmap = new Bitmap(1920, 2200);
                PDFiumSharp.RenderingExtensionsGdiPlus.Render(page, bitmap);
                
                image = bitmap;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return image;
        }

        //Converts all pages into images, and returns them as an array of images
        public System.Drawing.Image[] pdfToImageArray() {
            PdfDocument document = null;
            PdfPageCollection pages = null;
            System.Drawing.Image[] array = null;
            try
            {
                string filename = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/Pdf's/") + filename);

                document = new PdfDocument(Server.MapPath("~/Pdf's/") + filename);
                pages = document.Pages;
                pageCount.Text = "/" + pages.Count.ToString() + "   " + filename;
                array = new System.Drawing.Image[pages.Count];
                for (int i = 0; i < pages.Count; i++) {
                    PdfPage page = pages[i];
                    Bitmap bitmap = new Bitmap(1920, 2200);
                    PDFiumSharp.RenderingExtensionsGdiPlus.Render(page, bitmap);
                    System.Drawing.Image image = bitmap;
                    array[i] = image;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            //currentImages = array;
            return array;
        }

        protected void btnLoadPdf_Click(object sender, EventArgs e)
        {
            //pageNum.Visible = true;
            //displayFirstPage();
            //if (pdfToImageByPage(1) != null)
            //{
            //    displayAllPages();
            //}
            displayAllPages();

            PdfTextExtractor textExtractor = new PdfTextExtractor();
            textExtractor.getPdfText();
            
        }
    }
}