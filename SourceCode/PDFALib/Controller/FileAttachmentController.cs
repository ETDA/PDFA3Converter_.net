using iTextSharp.text.pdf;
using PDFALib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PDFALib.Controller
{
    class FileAttachmentController
    {

        public void AttachFile(PdfReader reader, PdfStamper stamper, string xmlFile, EmbedType embedType)
        {
            //Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();

            var catalog = reader.Catalog;
            //PdfDictionary names = catalog.GetAsDict(PdfName.NAMES);

            PdfArray af = catalog.GetAsArray(PdfName.AF);
            //if (af != null)
            //{
            //    PdfDictionary filespec = af.GetAsDict(0);
            //    if (filespec != null)
            //    {
            //        int x = filespec.Size;

            //    }
            //}
            ////var oldFileSpecList = new List<PdfFileSpecification>();
            //if (names != null)
            //{
            //    PdfDictionary EmbeddedFile = names.GetAsDict(PdfName.EMBEDDEDFILES);
            //    if (EmbeddedFile != null)
            //    {
            //        PdfArray fileSpecs = EmbeddedFile.GetAsArray(PdfName.NAMES);
            //        if (fileSpecs != null)
            //        {
            //            int eFLength = fileSpecs.Size;

            //            for (int i = 0; i < eFLength; i++)
            //            {
            //                i++; //objects are in pairs and only want odd objects (1,3,5...)
            //                PdfDictionary fileSpec = fileSpecs.GetAsDict(i); // may be null
            //                if (fileSpec != null)
            //                {
            //                    PdfDictionary refs = fileSpec.GetAsDict(PdfName.EF);
            //                    foreach (PdfName key in refs.Keys)
            //                    {
            //                        PRStream stream = (PRStream)PdfReader.GetPdfObject(refs.GetAsIndirectObject(key));

            //                        if (stream != null)
            //                        {
            //                            var a = fileSpec.GetAsString(key).ToString();
            //                            files.Add(fileSpec.GetAsString(key).ToString().Replace(".xml", string.Format("[{0}].xml", key)), PdfReader.GetStreamBytes(stream));

            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            // ---------------- Attach file ----------------
            string filename = Path.GetFileName(xmlFile);
            iTextSharp.text.pdf.PdfDictionary parameters = new iTextSharp.text.pdf.PdfDictionary();
            parameters.Put(iTextSharp.text.pdf.PdfName.MODDATE, new iTextSharp.text.pdf.PdfDate());
            PdfFileSpecification pfs = PdfFileSpecification.FileEmbedded(stamper.Writer, xmlFile, filename, null, "text/xml", parameters,0);
            pfs.Put(new iTextSharp.text.pdf.PdfName("AFRelationship"), new iTextSharp.text.pdf.PdfName("Alternative"));
            pfs.Put(new PdfName("F"), new PdfString(filename));
            pfs.Put(new PdfName("UF"), new PdfString(filename));
            stamper.Writer.AddFileAttachment(filename, pfs);
            iTextSharp.text.pdf.PdfArray array = new iTextSharp.text.pdf.PdfArray();
            array.Add(pfs.Reference);
            if (af != null)
            {
                af.Add(pfs.Reference);
            }
            else
            {
                stamper.Writer.ExtraCatalog.Put(new iTextSharp.text.pdf.PdfName("AF"), array);
            }
            
        }
    }
}















//string name = Path.GetFileNameWithoutExtension(xmlFile);
//// ---------------- Attach file ----------------
//string filename = Path.GetFileName(xmlFile);
//iTextSharp.text.pdf.PdfDictionary parameters = new iTextSharp.text.pdf.PdfDictionary();
//parameters.Put(iTextSharp.text.pdf.PdfName.MODDATE, new iTextSharp.text.pdf.PdfDate());
//PdfFileSpecification pfs = PdfFileSpecification.FileEmbedded(stamper.Writer, xmlFile, filename, null, "application/xml", parameters, 0);
//pfs.Put(new iTextSharp.text.pdf.PdfName("AFRelationship"), new iTextSharp.text.pdf.PdfName("Data"));
////pfs.Put()
//stamper.Writer.AddFileAttachment(filename, pfs);
//iTextSharp.text.pdf.PdfArray array = new iTextSharp.text.pdf.PdfArray();
//array.Add(pfs.Reference);
//stamper.Writer.ExtraCatalog.Put(new iTextSharp.text.pdf.PdfName("AF"), array);