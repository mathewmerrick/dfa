using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutomataApp.ViewModel;
using AutomataEngine;
using Microsoft.Win32;


namespace AutomataApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        // Business logic should not be put here, only logic pertaining to the presentation of information

        // A local index for obtaining states within the wrap panel
        // "State A was modified, it's at index 0" 
        private Dictionary<char, int> WrapPanelIndex = new Dictionary<char, int>();

        private MainViewModel VM;

        public MainWindow() {
            InitializeComponent();
            VM = new MainViewModel();
            DataContext = VM;
            VM.AutomataChanged += AutomataChangedEventHandler;

            // Load the available states from A-Z
            for (int i = 0; i < 26; i++) {
                nameComboBox.Items.Add( Convert.ToChar(i+65) );
            }
        }


        private void MenuItemOpen_OnClick(object sender, RoutedEventArgs e) {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Automata|*.xml";

            try {
                open.ShowDialog();
                string filename = open.FileName;
                VM.load(filename);

                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show("Loaded Successfully", "Success", button);

            }
            catch {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show("Error in loading file", "Error", button);
            }

        }

        public void AddStateButton(object sender, RoutedEventArgs e){


            if (WrapPanelIndex.ContainsKey(Convert.ToChar(nameComboBox.Text)) == false) {
                string text = "Name: " + nameComboBox.Text + "\nType: " + typeComboBox.Text + "\nPaths: ";
                VM.addState(Convert.ToChar(nameComboBox.Text), typeComboBox.Text);

                AddRectangle(Convert.ToChar(nameComboBox.Text), text);

                WrapPanelIndex[Convert.ToChar(nameComboBox.Text)] = AutomataWrapPanel.Children.Count - 1;
                UpdateDropDowns();
            }
        }


        public void DeleteStateButton(object sender, RoutedEventArgs e){



            char stateName = Convert.ToChar(nameComboBox.Text);

            if (WrapPanelIndex.ContainsKey(stateName)) {
                AutomataWrapPanel.Children.RemoveAt(WrapPanelIndex[stateName] - 1);
                WrapPanelIndex.Remove(stateName);
                UpdateDropDowns();
            }
            

        }


        // Clear a textbox on first click
        public void TextBox_GotFocus(object sender, RoutedEventArgs e){
            TextBox box = sender as TextBox;

            box.Text = string.Empty;
        }




        // Converts a string of text, which is pre-formatted with state name, 
        // state type and paths to a visual rectangle to add in the box
        private void AddRectangle(char stateName, string text) {

            Rectangle state = new Rectangle {
                Width = 100,
                Height = 100,
                StrokeThickness = 3,
                Stroke = new SolidColorBrush(Colors.White),
                Margin = new Thickness(10)
            };

            TextBlock stateDescription = new TextBlock {
                Text = text,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,

                FontSize = 11,
                Foreground = new SolidColorBrush(Colors.White),
            };

            Grid stateWrapper = new Grid();
            stateWrapper.Name = stateName.ToString();
            stateWrapper.Children.Add(state);
            stateWrapper.Children.Add(stateDescription);
            AutomataWrapPanel.Children.Add(stateWrapper);
        }

        // Event handler for the Add Path button in the view
        private void AddPath_OnClickPath(object sender, RoutedEventArgs e) {

            // Path boxes have nothing selected
            if (pathStartComboBox.Text.Length == 0
                || pathWeightComboBox.Text.Length == 0
                || pathEndComboBox.Text.Length == 0) {
                return;
            }


            bool y = pathStartComboBox.Text.Equals('\0');

            char start = Convert.ToChar(pathStartComboBox.Text);
            int weight = Convert.ToInt32(pathWeightComboBox.Text);
            char end = Convert.ToChar(pathEndComboBox.Text);


            VM.addPath(start, weight, end);
        }



        // Update the dropdowns for the paths, whenever a new state is added
        private void UpdateDropDowns() {
            pathStartComboBox.Items.Clear();
            pathEndComboBox.Items.Clear();
            foreach (char element in WrapPanelIndex.Keys) {
                pathStartComboBox.Items.Add(element);
                pathEndComboBox.Items.Add(element);
            }
        }




        // This function is called (usually in the event handler below) to add a state to the view
        // Formats the appropriate text to the be added to rectangle state in the view,
        // also adds the state name with the appropriate index to the WrapPanelDictionary
        private void AddState(char name, string stateType, Dictionary<int, char> paths) {

            



            string text = "Name: " + name.ToString() + "\n" + "Type: " + stateType + "\n" + "Paths: \n";
            foreach (int element in paths.Keys) {
                text = text + element + "->" + paths[element] + "\n";
            }

            AddRectangle(name, text);

            WrapPanelIndex[name] = AutomataWrapPanel.Children.Count - 1;
            UpdateDropDowns();
        }





        // Event handler for changing events
        private void AutomataChangedEventHandler(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "State") {

                var state = (KeyValuePair<char, AutomataGraph.State>)sender;

                if (WrapPanelIndex.ContainsKey(state.Key)) {
                    //int index = state.Key;
                    AutomataWrapPanel.Children.RemoveAt(WrapPanelIndex[state.Key]);
                }
                

                AddState(state.Key, state.Value.Type, state.Value.Paths);

                int i = 0;
                foreach (Grid element in AutomataWrapPanel.Children) {
                    WrapPanelIndex[Convert.ToChar(element.Name)] = i;
                    i++;
                }



                //WrapPanelIndex[state.Key] = AutomataWrapPanel.Children.Count;

            }
            UpdateDropDowns();
        }

        private void DeletePath_OnClick(object sender, RoutedEventArgs e) {

            
        }
    }

}
