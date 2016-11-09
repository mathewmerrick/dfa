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
    }
}
