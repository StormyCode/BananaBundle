using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace BananaBundle.controllers
{
    public class XmlBundleHandler
    {
        public List<BundleHandler> XMLBundles { get; private set; }

        public XmlBundleHandler(string path)
        {
            this.XMLBundles = new List<BundleHandler>();
            if (System.IO.Directory.Exists(@path))
            {
                foreach (string item in System.IO.Directory.GetFiles(@path, "*.xml"))
                {
                    BundleHandler b = new BundleHandler(XDocument.Load(item));
                    b.Path = Path.GetFileNameWithoutExtension(item);
                    this.XMLBundles.Add(b);
                }
            }
        }
    }
}
