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
        
        //GET api/<controller>
        //public String Get()
        //{
        //    String imagePath = Url.Content("~/Images/test.jpg");
        //    return imagePath;
        //}

        public String Get(String pageNum)
        {
            String imagePath = Url.Content("~/TestImages/" + pageNum + ".png");
            return imagePath;
        }






















        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

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