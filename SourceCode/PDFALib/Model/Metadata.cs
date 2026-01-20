using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;

namespace PDFALib.Model
{
    class Metadata
    {
        //private string _Title { get; set; }
        //private string _Author { get; set; }
        //private string _Subject { get; set; }
        //private string _Keywords { get; set; }
        //private string _Creator { get; set; }

        public string _Title { get; set; }
        public string _Author { get; set; }
        public string _Subject { get; set; }
        public string _Keywords { get; set; }
        public string _Creator { get; set; }


        public Metadata()
        {

        }
        //public Metadata(string title, string author, string subject, string keywords, string creator)
        //{
        //    this._Title = title;
        //    this. _Author = author;
        //    this._Subject = subject;
        //    this._Keywords = keywords;
        //    this._Creator = creator;
        //}
        
    }
}





//public void getMetadata(PdfReader reader,Metadata metadata)
//{
//    try
//    {
//        if (metadata._Title != null)
//        {
//            this._Title = metadata._Title;
//        }
//        else
//        {
//            if (reader.Info.ContainsKey("Title"))
//            {
//                this._Title = reader.Info["Title"];
//            }
//        }
//        if (metadata._Author != null)
//        {
//            this._Author = metadata._Author;
//        }
//        else
//        {
//            if (reader.Info.ContainsKey("Author"))
//            {
//                this._Author = reader.Info["Author"];
//            }
//        }
//        if (metadata._Subject != null)
//        {
//            this._Subject = metadata._Subject;
//        }
//        else
//        {
//            if (reader.Info.ContainsKey("Subject"))
//            {
//                this._Subject = reader.Info["Subject"];
//            }
//        }
//        if (metadata._Keywords != null)
//        {
//            this._Keywords = metadata._Keywords;
//        }
//        else
//        {
//            if (reader.Info.ContainsKey("Keywords"))
//            {
//                this._Keywords = reader.Info["Keywords"];
//            }
//        }
//        if (metadata._Creator != null)
//        {
//            this._Creator = metadata._Creator;
//        }
//        else
//        {
//            if (reader.Info.ContainsKey("Creator"))
//            {
//                this._Creator = reader.Info["Creator"];
//            }
//        }

//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }
//}

//public void getMetadata(PdfReader reader, string metadataFile)
//{
//    try
//    {
//        StreamReader r = File.OpenText(metadataFile);
//        JsonTextReader jsonreader = new JsonTextReader(r);
//        JObject o2 = (JObject)JToken.ReadFrom(jsonreader);

//        if (!o2.ContainsKey("Title"))
//        {
//            if (reader.Info.ContainsKey("Title"))
//            {
//                this._Title = reader.Info["Title"];
//            }
//        }
//        else
//        {
//            this._Title = (string)o2["Title"];
//        }
//        if (!o2.ContainsKey("Author"))
//        {
//            if (reader.Info.ContainsKey("Author"))
//            {
//                this._Author = reader.Info["Author"];
//            }

//        }
//        else
//        {
//            this._Author = (string)o2["Author"];
//        }
//        if (!o2.ContainsKey("Subject"))
//        {
//            if (reader.Info.ContainsKey("Subject"))
//            {
//                this._Subject = reader.Info["Subject"];
//            }

//        }
//        else
//        {
//            this._Subject = (string)o2["Subject"];
//        }
//        if (!o2.ContainsKey("Keywords"))
//        {
//            if (reader.Info.ContainsKey("Keywords"))
//            {
//                this._Keywords = reader.Info["Keywords"];
//            }

//        }
//        else
//        {
//            this._Keywords = (string)o2["Keywords"];
//        }
//        if (!o2.ContainsKey("Creator"))
//        {
//            if (reader.Info.ContainsKey("Creator"))
//            {
//                this._Creator = reader.Info["Creator"];
//            }

//        }
//        else
//        {
//            this._Creator = (string)o2["Creator"];
//        }

//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }
//}

//public void getMetadata(PdfReader reader)
//{
//    try
//    {
//        if (!reader.Info.ContainsKey("Title"))
//        {
//            this._Title = "";
//        }
//        else
//        {
//            this._Title = reader.Info["Title"];

//        }
//        if (!reader.Info.ContainsKey("Author"))
//        {
//            this._Author = "";
//        }
//        else
//        {
//            this._Author = reader.Info["Author"];

//        }
//        if (!reader.Info.ContainsKey("Subject"))
//        {
//            this._Subject = "";
//        }
//        else
//        {
//            this._Subject = reader.Info["Subject"];
//        }
//        if (!reader.Info.ContainsKey("Keywords"))
//        {
//            this._Keywords = "";
//        }
//        else
//        {
//            this._Keywords = reader.Info["Keywords"];
//        }
//        if (!reader.Info.ContainsKey("Creator"))
//        {
//            this._Creator = "";
//        }
//        else
//        {
//            this._Creator = reader.Info["Creator"];
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }
//}

//public string getXMP()
//{
//    return
//            " <?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>"
//            + "<x:xmpmeta xmlns:x=\"adobe: ns: meta / \">"
//                + "<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">"
//                + "<rdf:Description xmlns:dc=\"http://purl.org/dc/elements/1.1/\" rdf:about=\"\">"
//                 + "<dc:title>"
//                   + "<rdf:Alt>"
//                      + string.Format("<rdf:li xml:lang=\"x-default\">{0}</rdf:li>", _Title)
//                   + "</rdf:Alt>"
//                 + "</dc:title>"
//                 + "<dc:creator>"
//                   + "<rdf:Seq>"
//                      + string.Format("<rdf:li>{0}</rdf:li>", _Author)
//                    + "</rdf:Seq>"
//                 + "</dc:creator>"
//                 + "<dc:description>"
//                    + "<rdf:Alt>"
//                       + string.Format("<rdf:li xml:lang=\"x-default\">{0}</rdf:li>", _Subject)
//                    + "</rdf:Alt>"
//                 + "</dc:description>"
//                 + "<dc:subject>"  
//                    + "<rdf:Alt>"
//                       + string.Format("<rdf:li>{0}</rdf:li>", _Keywords)
//                    + "</rdf:Alt>"
//                 + "</dc:subject>"
//                + "</rdf:Description>"
//                +string.Format("<rdf:Description xmlns:xmp=\"http://ns.adobe.com/xap/1.0/\" rdf:about=\"\" xmp:CreatorTool=\"{0}\"/>", _Creator)// Set Creator
//                //+ "<rdf:Description xmlns:pdf=\"http://ns.adobe.com/pdf/1.3/\" rdf:about=\"\" pdf:Producer=\"\"/>" // Can't Set Producer 
//                + "<rdf:Description xmlns:pdfaid=\"http://www.aiim.org/pdfa/ns/id/\" rdf:about=\"\" pdfaid:part=\"3\" pdfaid:conformance=\"U\"/>"
//              + "</rdf:RDF>"
//            + "</x:xmpmeta>"
//            + "<?xpacket end='w'?>"
//            ;
//}