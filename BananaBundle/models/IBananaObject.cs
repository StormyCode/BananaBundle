using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaBundle.models
{
    protected interface IBananaObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public double Size { get; set; }
    }
}
