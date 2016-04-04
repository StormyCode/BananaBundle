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
            //this.Bundle = new BundleHandler(XDocument.Load("test_structure.xml"));
            this.XMLBundles = new XmlBundleHandler(@"C:\Users\" + Environment.UserName + @"\Google Drive\BananaBundle");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.localBundle.ItemsSource = this.Bundle.GetTree();
        }

        private void localBundle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.tb_infobox.Text = this.Bundle.GetElementById(((TreeViewItem)this.localBundle.SelectedItem).Tag.ToString()).GetInfo();
        }

        private void ShouldCompare_Click(object sender, RoutedEventArgs e)
        {
            this.cbb_compareableUsers.IsEnabled = this.ShouldCompare.IsChecked == true;
            this.cbb_compareableUsers.ItemsSource = this.XMLBundles.XMLBundles.Where(x=> x.Path != Environment.UserName).Select(x => x.Path);
            this.cbb_compareableUsers.SelectedIndex = 0;
            if (this.ShouldCompare.IsChecked == false)
            {
                this.localBundle.ItemsSource = this.Bundle.GetTree();
                this.cbb_compareableUsers.SelectedIndex = -1;
            }
        }

        private void cbb_compareableUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ShouldCompare.IsChecked == true)
                this.localBundle.ItemsSource = this.Bundle.Compare(this.XMLBundles.GetBundleByName(this.cbb_compareableUsers.SelectedValue.ToString())).GetTree();
        }
    }
}
