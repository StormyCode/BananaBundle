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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double _size;
        public double Size
        {
            get
            {
                if (this._size == 0)
                    this._size = this.Episodes.Select(x => x.Size).Sum();
                return this._size;
            }
            private set
            {
                this._size = value;
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

        public Season(string path, double size)
        {
            this.Episodes = new List<Episode>();
            this.Path = path;
            this.Size = size;
        }


        public string GetInfo()
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine(this.Name);
            s.AppendLine(Math.Round(this.Size, 2).ToString() + " GB");
            s.AppendLine((this.Episodes.Count > 1) ? this.Episodes.Count + " episodes" : this.Episodes.Count + " episode");
            return s.ToString();
        }
    }
}
