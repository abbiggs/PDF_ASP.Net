using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                System.Drawing.Image image1 = pdfToImage(0);

                image1.Save(Server.MapPath("~/TestImages/lol.jpg"));
                
                testImage.Src = "~/TestImages/lol.jpg";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        //This gets the filepath of the uploaded file, converts to image and returns the image
        //Eventually this needs to take the uploaded file and convert it to a stream, before beginning conversions
        //as to eliminate the need for filepaths
        public System.Drawing.Image pdfToImage(int pageNum) 
        {
            PdfDocument document = null;
            PdfPage page = null;
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Image image = null;
            try
            {
                string filename = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/") + filename);


                document = new PdfDocument(Server.MapPath("~/") + filename);
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
    }
}