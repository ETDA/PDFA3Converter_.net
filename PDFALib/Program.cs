using iTextSharp.text.pdf;
using PDFALib.Controller;
using PDFALib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFALib
{
    class Program
    {
        static void Main(string[] args)
        {
            runWithExternalInput(args);
        }

        public static void runWithExternalInput(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    throw new Exception("Parameter cannot be blank");
                }
                else
                {
                    new ParameterController(args);
                    Console.WriteLine("Complete");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
            
        }
    }
}













