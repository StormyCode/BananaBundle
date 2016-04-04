using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BananaBundle.models
{
    [DebuggerDisplay("{Name}")]
    public class Episode : IBananaObject
    {
        
        public string Id
        {
            get
            {
                return this.Name;
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
                if(this._size == 0)
                this._size = new System.IO.FileInfo(this.Path).Length / Math.Pow(1024, 3);
                return this._size;
            }
            private set
            {
                this._size = value;
            }
        }
        
        public string Extension
        {
            get
            {
                return System.IO.Path.GetExtension(this.Path);
            }
        }

        public Episode(string file)
        {
            this.Path = file;
        }

        public Episode(string path, double size)
        {
            this.Path = path;
            this.Size = size;
        }


        public string GetInfo()
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine("Name: "+this.Name);
            s.AppendLine("Size: "+this.Size.ToString());
            s.AppendLine("Extension: "+this.Extension);
            return s.ToString();
        }
    }
}
