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

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Loads the Pdf document
            PDFiumSharp.PdfDocument document = null;
            PDFiumSharp.PdfPageCollection pageCollection = null;
            PDFiumSharp.PdfPage page = null;
            System.Drawing.Bitmap bitmap1 = null;
            try
            {
                document = new PdfDocument("C:/Users/ab716/Desktop/PDF_InBrowserRendering/PDF's/annotation links.pdf");
                System.Diagnostics.Debug.WriteLine("File Version: " + document.FileVersion.ToString());
                //document.Close();

                pageCollection = document.Pages;
                System.Diagnostics.Debug.WriteLine("Number of pages: " + pageCollection.Count);

                page = pageCollection[0];
                bitmap1 = new Bitmap(2000, 2000);
                PDFiumSharp.RenderingExtensionsGdiPlus.Render(page, bitmap1);
                
                document.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            //Saves the bitmap to a memory stream
            //Draws an image from the stream, and attempts to save it to the projects Images folder
            //After saving, attempts to display the image in the web pages' image control
            try
            {
                System.Drawing.Image image1 = bitmap1;
                image1.Save("C:/Users/ab716/Desktop//test_folder/test5.Bmp");
                //This isn't the image that's being saved, this is just a test image that's in the project directory, still have work to do here
                Image1.ImageUrl = "~/Images/test.jpg";         
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}