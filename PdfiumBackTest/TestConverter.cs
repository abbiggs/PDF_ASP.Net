using System;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pdf_In_Browser_1;
using Pdf_In_Browser_1.BackendClasses;
using Pdf_In_Browser_1.TextExtraction;
using PDFiumSharp;

namespace PdfiumBackTest {

    [TestClass]
    public class TestConverter {

        [TestMethod]
        public void TestGetFilePath()
        {
            PathFinder finder = new PathFinder();
            string fileName = "testdoc.pdf";

            string returned = finder.GetPdfFilePath(fileName);

            Assert.AreEqual(returned, "testdoc.pdf");
        }

        [TestMethod]
        public void TestGetFilePath2()
        {
            PathFinder finder = new PathFinder();
            string fileName = "testdoc.pdf";

            string returned = finder.GetPdfFilePath(fileName);

            Assert.AreNotEqual(returned, "annotations.pdf");
        }

        [TestMethod]
        public void TestTextPositioning()
        {
            PdfTextExtractor extractor = new PdfTextExtractor();

            double result = extractor.GetModPos(50, 200);

            Assert.AreEqual(25, result);
        }

        [TestMethod]
        public void TestTextPositioning2()
        {
            PdfTextExtractor extractor = new PdfTextExtractor();

            double result = extractor.GetModPos(150, 200);

            Assert.AreNotEqual(25, result);
        }

        [TestMethod]
        public void TestGetDocument()
        {
            MainController controller = new MainController("testdoc.pdf");

            PdfDocument doc = controller.GetDocument();

            Assert.IsNull(doc);
        }

        [TestMethod]
        public void TestGetImage()
        {
            MainController controller = new MainController("testdoc.pdf");
            PdfToImageConverter converter = new PdfToImageConverter();
            PdfDocument doc = controller.GetDocument();

            Image img = converter.PdfToImageByPage(0, doc);

            Assert.IsNull(img);
        }

        [TestMethod]
        public void TestGetP()
        {
            PdfTextExtractor extractor = new PdfTextExtractor();

            HtmlGenericControl p = extractor.GetP(25, 50, 12.5, "test");

            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void TestGetP2()
        {
            PdfTextExtractor extractor = new PdfTextExtractor();

            HtmlGenericControl p = extractor.GetP(25, 50, 12.5, "test");

            Assert.AreEqual(p.InnerHtml, "test");
        }
    }
}


