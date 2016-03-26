using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaBundle.models
{
    class Season : IBananaObject
    {
        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Path
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public double Size
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<Episode> Episodes { get; set; }

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
