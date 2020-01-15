using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using PDFiumSharp;

namespace Pdf_In_Browser_1 {

    //This class is responsible for converting the PDFs to imgs
    public class PdfToImageConverter {

        //Loads a specific page
        public Image pdfToImageByPage(int pageNum, PdfDocument document) {

            Bitmap bitmap = null;
            Image image = null;
            PdfPage page = null;

            try {

                page = document.Pages[pageNum];

                //Use (int)page.Width and (int)page.Height in future
                bitmap = new Bitmap(1920, 2200);
                RenderingExtensionsGdiPlus.Render(page, bitmap);
                image = bitmap;

            } catch (Exception ex) {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return image;
        }


        //Loads a selection of pages
        public Image[] pdfToImageArray(int startingPage, int endingPage, PdfDocument document) {

            Image[] array = null;

            for(int i = startingPage; i < endingPage; i++) {

                array[i] = pdfToImageByPage(i, document);
            }

            return array;
        }
    }
}