using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AutomataEngine;

namespace AutomataApp.ViewModel {
    class MainViewModel {

        // This is the main datasource for the View
        // Event handlers that are fired from the view can call these methods, 
        // which in turn can interact with the model(s)

        // Maintain a dictionary of states, corresponding with items in the stack panel


        public event PropertyChangedEventHandler AutomataChanged;


        private AutomataGraph _automata;

        public MainViewModel() {
            _automata = new AutomataGraph();
            _automata.StateChanged += AutomataChanged;
            StateTypes = new [] { "Starting", "Transition", "Accepting" };
        }



        private string[] _stateTypes;
        public string[] StateTypes {
            get { return _stateTypes; }
            set { _stateTypes = value; }
        }



        public void Load(string filename) {
            _automata = new AutomataGraph();
            _automata.Read(filename);
            foreach (KeyValuePair<char, AutomataGraph.State> entry in _automata.States) {
                AutomataChanged?.Invoke(entry, new PropertyChangedEventArgs("State"));
            }
        }


        public void Save (string filename) {
            _automata.Save(filename);
        }

        public void DeleteState(char stateName) {
            _automata.DeleteState(stateName);
        }

        public void DeletePath(char startState, int weight, char targetState) {
            
        }

        public bool addState(char name, string type) {
            return _automata.AddState(name, type);
        }

        public void addPath(char startState, int weight, char targetState) {
            _automata.AddPath(startState, weight, targetState);
            var stateNameAndClass = new KeyValuePair<char, AutomataGraph.State>(startState, _automata.States[startState]);
            AutomataChanged?.Invoke( stateNameAndClass, new PropertyChangedEventArgs("State"));
        }

        public bool Evaluate(string eval) {
            return _automata.TestString(eval);
        }

    }
}
