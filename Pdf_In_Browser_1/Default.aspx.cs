using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using Pdf_In_Browser_1.BackendClasses;

namespace Pdf_In_Browser_1
{
    public partial class Default : Page
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

        [Obsolete]
        protected void BtnLoadPdf_Click(object sender, EventArgs e)
        {

            //Deletes the contents of the TestImages Folder when LoadPDF button is clicked
            System.IO.DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/TestImages"));

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }


            if (FileUpload1.HasFile)
            {
                //Saves the document, updates page label, and then fires client side function to make API call to save all pages as images
                MainController controller = new MainController(Path.GetFileName(FileUpload1.FileName));
                SaveDocument();
                controller.GetDocument();
                UpdatePageTotalUI(controller.GetPageCount());
                Page.RegisterStartupScript("page", "<script language='javascript'>saveFirstPages()</script>");
            }
            


        }
    }
}