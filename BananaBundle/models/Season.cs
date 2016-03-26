using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BananaBundle.models
{
    class Season : IBananaObject
    {
        public string Id
        {
            get
            {
                return Regex.Replace(this.Path, @"[^\d]", "").TrimStart('0');
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
                return this.Episodes.Select(x => x.Size).Sum();
            }
            private set;
        }

        public List<Episode> Episodes { get; private set; }

        public Season(string directory)
        {
            this.Episodes = new List<Episode>();
            if (Directory.Exists(@directory))
            {
                foreach (string episodeFile in Directory.GetFiles(@directory))
                {
                    this.Episodes.Add(new Episode(episodeFile));
                }
            }
        }
    }
}
