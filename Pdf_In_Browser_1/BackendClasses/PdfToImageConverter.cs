using System;
using System.Drawing;
using PDFiumSharp;

namespace Pdf_In_Browser_1
{

    //This class is responsible for converting the PDFs to imgs
    public class PdfToImageConverter {

        //Loads a specific page
        public Image PdfToImageByPage(int pageNum, PdfDocument document)
        {
            Image image = null;
            try
            {

                PdfPage page = document.Pages[pageNum];
                int width = Convert.ToInt32(page.Width);
                int height = Convert.ToInt32(page.Height);

                //Use (int)page.Width and (int)page.Height in future
                Bitmap bitmap = new Bitmap(width, height);
                bitmap.SetResolution(400.0f, 400.0f);
                RenderingExtensionsGdiPlus.Render(page, bitmap);
                image = bitmap;

            } catch (Exception ex) {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return image;
        }

        //Loads a selection of pages
        public Image[] PdfToImageArray(int startingPage, int endingPage, PdfDocument document)
        {

            Image[] array = null;

            for(int i = startingPage; i < endingPage; i++) {

                array[i] = PdfToImageByPage(i, document);
            }

            return array;
        }
    }
}