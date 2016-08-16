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
using System.Windows.Shapes;

namespace BananaBundle
{
    /// <summary>
    /// Interaction logic for InitSettings.xaml
    /// </summary>
    public partial class InitSettings : Window
    {
        public string PathSeries { get; private set; }
        public string PathGDrive { get; private set; }

        public InitSettings()
        {
            InitializeComponent();
            btn_ok.IsEnabled = false;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            this.PathSeries = this.txtbox_series.@Text;
            this.PathGDrive = this.txtbox_gdrive.@Text;
            this.Close();
        }

        private void txtbox_series_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.IO.Directory.Exists(this.txtbox_series.@Text)
                && System.IO.Directory.Exists(this.txtbox_gdrive.@Text))
            {
                this.btn_ok.IsEnabled = true;
            }
        }

        private void txtbox_gdrive_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.IO.Directory.Exists(this.txtbox_series.@Text)
                && System.IO.Directory.Exists(this.txtbox_gdrive.@Text))
            {
                this.btn_ok.IsEnabled = true;
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.PathSeries = null;
            this.PathGDrive = null;
            this.Close();
        }

        private void btn_gdrive_browse_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
