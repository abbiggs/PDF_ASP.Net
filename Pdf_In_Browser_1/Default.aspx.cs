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
            //Current error occurs here
            PDFiumSharp.PdfDocument document = new PdfDocument("~/Images/blank.pdf");
            //Collection of the documents pages
            PDFiumSharp.PdfPageCollection pageCollection = document.Pages;
            //Selects the first page
            PDFiumSharp.PdfPage page = pageCollection[0];

            //Creates a bitmap, and renders the first page to the bitmap
            PDFiumSharp.PDFiumBitmap bitmap = new PDFiumBitmap(500, 500, true);
            page.Render(bitmap);



            //Saves the bitmap to a memory stream
            //Draws an image from the stream, and attempts to save it to the projects Images folder
            //After saving, attempts to display the image in the web pages image control
            using (var Stream = new MemoryStream())
            {
                bitmap.Save(Stream, 500, 500);

                using (System.Drawing.Image image = System.Drawing.Image.FromStream(Stream))
                {
                    // Upon success image contains the bitmap
                    // And can be saved to a file:
                    try
                    {
                        //If it makes it past the error when loading the document, another 
                        //Error occurs here
                        image.Save("~/Images/testImage", System.Drawing.Imaging.ImageFormat.Bmp);
                        Image1.ImageUrl = "~/Images/testImage.Bmp";
                    }
                    catch (System.Exception ex)
                    {
                        ex.ToString();
                        Console.WriteLine(ex);
                    }

                }
            }
        }
    }
}