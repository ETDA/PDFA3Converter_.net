using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFALib.Controller
{
    class ParameterController
    {
        public ParameterController(string[] args)
        {
            
            Dictionary<string, string> values = new Dictionary<string, string>();
            Console.WriteLine("\tParameter list:");
            for (int i = 0; i < args.Length; i += 2)
            {
                Console.WriteLine("\t\t" + args[i].ToString().Trim() + ": " + args[i + 1].ToString().Trim());
                values.Add(args[i], args[i + 1]);
            }

            PDFA3Converter converter = new PDFA3Converter();

            if (!values.ContainsKey("-inputFile") || !values.ContainsKey("-outputFile"))
            {
                throw new Exception("Required parameter is missing");
            }
            else
            {
                converter._inputFile = values["-inputFile"];
                converter._outputFile = values["-outputFile"];
                if (!values.ContainsKey("-colorProfile"))
                {
                    converter._iccproFile = null;
                }
                else
                {
                    converter._iccproFile = values["-colorProfile"];
                }
                if (!values.ContainsKey("-embedFile"))
                {
                    converter._xmlFile = null;
                }
                else
                {
                    converter._xmlFile = values["-embedFile"];
                }
                if (!values.ContainsKey("-font"))
                {
                    converter._font = null;
                }
                else
                {
                    converter._font = values["-font"];
                }
                if (!values.ContainsKey("-metaData"))
                {
                    converter._metaDataFile = null;
                }
                else
                {
                    converter._metaDataFile = values["-metaData"];
                }

                if (!values.ContainsKey("-embedType"))
                {
                    converter._embedType = Model.EmbedType.ADD;
                }else if (values["-embedType"] == "REPLACE")
                {
                    converter._embedType = Model.EmbedType.REPLACE;
                }
                else if (values["-embedType"] == "ADD")
                {
                    converter._embedType = Model.EmbedType.ADD;
                }
                else
                {
                    throw new Exception("Unrecognized Embed type");
                }

                if (!values.ContainsKey("-outputName"))
                {
                    converter._outputName = null;
                }
                else
                {
                    converter._outputName = values["-outputName"];
                }

                converter.Convert();
            }

        }
    }
}
