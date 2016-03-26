using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaBundle.models
{
    class Serie : IBananaObject
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

        public List<Season> Seasons { get; set; }

        public Serie(string directory)
        {
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
