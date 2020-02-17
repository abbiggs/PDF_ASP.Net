using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.UI.HtmlControls;
using PDFiumSharp;
using Pdf_In_Browser_1.BackendClasses;
using Pdf_In_Browser_1.TextExtraction;

namespace Pdf_In_Browser_1
{
    public class PdfPageAPIController : ApiController
    {

        //public String Get(String pageNum)
        //{
        //    String imagePath = Url.Content("~/TestImages/" + pageNum + ".png");
        //    return imagePath;
        //}

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

            PdfPageImage page = new PdfPageImage();
            page.imgPath = Url.Content("~/TestImages/" + pageNum + ".png");
            page.textData = textData;

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






















        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}