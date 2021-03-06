﻿using System;
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
using BananaBundle.models;

namespace BananaBundle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BundleHandler Bundle { get; set; }
        public XmlBundleHandler XMLBundles { get; set; }
        public BundleHandler TreeViewSource { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                this.Bundle = new BundleHandler(SettingsHandler.Instance.SeriesDirectory);
                this.TreeViewSource = this.Bundle;
                //this.Bundle = new BundleHandler(XDocument.Load("test_structure.xml"));
                this.XMLBundles = new XmlBundleHandler(SettingsHandler.Instance.GDriveDirectory);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid startup settings");
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.localBundle.ItemsSource = this.TreeViewSource.GetTree();
        }

        private void localBundle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.tb_infobox.Text = ((IBananaObject)((TreeViewItem)this.localBundle.SelectedItem).Tag).GetInfo();
            //this.tb_infobox.Text = this.TreeViewSource.GetElementById(((TreeViewItem)this.localBundle.SelectedItem).Tag.ToString()).GetInfo();
        }

        private void ShouldCompare_Click(object sender, RoutedEventArgs e)
        {
            this.cbb_compareableUsers.IsEnabled = this.ShouldCompare.IsChecked == true;
            this.cbb_compareableUsers.ItemsSource = this.XMLBundles.XMLBundles.Where(x => x.Path != Environment.UserName).Select(x => x.Path);
            this.cbb_compareableUsers.SelectedIndex = 0;
            if (this.ShouldCompare.IsChecked == false)
            {
                this.TreeViewSource = this.Bundle;
                this.localBundle.ItemsSource = this.TreeViewSource.GetTree();
                this.cbb_compareableUsers.SelectedIndex = -1;
            }
        }

        private void cbb_compareableUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ShouldCompare.IsChecked == true)
            {
                this.localBundle.ItemsSource = this.Bundle.Combine(this.Bundle.Compare(this.XMLBundles.GetBundleByName(this.cbb_compareableUsers.SelectedValue.ToString())), Brushes.Red);
            }
        }
    }
}
