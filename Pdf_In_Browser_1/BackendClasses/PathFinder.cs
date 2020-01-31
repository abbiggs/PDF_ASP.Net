using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Pdf_In_Browser_1.BackendClasses
{
    public class PathFinder
    {

        public string GetFileName(string fileName)
        {

            return Path.GetFileName(fileName);
        }

        public string GetPdfFilePath(string fileName)
        {

            return HostingEnvironment.MapPath("~/Pdf's/") + fileName;
        }

        public string GetImgFilePath(string fileName, int i)
        {

            //return "~/TestImages/" + fileName + i.ToString() + ".png";
            return "~/TestImages/" + i.ToString() + ".png";
        }
    }
}