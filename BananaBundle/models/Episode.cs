﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaBundle.models
{
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

        public double Size
        {
            get
            {
                return new System.IO.FileInfo(this.Path).Length / Math.Pow(1024, 3);
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
    }
}
