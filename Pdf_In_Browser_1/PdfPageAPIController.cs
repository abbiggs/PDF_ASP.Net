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
            String[] parsedRequest = parseRequest(filename);
            PdfPageImage page = getPage(parsedRequest[0], parsedRequest[1]);

            return page;
        }

        public String[] parseRequest(String request)
        {
            String pageNum = "";
            String actualFile = "";

            Char[] fileAsArr = request.ToCharArray();

            for (int i = 0; i < fileAsArr.Length; i++)
            {
                if (fileAsArr[i].ToString() == "_")
                {
                    pageNum = request.Substring(0, i);
                    actualFile = request.Substring(i + 1, request.Length - i - 1);
                    break;
                }
            }

            String[] parsedRequest = new String[2];
            parsedRequest[0] = actualFile;
            parsedRequest[1] = pageNum;
            return parsedRequest;
        }

        public PdfPageImage getPage(String filename, String pageNum)
        {
            MainController pageController = new MainController(filename);
            PdfDocument document = pageController.GetDocument();
            PdfTextExtractor extractor = new PdfTextExtractor();

            String[,] textData = extractor.GetRawText(Convert.ToInt32(pageNum), document);  //System.Format Exception

            PdfPageImage page = new PdfPageImage
            {
                imgPath = Url.Content("~/TestImages/" + pageNum + ".png"),
                textData = textData
            };

            return page;
        }

        [HttpPost]
        public double Post(String filename)
        {
            if(filename.EndsWith("f"))
            {
                filename = filename.Substring(0, filename.Length - 2);
                MainController pageController = new MainController(filename);
                pageController.SaveFirstImages();
                return pageController.GetTotalPageHeight(pageController.GetDocument());
            }
            else if(filename.EndsWith("a"))
            {
                filename = filename.Substring(0, filename.Length - 2);
                MainController pageController = new MainController(filename);
                pageController.SaveAllImages();
                return 0.0;
            }
            return 0.0;
        }
    }
}