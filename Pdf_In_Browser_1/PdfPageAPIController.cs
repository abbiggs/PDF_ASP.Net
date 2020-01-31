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
          
        PdfPageImage[] pageImages = new PdfPageImage[]
        {
            new PdfPageImage {name = "page1"},
            new PdfPageImage {name = "page2"},
            new PdfPageImage {name = "page3"},
            
        };

        




        //GET api/<controller>
        public String Get()
        {
            Url.Content("~Images/test.jpg");
            //String imagePath = HttpContext.Current.Server.MapPath("~/Images/test.jpg");
            String imagePath = Url.Content("~/Images/test.jpg");
            
            return imagePath;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

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