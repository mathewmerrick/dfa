using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomataEngine;

namespace AutomataApp {
    class App {
        static void Main(string[] args) {

            AutomataGraph automata = new AutomataGraph();


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
