using System;
using System.Collections.Generic;
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


        private MainViewModel VM;

        public MainWindow() {
            InitializeComponent();
            VM = new MainViewModel();
            DataContext = VM;
        }


        private void MenuItemOpen_OnClick(object sender, RoutedEventArgs e) {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Automata|*.xml";

            try {
                open.ShowDialog();
                string filename = open.FileName;
                VM.load(filename);

                MessageBoxButton button = MessageBoxButton.OK;
                var result = MessageBox.Show("Loaded Successfully", "Success", button);

            }
            catch {
                MessageBoxButton button = MessageBoxButton.OK;
                var result = MessageBox.Show("Error in loading file", "Error", button);
            }

        }

        public void AddState(object sender, RoutedEventArgs e){

            Rectangle state = new Rectangle
            {
                Width = 100,
                Height = 100,
                StrokeThickness = 3,
                Stroke = new SolidColorBrush(Colors.White),
                Margin = new Thickness(10)
            };

            TextBlock stateName = new TextBlock {
                Text = "Name: " + textBox.Text + "\n" + 
                                        "--------\n" + 
                                        "Paths: \n" +
                                        "1 -> A\n" +
                                        "{weight} -> {state}",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                
                FontSize = 11,
                Foreground = new SolidColorBrush(Colors.White),
            };



            Grid stateWrapper = new Grid();
            stateWrapper.Children.Add(state);
            stateWrapper.Children.Add(stateName);

            AutomataPanel.Children.Add(stateWrapper);

        }

        public void DeleteState(object sender, RoutedEventArgs e){
            
            // Get # of children in the panel
            int count = AutomataPanel.Children.Count;

            // remove the child that was most recently added (like a stack)
            if ( count > 0){
                AutomataPanel.Children.RemoveAt(count - 1);
            }

        }


        // Clear a textbox on first click
        public void TextBox_GotFocus(object sender, RoutedEventArgs e){
            TextBox box = sender as TextBox;

            box.Text = string.Empty;

            // Unsubscribe from this event handler, so the cell isn't reset every time it's clicked on
            box.GotFocus -= TextBox_GotFocus;
        }




    }
}
