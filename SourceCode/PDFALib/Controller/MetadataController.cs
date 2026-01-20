using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDFALib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PDFALib.Controller
{
    class MetadataController
    {
        Metadata modelMetadata = new Metadata();

        public MetadataController()
        {
            
        }
        

        public void getMetadata(PdfReader reader, Metadata metadata)
        {
            
            try
            {
                if (metadata._Title != null)
                {
                    modelMetadata._Title = metadata._Title;
                }
                else
                {
                    if (reader.Info.ContainsKey("Title"))
                    {
                        modelMetadata._Title = reader.Info["Title"];
                    }
                }
                if (metadata._Author != null)
                {
                    modelMetadata._Author = metadata._Author;
                }
                else
                {
                    if (reader.Info.ContainsKey("Author"))
                    {
                        modelMetadata._Author = reader.Info["Author"];
                    }
                }
                if (metadata._Subject != null)
                {
                    modelMetadata._Subject = metadata._Subject;
                }
                else
                {
                    if (reader.Info.ContainsKey("Subject"))
                    {
                        modelMetadata._Subject = reader.Info["Subject"];
                    }
                }
                if (metadata._Keywords != null)
                {
                    modelMetadata._Keywords = metadata._Keywords;
                }
                else
                {
                    if (reader.Info.ContainsKey("Keywords"))
                    {
                        modelMetadata._Keywords = reader.Info["Keywords"];
                    }
                }
                if (metadata._Creator != null)
                {
                    modelMetadata._Creator = metadata._Creator;
                }
                else
                {
                    if (reader.Info.ContainsKey("Creator"))
                    {
                        modelMetadata._Creator = reader.Info["Creator"];
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void getMetadata(PdfReader reader, string metadataFile)
        {
            try
            {
                StreamReader r = File.OpenText(metadataFile);
                JsonTextReader jsonreader = new JsonTextReader(r);
                JObject o2 = (JObject)JToken.ReadFrom(jsonreader);

                if (!o2.ContainsKey("Title"))
                {
                    if (reader.Info.ContainsKey("Title"))
                    {
                        modelMetadata._Title = reader.Info["Title"];
                    }
                }
                else
                {
                    modelMetadata._Title = (string)o2["Title"];
                }
                if (!o2.ContainsKey("Author"))
                {
                    if (reader.Info.ContainsKey("Author"))
                    {
                        modelMetadata._Author = reader.Info["Author"];
                    }

                }
                else
                {
                    modelMetadata._Author = (string)o2["Author"];
                }
                if (!o2.ContainsKey("Subject"))
                {
                    if (reader.Info.ContainsKey("Subject"))
                    {
                        modelMetadata._Subject = reader.Info["Subject"];
                    }

                }
                else
                {
                    modelMetadata._Subject = (string)o2["Subject"];
                }
                if (!o2.ContainsKey("Keywords"))
                {
                    if (reader.Info.ContainsKey("Keywords"))
                    {
                        modelMetadata._Keywords = reader.Info["Keywords"];
                    }

                }
                else
                {
                    modelMetadata._Keywords = (string)o2["Keywords"];
                }
                if (!o2.ContainsKey("Creator"))
                {
                    if (reader.Info.ContainsKey("Creator"))
                    {
                        modelMetadata._Creator = reader.Info["Creator"];
                    }

                }
                else
                {
                    modelMetadata._Creator = (string)o2["Creator"];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void getMetadata(PdfReader reader)
        {
            try
            {
                if (!reader.Info.ContainsKey("Title"))
                {
                    modelMetadata._Title = "";
                }
                else
                {
                    modelMetadata._Title = reader.Info["Title"];

                }
                if (!reader.Info.ContainsKey("Author"))
                {
                    modelMetadata._Author = "";
                }
                else
                {
                    modelMetadata._Author = reader.Info["Author"];

                }
                if (!reader.Info.ContainsKey("Subject"))
                {
                    modelMetadata._Subject = "";
                }
                else
                {
                    modelMetadata._Subject = reader.Info["Subject"];
                }
                if (!reader.Info.ContainsKey("Keywords"))
                {
                    modelMetadata._Keywords = "";
                }
                else
                {
                    modelMetadata._Keywords = reader.Info["Keywords"];
                }
                if (!reader.Info.ContainsKey("Creator"))
                {
                    modelMetadata._Creator = "";
                }
                else
                {
                    modelMetadata._Creator = reader.Info["Creator"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string getXMP()
        {
            return
                    " <?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>"
                    + "<x:xmpmeta xmlns:x=\"adobe: ns: meta / \">"
                        + "<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">"
                        + "<rdf:Description xmlns:dc=\"http://purl.org/dc/elements/1.1/\" rdf:about=\"\">"
                         + "<dc:title>"
                           + "<rdf:Alt>"
                              + string.Format("<rdf:li xml:lang=\"x-default\">{0}</rdf:li>", modelMetadata._Title)
                           + "</rdf:Alt>"
                         + "</dc:title>"
                         + "<dc:creator>"
                           + "<rdf:Seq>"
                              + string.Format("<rdf:li>{0}</rdf:li>", modelMetadata._Author)
                            + "</rdf:Seq>"
                         + "</dc:creator>"
                         + "<dc:description>"
                            + "<rdf:Alt>"
                               + string.Format("<rdf:li xml:lang=\"x-default\">{0}</rdf:li>", modelMetadata._Subject)
                            + "</rdf:Alt>"
                         + "</dc:description>"
                         + "<dc:subject>"
                            + "<rdf:Alt>"
                               + string.Format("<rdf:li>{0}</rdf:li>", modelMetadata._Keywords)
                            + "</rdf:Alt>"
                         + "</dc:subject>"
                        + "</rdf:Description>"
                        + string.Format("<rdf:Description xmlns:xmp=\"http://ns.adobe.com/xap/1.0/\" rdf:about=\"\" xmp:CreatorTool=\"{0}\"/>", modelMetadata._Creator)// Set Creator
                                                                                                                                                         //+ "<rdf:Description xmlns:pdf=\"http://ns.adobe.com/pdf/1.3/\" rdf:about=\"\" pdf:Producer=\"\"/>" // Can't Set Producer 
                        + "<rdf:Description xmlns:pdfaid=\"http://www.aiim.org/pdfa/ns/id/\" rdf:about=\"\" pdfaid:part=\"3\" pdfaid:conformance=\"U\"/>"
                      + "</rdf:RDF>"
                    + "</x:xmpmeta>"
                    + "<?xpacket end='w'?>"
                    ;
        }

    }
}








//public MetadataController(PdfReader reader , string metadataFile)
//{
//    getMetadatafromjson(reader, metadataFile);
//}

//public void getMetadatafromjson(PdfReader reader,string metadataFile)
//{
//    string title = "";
//    string author = "";
//    string subject = "";
//    string keywords = "";
//    string creator = "";
//    if (metadataFile != null)
//    {

//        StreamReader r = File.OpenText(metadataFile);
//        JsonTextReader jsonreader = new JsonTextReader(r);
//        JObject o2 = (JObject)JToken.ReadFrom(jsonreader);

//        Metadata metadata = new Metadata();
//        if (!o2.ContainsKey("Title"))
//        {
//            metadata._Title = reader.Info["Title"];
//        }
//        else
//        {
//            metadata._Title = (string)o2["Title"];
//        }
//        if (!o2.ContainsKey("Author"))
//        {
//            metadata._Author = reader.Info["Author"];
//        }
//        else
//        {
//            metadata._Author = (string)o2["Author"];
//        }
//        if (!o2.ContainsKey("Subject"))
//        {
//            metadata._Subject = reader.Info["Subject"];
//        }
//        else
//        {
//            metadata._Subject = (string)o2["Subject"];
//        }
//        if (!o2.ContainsKey("Keywords"))
//        {
//            metadata._Keywords = reader.Info["Keywords"];
//        }
//        else
//        {
//            metadata._Keywords = (string)o2["Keywords"];
//        }
//        if (!o2.ContainsKey("Creator"))
//        {
//            metadata._Creator = reader.Info["Creator"];
//        }
//        else
//        {
//            metadata._Creator = (string)o2["Creator"];
//        }

//        //new Metadata
//        //    (
//        //        title,
//        //        author,
//        //        subject,
//        //        keywords,
//        //        creator


//        //        //!o2.ContainsKey("Title") ? reader.Info["Title"] : (string)o2["Title"],
//        //        //!o2.ContainsKey("Author") ? reader.Info["Author"] : (string)o2["Author"],
//        //        //!o2.ContainsKey("Subject") ? reader.Info["Subject"] : (string)o2["Subject"],
//        //        //!o2.ContainsKey("Keywords") ? reader.Info["Keywords"] : (string)o2["Keywords"],
//        //        //!o2.ContainsKey("Creator") ? reader.Info["Creator"] : (string)o2["Creator"]
//        //    );
//        //foreach (var item in o2)
//        //{
//        //    Console.WriteLine("{0} {1}", item.Key, item.Value);
//        //}
//    }
//    else
//    {

//    }
//}
