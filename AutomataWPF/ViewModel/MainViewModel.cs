using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomataEngine;

namespace AutomataApp.ViewModel {
    class MainViewModel {

        private AutomataGraph _automata;

        public MainViewModel() {
            _automata = new AutomataGraph();
        }

        public void load(string filename) {
            _automata.Read(filename);
        }


    }
}
