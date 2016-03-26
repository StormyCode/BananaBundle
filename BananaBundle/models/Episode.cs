using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaBundle.models
{
    class Episode : IBananaObject
    {
        public string Id
        {
            get
            {
                return this.Name;
            }
            private set;
        }

        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(this.Path);
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
                return new System.IO.FileInfo(this.Path).Length / Math.Pow(1024, 3);
            }
            private set;
        }

        public string Extension 
        {
            get
            {
                return System.IO.Path.GetExtension(this.Path);
            }
            private set; 
        }

        public Episode(string file)
        {
            this.Path = file;
        }
    }
}
