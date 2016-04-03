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
        public XmlBundleHandler XMLBundles { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Bundle = new BundleHandler(@"J:\Serien");
            this.XMLBundles = new XmlBundleHandler(@"C:\Users\"+Environment.UserName+@"\Google Drive\BananaBundle");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.localBundle.ItemsSource = this.Bundle.GetTree();
        }

        private void localBundle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.txt1.Text = this.Bundle.GetElementById(((TreeViewItem)this.localBundle.SelectedItem).Tag.ToString()).Id;
        }

        private void ShouldCompare_Click(object sender, RoutedEventArgs e)
        {
            this.cbb_compareableUsers.IsEnabled = this.ShouldCompare.IsChecked == true;
            this.cbb_compareableUsers.ItemsSource = this.XMLBundles.XMLBundles.Select(x => x.Path);
            this.cbb_compareableUsers.SelectedIndex = 0;
            if (this.ShouldCompare.IsChecked == false)
                this.localBundle.ItemsSource = this.Bundle.GetTree();
        }

        private void cbb_compareableUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BundleHandler b = this.Bundle.Compare(this.XMLBundles.GetBundleByName(this.cbb_compareableUsers.SelectedValue.ToString()));
            
        }
    }
}
