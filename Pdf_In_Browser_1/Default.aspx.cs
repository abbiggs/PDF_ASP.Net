using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using PDFiumSharp;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Pdf_In_Browser_1.BackendClasses;

namespace Pdf_In_Browser_1
{
    public partial class _Default : Page
    {

        public void AddElementsToContainer(HtmlGenericControl div)
        {
            customViewerL.Controls.Add(div);
        }

        public void UpdatePageTotalUI(int total)
        {

            pageCount.Text = "/" + total.ToString() + "   " + FileUpload1.FileName;
        }

        public void SaveDocument()
        {
            PathFinder finder = new PathFinder();
            string currentPath = Path.GetFileName(FileUpload1.FileName);
            string path = finder.GetPdfFilePath(currentPath);

            FileUpload1.SaveAs(path);
        }

        protected void btnLoadPdf_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                MainController controller = new MainController(Path.GetFileName(FileUpload1.FileName));
                SaveDocument();
                //AddElementsToContainter(controller.InitialPageDisplay());

                AddElementsToContainer(controller.DisplayPage(0));
                AddElementsToContainer(controller.DisplayPage(1));
                controller.saveAllImages();
                UpdatePageTotalUI(controller.GetPageCount());
            }
        }
    }
}