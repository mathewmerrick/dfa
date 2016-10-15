using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
                Paths =  new Dictionary<int, char>(); //Path A: 0
            }

        }

        // A->B, weight 0
            // target B
            // weight 0
            
        public class Path {
            public int Weight; 
            public char Target;

            public Path(int weight, char target) {
                Weight = weight;
                Target = target;
            }

        }

        public char AddPath(char target) {

            if (!States.ContainsKey(target)) {
                States[target] = new State();
            }
            return target;
        }


        public AutomataGraph() {
            States = new Dictionary<char, State>();

            ////////////////////////////////////////
            if (!States.ContainsKey('A')) {
                States['A'] = new State();
            }
            States['A'].Type = "Starting";
            States['A'].Paths[0] ='C';
            States['A'].Paths[1] = 'B';
            ////////////////////////////////////////
            if (!States.ContainsKey('B')) {
                States['B'] = new State();
            }
            States['B'].Type = "Transition";
            States['B'].Paths[0] = 'A';
            States['B'].Paths[1] = 'C';
            ////////////////////////////////////////
            if (!States.ContainsKey('C')) {
                States['C'] = new State();
            }
            States['C'].Type = "Accepting";
        }

        public bool TestString(string word) {

            return TryString(word, 'A');
        }


        private bool TryString(string word, char currentState) {
            if (word.Length == 0 && States[currentState].Type == "Accepting") {
                return true;
            }
            if (word.Length == 0) {
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
