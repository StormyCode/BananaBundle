using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace BananaBundle.models
{
    [DebuggerDisplay("{Name}")]
    public class Serie : IBananaObject
    {
        public string Id
        {
            get
            {
                return Regex.Replace(this.Name, @"[^A-Za-z0-9]", "").ToLower();
            }
        }

        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(this.Path);
            }
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
