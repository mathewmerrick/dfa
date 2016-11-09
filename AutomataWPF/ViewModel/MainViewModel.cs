using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomataEngine;

namespace AutomataApp.ViewModel {
    class MainViewModel {

        // This is the main datasource for the View
        // Event handlers that are fired from the view can call these methods, 
        // which in turn can interact with the model(s)


        private AutomataGraph _automata;

        public MainViewModel() {
            _automata = new AutomataGraph();
        }

        public void load(string filename) {
            _automata.Read(filename);
        }


    }
}
