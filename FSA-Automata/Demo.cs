using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace FSA_Automata {


    class AutomataString {
        public string Input;
        public string Compare;
        public bool IsEqual;
    }


    class Demo {

        public AutomataString DemoString;


        public void RunDemo() {

            DemoString = new AutomataString();
            DemoString.Input = "Hello";
            DemoString.Compare = "World";
            DemoString.IsEqual = false;

            SaveStringAutomata("..\\..\\..\\Demo\\demo.xml");

            Console.WriteLine();

            LoadStringAutomata("..\\..\\..\\Demo\\demo.xml");

        }


        public void LoadStringAutomata(string filepath) {

            Console.WriteLine("Loading Document from save file");

            XDocument doc = XDocument.Load(filepath);

            //var authors = doc.Descendants("Input");

            DemoString.Input = (from xml2 in doc.Descendants("Input")
                select xml2).FirstOrDefault()?.Value;

            DemoString.Compare = (from xml2 in doc.Descendants("Compare")
                select xml2).FirstOrDefault()?.Value;

            DemoString.IsEqual = Convert.ToBoolean((from xml2 in doc.Descendants("IsEqual")
                select xml2).FirstOrDefault()?.Value);


            Console.WriteLine(DemoString.Input);
            Console.WriteLine(DemoString.Compare);
            Console.WriteLine(DemoString.IsEqual.ToString());

        }






        public void SaveStringAutomata(string filepath) {

            Console.WriteLine("Save file variables as XML");

            XElement FileAutomata =
                new XElement("File",
                    new XElement("Input", DemoString.Input),
                    new XElement("Compare", DemoString.Compare),
                    new XElement("Equal", DemoString.IsEqual)
                );

            FileAutomata.Save("..\\..\\..\\Demo\\demo.xml");
            string str = File.ReadAllText("..\\..\\..\\Demo\\demo.xml");
            Console.WriteLine(str);

        }
    }
}

