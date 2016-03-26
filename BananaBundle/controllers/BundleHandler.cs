using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BananaBundle.models;

namespace BananaBundle.controllers
{
    class BundleHandler
    {
        public List<Serie> Series { get; private set; }

        public string Path { get; set; }

        public BundleHandler(string directory)
        {
            this.Series = new List<Serie>();
            if (Directory.Exists(@directory))
            {
                this.Path = @directory;
                foreach (string serieFolder in Directory.GetDirectories(this.Path))
                {
                    this.Series.Add(new Serie(serieFolder));
                }
            }
        }
    }
}
