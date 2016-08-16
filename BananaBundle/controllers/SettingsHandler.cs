using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BananaBundle.controllers
{
    class SettingsHandler
    {
        private static SettingsHandler _instance;
        public static SettingsHandler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SettingsHandler();
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        private string _seriesDirectory;
        public string SeriesDirectory
        {
            get
            {
                if (Directory.Exists(_seriesDirectory) == false)
                    Directory.CreateDirectory(_seriesDirectory);
                return _seriesDirectory;
            }
            private set
            {
                _seriesDirectory = value;
            }
        }

        private string _gDriveDirectory;
        public string GDriveDirectory
        {
            get
            {
                if (Directory.Exists(_gDriveDirectory) == false)
                    Directory.CreateDirectory(_gDriveDirectory);
                return _gDriveDirectory;
            }
            private set
            {
                _gDriveDirectory = value;
            }
        }


        private SettingsHandler()
        {
            this.ReadSettings();
        }

        private void ReadSettings()
        {
            if (File.Exists(@"settings.txt") == false)
            {
                InitSettings form = new InitSettings();
                form.ShowDialog();
                if (form.PathSeries == null || form.PathGDrive == null) return;
                using (StreamWriter sw = new StreamWriter(@"settings.txt"))
                {
                    sw.WriteLine(string.Format("seriesdirectory={0}", form.@PathSeries));
                    sw.WriteLine(string.Format("gdrivedirectory={0}", form.@PathGDrive));
                }
                this.SeriesDirectory = form.PathSeries;
                this.GDriveDirectory = form.PathGDrive;
            }

            using (StreamReader sr = new StreamReader(@"settings.txt"))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (line.Split('=').First().ToLower().Trim())
                    {
                        case "seriesdirectory":
                            string s = @line.Split('=').Last().Trim();
                            this.SeriesDirectory = s;
                            break;
                        case "gdrivedirectory":
                            this.GDriveDirectory = @line.Split('=').Last().Trim();
                            break;
                    }
                }
            }
        }
    }
}
