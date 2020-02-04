using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.UI.HtmlControls;
using Pdf_In_Browser_1.BackendClasses;

namespace Pdf_In_Browser_1
{
    public class PdfPageAPIController : ApiController
    {

        public String Get(String pageNum)
        {
            String imagePath = Url.Content("~/TestImages/" + pageNum + ".png");
            return imagePath;
        }


        [HttpPost]
        public void Post(String filename)
        {

            if(filename.EndsWith("f"))
            {
                filename = filename.Substring(0, filename.Length - 2);
                MainController pageController = new MainController(filename);
                pageController.saveFirstImages();
            }
            else if(filename.EndsWith("a"))
            {
                filename = filename.Substring(0, filename.Length - 2);
                MainController pageController = new MainController(filename);
                pageController.saveAllImages();
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