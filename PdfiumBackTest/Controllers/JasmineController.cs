using System;
using System.Web.Mvc;

namespace PdfiumBackTest.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
