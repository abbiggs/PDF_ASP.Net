using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using PDFiumSharp;
using System.IO;
using System.Drawing;
using Pdf_In_Browser_1.TextExtraction;

namespace Pdf_In_Browser_1.PdfRenderer
{
    public class PdfRenderer
    {
        PdfDocument document = null;
        //Loads a specific page, passed as a parameter, of the uploaded pdf.
        public System.Drawing.Image pdfToImageByPage(HttpPostedFile file, int pageNum)
        {

            PdfDocument document = null;
            PdfPage page = null;
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Image image = null;

            try
            {

                Stream stream = new MemoryStream();
                stream = file.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                byte[] bytes = reader.ReadBytes((Int32)stream.Length);

                document = new PdfDocument(bytes);
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
        public System.Drawing.Image[] pdfToImageArray(HttpPostedFile file)
        {

            PdfDocument document = null;
            PdfPageCollection pages = null;
            System.Drawing.Image[] array = null;

            try
            {

                Stream stream = new MemoryStream();
                stream = file.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                byte[] bytes = reader.ReadBytes((Int32)stream.Length);

                document = new PdfDocument(bytes);
                pages = document.Pages;

                //pageCount.Text = "/" + pages.Count.ToString() + "   " + filename;

                array = new System.Drawing.Image[pages.Count];

                for (int i = 0; i < pages.Count; i++)
                {
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

            return array;
        }

    }
}