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
using System.Xml.Linq;
using BananaBundle.controllers;

namespace BananaBundle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BundleHandler Bundle { get; set; }
        public BundleHandler xmlBundle { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //this.Bundle = new BundleHandler(@"C:\Users\Julian\Desktop\Serien");
            this.Bundle = new BundleHandler(@"J:\Serien");
            this.xmlBundle = new BundleHandler(XDocument.Load("test_structure.xml"));
            // this.Bundle = Bundle.Compare(this.xmlBundle);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.localBundle.ItemsSource = this.Bundle.GetTree();
        }

        private void localBundle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.txt1.Text = this.Bundle.GetElementById(((TreeViewItem)this.localBundle.SelectedItem).Tag.ToString()).Id;
        }

        private void ShouldCompare_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void ShouldCompare_Click(object sender, RoutedEventArgs e)
        {
            this.cbb_compareableUsers.IsEnabled = this.ShouldCompare.IsChecked == true;
        }
    }
}
