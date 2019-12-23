using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using PDFiumSharp;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace Pdf_In_Browser_1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLoadPdf_Click(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Image[] array = pdfToImageArray();
                for (int i = 0; i < array.Length; i++) 
                {
                    System.Drawing.Image image = array[i];
                    image.Save(Server.MapPath("~/TestImages/image" + i + ".jpg"));
                    HtmlImage img = new HtmlImage();
                    img.Src = "~/TestImages/image" + i + ".jpg";
                    customViewer1.Controls.Add(img);
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
                
                bitmap = new Bitmap(2000, 2000);
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
                array = new System.Drawing.Image[pages.Count];
                for (int i = 0; i < pages.Count; i++) {
                    PdfPage page = pages[i];
                    Bitmap bitmap = new Bitmap(2000, 2000);
                    PDFiumSharp.RenderingExtensionsGdiPlus.Render(page, bitmap);
                    System.Drawing.Image image = bitmap;
                    array[i] = image;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return array;
        }
    }
}