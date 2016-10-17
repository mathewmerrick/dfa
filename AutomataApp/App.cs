using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomataEngine;

namespace AutomataApp {
    class App {
        static void Main(string[] args) {

            AutomataGraph automata = new AutomataGraph("../../../demo.xml");


            // Hardcoded Example:

            // XML contains states, and a string

            //EX:
            //State A:
            //    Starting
            //    Path: 1, B
            //    Path: 0, C

            //State B:
            //    Transition
            //    Path: 0, A
            //    Path: 1, C

            //State C:
            //    Accepting

            //        Open XML file, connect states with each other

            Console.WriteLine(automata.TestString("1011"));


        }
    }
}


////            States = new Dictionary<char, State>();

//            ////////////////////////////////////////
//            if (!States.ContainsKey('A')) {
//                States['A'] = new State();
//            }
//            States['A'].Type = "Starting";
//            States['A'].Paths[0] ='C';
//            States['A'].Paths[1] = 'B';
//            ////////////////////////////////////////
//            if (!States.ContainsKey('B')) {
//                States['B'] = new State();
//            }
//            States['B'].Type = "Transition";
//            States['B'].Paths[0] = 'A';
//            States['B'].Paths[1] = 'C';
//            ////////////////////////////////////////
//            if (!States.ContainsKey('C')) {
//                States['C'] = new State();
//            }
//            States['C'].Type = "Accepting";
//        }

