using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BananaBundle.models
{
    class Serie : IBananaObject
    {
        public string Id
        {
            get
            {
                return Regex.Replace(this.Path, @"[^A-Za-z0-9]", "").ToLower();
            }
            private set;
        }

        public string Name
        {
            get
            {
                return System.IO.Path.GetDirectoryName(this.Path);
            }
            private set;
        }

        public string Path
        {
            get;
            private set;
        }

        public double Size
        {
            get
            {
                return this.Seasons.Select(x => x.Size).Sum();
            }
            private set;
        }

        public List<Season> Seasons { get; private set; }

        public Serie(string directory)
        {
            this.Path = @directory;
            this.Seasons = new List<Season>();
            if(Directory.Exists(@directory))
            {
                foreach (string serieFolder in Directory.GetDirectories(@directory))
                {
                    this.Seasons.Add(new Season(serieFolder));
                }
            }
        }
    }
}
