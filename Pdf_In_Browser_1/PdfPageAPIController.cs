using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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


        
        



        // GET api/<controller>
        public IEnumerable<PdfPageImage> Get()
        {
            return pageImages;
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