using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaBundle.models
{
    public interface IBananaObject
    {
        string Id { get; }
        string Name { get; }
        string Path { get; }
        double Size { get; }
    }
}
