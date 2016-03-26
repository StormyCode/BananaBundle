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
    public class Season : IBananaObject
    {
        public string Id
        {
            get
            {
                return Regex.Replace(this.Name, @"[^\d]", "").TrimStart('0');
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
                return this.Episodes.Select(x => x.Size).Sum();
            }
        }

        public List<Episode> Episodes { get; private set; }

        public Season(string directory)
        {
            this.Episodes = new List<Episode>();
            this.Path = @directory;
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
