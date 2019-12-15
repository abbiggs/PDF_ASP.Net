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
            PDFiumSharp.PDFiumBitmap bitmap = null;
            try
            {
                document = new PdfDocument("C:/Users/ab716/Desktop/PDF_InBrowserRendering/PDF's/annotation links.pdf");
                System.Diagnostics.Debug.WriteLine("File Version: " + document.FileVersion.ToString());
                //document.Close();

                pageCollection = document.Pages;
                System.Diagnostics.Debug.WriteLine("Number of pages: " + pageCollection.Count);

                bitmap = new PDFiumBitmap(500, 500, true);

                page = pageCollection[0];

                page.Render(bitmap);
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
                using (var Stream = new MemoryStream())
                {
                    bitmap.Save(Stream, 500, 500);

                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(Stream))
                    {
                        // Upon success image contains the bitmap
                        // And can be saved to a file:
                        try
                        {

                            image.Save("C:/Users/ab716/Desktop/test3.png");

                            //This isn't the image that's being saved, this is just a test image that's in the project directory, still have work to do here
                            Image1.ImageUrl = "~/Images/test.jpg";
                        }
                        catch (System.Exception ex)
                        {
                            ex.ToString();
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }
    }
}