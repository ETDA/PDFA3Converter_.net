using iTextSharp.text.pdf;
using iTextSharp.text.xml.xmp;
using PDFALib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PDFALib.Controller
{
    class PDFA3Converter
    {
        public string _inputFile { get; set; }
        public string _outputFile { get; set; }
        public string _iccproFile { get; set; }
        public string _xmlFile { get; set; }
        public string _font { get; set; }
        public EmbedType _embedType;
        public string _metaDataFile { get;  set; }
        public Metadata _metaData;
        public string _outputName { get; set; }

        public PDFA3Converter()
        {
            
        }

        public PDFA3Converter(string inputFile, string outputFile, string ICCProfile, string xmlFile, string font,EmbedType embedType,Metadata metadata,string outputName)
        {
            this._inputFile = inputFile;
            this._outputFile = outputFile;
            this._iccproFile = ICCProfile;
            this._xmlFile = xmlFile;
            this._font = font;
            this._embedType = embedType;
            this._metaData = metadata;
            this._outputName = outputName;
            Convert();
        }

        public void Convert()
        {
            try
            {
                if (_xmlFile != null)
                {
                    if ((File.GetAttributes(_inputFile) & FileAttributes.Directory) == FileAttributes.Directory && (File.GetAttributes(_outputFile) & FileAttributes.Directory) == FileAttributes.Directory && (File.GetAttributes(_xmlFile) & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        string[] inputbulkFile = Directory.GetFiles(_inputFile, "*.pdf", SearchOption.AllDirectories);
                        string[] inputbulkXML = Directory.GetFiles(_xmlFile, "*.xml", SearchOption.AllDirectories);

                        if (inputbulkFile.Length == inputbulkXML.Length)
                        {
                            for (int i = 0; i < inputbulkFile.Length; i++)
                            {
                                string ibf = Path.GetFileNameWithoutExtension(inputbulkFile[i]);
                                string ibx = Path.GetFileNameWithoutExtension(inputbulkXML[i]);
                                if (ibf.Equals(ibx, StringComparison.OrdinalIgnoreCase))
                                {
                                    ConverttoPDFA(inputbulkFile[i], _outputFile, _iccproFile, inputbulkXML[i], _font, _embedType);
                                    
                                }
                                else
                                {
                                    throw new Exception("PDF and Attachment file must be the same name (Exclude extension)");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("If input PDF is directory, " + "number of attachment file must equal to number of PDF");
                        }
                    }
                    else if(File.Exists(_inputFile) == true && Path.HasExtension(_outputFile) && File.Exists(_xmlFile) == true)
                    {
                        ConverttoPDFA(_inputFile, _outputFile, _iccproFile, _xmlFile, _font, _embedType);
                    }
                    else
                    {
                        throw new Exception("Input parameters must be files or folder all the same");
                    }
                }
                else
                {
                    if (File.GetAttributes(_inputFile) == FileAttributes.Directory && File.GetAttributes(_outputFile) == FileAttributes.Directory)
                    {
                        
                        foreach (string file in Directory.GetFiles(_inputFile, "*.pdf", SearchOption.AllDirectories))
                        {
                            ConverttoPDFA(file, _outputFile, _iccproFile, null, _font, _embedType);
                        }
                    }
                    else if (File.Exists(_inputFile) == true && Path.HasExtension(_outputFile))
                    {
                        ConverttoPDFA(_inputFile, _outputFile, _iccproFile, null, _font, _embedType);
                    }
                    else
                    {
                        throw new Exception("Input parameters must be files or folder all the same");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }

        public void ConverttoPDFA(string inputFile, string outputFile, string colorProfile, string xmlFile, string font,EmbedType embedType)
        {
            try
            {
                string output;
                if (Path.HasExtension(outputFile))
                {
                    output = outputFile;
                }
                else
                {
                    if (_outputName != null)
                    {
                        string outputName = Path.GetFileNameWithoutExtension(inputFile);
                        output = outputFile + outputName + _outputName + ".pdf";
                    }
                    else
                    {
                        string outputName = Path.GetFileName(inputFile);
                        output = outputFile + outputName;
                    }
                    
                }
                //else
                //{
                //    throw new Exception("outputFile path not correct");
                //}
                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(inputFile);
                PdfStamper stamper = new PdfStamper(reader, new FileStream(output, FileMode.Create));

                //----------TEST------------
                //var catalog = reader.Catalog;
                //PdfArray pdfArray = catalog.GetAsArray(PdfName.AF);
                //var fileArray = pdfArray.GetAsDict(0);
                //var x = fileArray.GetAsString(PdfName.F);
                //PdfDictionary pdfDictionary = 
                //PdfNameTree.ReadTree()
                //----------TEST------------


                // ---------------- set XMP ----------------
                MetadataController metadataController = new MetadataController();
                if (_metaDataFile != null)
                {
                    metadataController.getMetadata(reader, _metaDataFile);
                }else if (_metaData != null)
                {
                    metadataController.getMetadata(reader,_metaData);
                }
                else
                {
                    metadataController.getMetadata(reader);
                }
                
                byte[] xmp = Encoding.ASCII.GetBytes(metadataController.getXMP());
                stamper.XmpMetadata = xmp;
               

                // ---------------- set Color Profile ----------------
                if (colorProfile != null)
                {
                    Stream fileStream = new FileStream(colorProfile, FileMode.Open, FileAccess.Read);
                    ICC_Profile icc = ICC_Profile.GetInstance(fileStream);
                    iTextSharp.text.pdf.PdfDictionary outi = new iTextSharp.text.pdf.PdfDictionary(iTextSharp.text.pdf.PdfName.OUTPUTINTENT);
                    outi.Put(iTextSharp.text.pdf.PdfName.OUTPUTCONDITIONIDENTIFIER, new iTextSharp.text.pdf.PdfString("sRGB IEC61966-2.1"));
                    outi.Put(iTextSharp.text.pdf.PdfName.INFO, new iTextSharp.text.pdf.PdfString("sRGB IEC61966-2.1"));
                    outi.Put(iTextSharp.text.pdf.PdfName.S, iTextSharp.text.pdf.PdfName.GTS_PDFA1);
                    PdfICCBased ib = new PdfICCBased(icc);
                    ib.Remove(iTextSharp.text.pdf.PdfName.ALTERNATE);
                    outi.Put(iTextSharp.text.pdf.PdfName.DESTOUTPUTPROFILE, stamper.Writer.AddToBody(ib).IndirectReference);
                    stamper.Writer.ExtraCatalog.Put(iTextSharp.text.pdf.PdfName.OUTPUTINTENTS, new iTextSharp.text.pdf.PdfArray(outi));
                }
                else
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var resourceName = "PDFALib.resources.sRGB_CS_profile.icm";
                    Stream stream = assembly.GetManifestResourceStream(resourceName);

                    ICC_Profile icc = ICC_Profile.GetInstance(stream);
                    iTextSharp.text.pdf.PdfDictionary outi = new iTextSharp.text.pdf.PdfDictionary(iTextSharp.text.pdf.PdfName.OUTPUTINTENT);
                    outi.Put(iTextSharp.text.pdf.PdfName.OUTPUTCONDITIONIDENTIFIER, new iTextSharp.text.pdf.PdfString("sRGB IEC61966-2.1"));
                    outi.Put(iTextSharp.text.pdf.PdfName.INFO, new iTextSharp.text.pdf.PdfString("sRGB IEC61966-2.1"));
                    outi.Put(iTextSharp.text.pdf.PdfName.S, iTextSharp.text.pdf.PdfName.GTS_PDFA1);
                    PdfICCBased ib = new PdfICCBased(icc);
                    ib.Remove(iTextSharp.text.pdf.PdfName.ALTERNATE);
                    outi.Put(iTextSharp.text.pdf.PdfName.DESTOUTPUTPROFILE, stamper.Writer.AddToBody(ib).IndirectReference);
                    stamper.Writer.ExtraCatalog.Put(iTextSharp.text.pdf.PdfName.OUTPUTINTENTS, new iTextSharp.text.pdf.PdfArray(outi));

                }

                //// ---------------- Attach file ----------------
                if (xmlFile != null)
                {
                    try
                    {
                        if (embedType == EmbedType.REPLACE)
                        {
                            PdfDictionary root = reader.Catalog;
                            PdfDictionary names = root.GetAsDict(PdfName.NAMES);
                            PdfArray AF = root.GetAsArray(PdfName.AF);
                            //PdfDictionary EmbeddedFile = names.GetAsDict(PdfName.EMBEDDEDFILES);
                            //PdfArray arraynames = EmbeddedFile.GetAsArray(PdfName.NAMES);
                            if (names != null)
                            {
                                int afsize = AF.Size;
                                for (int i = 0;i<afsize;i++)
                                {
                                    AF.Remove(0);
                                }
                                names.Remove(PdfName.EMBEDDEDFILES);
                            }
                        }
                        FileAttachmentController fileAttachmentController = new FileAttachmentController();
                        fileAttachmentController.AttachFile(reader, stamper, xmlFile, embedType);
                    }   
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                // ---------------- embed font ----------------
                if (font != null)
                {
                    if (Path.HasExtension(font))
                    {
                        PdfContentByte cb = stamper.GetOverContent(1);
                        cb.BeginText();
                        cb.SetFontAndSize(BaseFont.CreateFont(font, BaseFont.WINANSI, BaseFont.EMBEDDED), 12);
                        cb.EndText();
                    }
                    else
                    {
                        foreach (string loopfont in Directory.GetFiles(font, "*.ttf", SearchOption.AllDirectories))
                        {
                            PdfContentByte cb = stamper.GetOverContent(1);
                            cb.BeginText();
                            cb.SetFontAndSize(BaseFont.CreateFont(loopfont, BaseFont.WINANSI, BaseFont.EMBEDDED), 12);
                            //cb.SetFontAndSize(fontBold, 12);
                            //cb.ShowTextAligned(Element.ALIGN_LEFT, "abcdefghijklmnopqrstuvwxyz", 36, 770, 0);
                            cb.EndText();
                        }
                    }
                    
                }
                stamper.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
