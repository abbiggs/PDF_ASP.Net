using System;
using System.Web.Http;
using PDFiumSharp;
using Pdf_In_Browser_1.BackendClasses;
using Pdf_In_Browser_1.TextExtraction;

namespace Pdf_In_Browser_1
{
    public class PdfPageAPIController : ApiController
    {

        public PdfPageImage Get(String filename)
        {
            String pageNum = "";
            String actualFile = "";

            Char[] fileAsArr = filename.ToCharArray();
           
            for(int i = 0; i < fileAsArr.Length; i++)
            {
                if(fileAsArr[i].ToString() == "_"){
                    pageNum = filename.Substring(0, i);
                    actualFile = filename.Substring(i + 1, filename.Length - i - 1);
                    break;
                }
            }

            MainController pageController = new MainController(actualFile);
            PdfDocument document = pageController.GetDocument();
            PdfTextExtractor extractor = new PdfTextExtractor();

            String[,] textData = extractor.GetRawText(Convert.ToInt32(pageNum), document);

            PdfPageImage page = new PdfPageImage
            {
                imgPath = Url.Content("~/TestImages/" + pageNum + ".png"),
                textData = textData
            };

            return page;
        }


        [HttpPost]
        public void Post(String filename)
        {

            if(filename.EndsWith("f"))
            {
                filename = filename.Substring(0, filename.Length - 2);
                MainController pageController = new MainController(filename);
                pageController.SaveFirstImages();
            }
            else if(filename.EndsWith("a"))
            {
                filename = filename.Substring(0, filename.Length - 2);
                MainController pageController = new MainController(filename);
                pageController.SaveAllImages();
            }
        }
    }
}