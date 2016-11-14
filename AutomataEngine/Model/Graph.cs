using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutomataEngine {
    public class AutomataGraph {
        public Dictionary<char, State> States;


        public class State {
            public string Type; // Starting, Accepting, Transition

            // Only two paths, 
            public Dictionary<int, char> Paths;

            // Default state is transition
            public State() {
                Type = "Transition";
                Paths = new Dictionary<int, char>(); //Path A: 0
            }

            public State(string type) {
                Type = type;
                Paths = new Dictionary<int, char>(); //Path A: 0
            }

        }

        // A->B, weight 0
        // target B
        // weight 0


        public char AddPath(char target) {

            if (!States.ContainsKey(target)) {
                States[target] = new State();
            }
            return target;
        }

        public void Save(string filename) {
            XDocument doc = new XDocument(
                new XElement("automata"));

            foreach (char state in States.Keys) {
                doc.Root.Add(new XElement("state", new XAttribute("name", state), new XAttribute("type", States[state].Type)));


            }

            foreach (XElement element in doc.Descendants("state")) {
                char state = Convert.ToChar(element.Attribute("name").Value);

                foreach (int path in States[state].Paths.Keys) {
                    element.Add(new XElement("path", new XAttribute("weight", path), new XAttribute("target", States[state].Paths[path])));
                }
            }

            // When required, change this arg to filename
            //doc.Save("../../../demo.save.xml");
            doc.Save(filename);
        }






        public void Read(string filename) {
            XDocument doc = XDocument.Load(filename);

            foreach (XElement element in doc.Root.Elements()) {

                string name = element.Attribute("name").Value.ToString();



                char stateName = Convert.ToChar(element.Attribute("name").Value);
                string type = element.Attribute("type").Value.ToString();

                State s = new State(type);

                Console.WriteLine($"State: {name} {type}");

                foreach (XElement xel in element.Elements()) {

                    int weight = Convert.ToInt32(xel.Attribute("weight").Value);
                    char target = Convert.ToChar(xel.Attribute("target").Value);


                    Console.WriteLine($"   Path: weight = {weight} target = {target}");

                    s.Paths.Add(weight, target);
                }

                States.Add(stateName, s);
            }

        }

        public string AddState(char stateName, string type) {
            if ((type != "Starting") && (type != "Transition") && (type != "Accepting")) {
                return "Invalid state type";
            }
            
            if (!States.ContainsKey(stateName)) {
                States[stateName] = new State(type);
            }
            else {
                return "State already exists.";
            }
            return "State successfully created.";
        }

        public string AddPath(char stateName, int weight, char target) {

            if (States[stateName] == null) {
                return "State does not exist";
            }

            if (States[stateName].Paths == null) {
                States[stateName].Paths = new Dictionary<int, char>();
            }

            States[stateName].Paths[weight] = target;
            return "Path successfully created";

        }


        public AutomataGraph(string filename) {
   
            States = new Dictionary<char, State>();
            Read(filename);
        }




        public AutomataGraph() {
            States = new Dictionary<char, State>();

            //////////////////////////////////////////
            //if (!States.ContainsKey('A')) {
            //    States['A'] = new State();
            //}
            //States['A'].Type = "Starting";
            //States['A'].Paths[0] ='C';
            //States['A'].Paths[1] = 'B';
            //////////////////////////////////////////
            //if (!States.ContainsKey('B')) {
            //    States['B'] = new State();
            //}
            //States['B'].Type = "Transition";
            //States['B'].Paths[0] = 'A';
            //States['B'].Paths[1] = 'C';
            //////////////////////////////////////////
            //if (!States.ContainsKey('C')) {
            //    States['C'] = new State();
            //}
            //States['C'].Type = "Accepting";
        }

        public bool TestString(string word) {
            char accepting = '\0';
            foreach (char state in States.Keys) {
                if (States[state].Type == "Starting") {
                    accepting = state;
                }
            }
            return TryString(word, accepting);
        }


        private bool TryString(string word, char currentState) {
            if (word.Length == 0)  {
                if (States[currentState].Type == "Accepting") {
                    return true;
                }
                return false;
            }

            int letter = word[0] - 48;
            if (States[currentState].Paths.ContainsKey(letter)) {
                string subWord = word.Substring(1);
                char target = States[currentState].Paths[letter];
                return TryString(subWord, target);

            }
            return false;
        }


    }
}
