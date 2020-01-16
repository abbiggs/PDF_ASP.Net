using System;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pdf_In_Browser_1;
using PDFiumSharp;

namespace PdfiumBackTest {

    [TestClass]
    public class TestConverter {

        [TestMethod]
        public void TestFindingPdf() {

            //Find pdf doc
            //Set to pdfdocument variable
            //create instance of PdfToImageConverter
            //Convert pdfdocument to image
            //assert that returned object is an image datatype
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            PdfDocument document;

            path = path + @"\TestDocuments\testdoc.pdf";
            document = new PdfDocument(path);

            Assert.IsNotNull(document);
        }

        [TestMethod]
        public void TestConvertingToImage() {

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            PdfToImageConverter converter = new PdfToImageConverter();
            PdfDocument document;
            Image result;

            path = path + @"\TestDocuments\testdoc.pdf";
            document = new PdfDocument(path);

            result = converter.pdfToImageByPage(0, document);

            Assert.IsNotNull(result);
        }
    }
}


