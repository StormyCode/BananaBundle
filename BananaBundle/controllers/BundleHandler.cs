using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BananaBundle.models;
using System.Xml;
using System.Xml.Linq;

namespace BananaBundle.controllers
{
    public class BundleHandler
    {
        public List<Serie> Series { get; private set; }

        public string Path { get; set; }

        public BundleHandler(string directory)
        {
            this.Series = new List<Serie>();
            if (Directory.Exists(@directory))
            {
                this.Path = @directory;
                foreach (string serieFolder in Directory.GetDirectories(this.Path))
                {
                    this.Series.Add(new Serie(serieFolder));
                }
                this.WriteXML();
            }
        }

        public BundleHandler(XDocument xDoc)
        {
            this.Series = new List<Serie>();
            foreach (XElement xSerie in xDoc.Descendants("serie"))
            {
                Serie serie = new Serie(xSerie.Attribute("path").Value, double.Parse(xSerie.Attribute("size").Value));
                foreach (XElement xSeason in xSerie.Descendants("season"))
                {
                    Season season = new Season(xSeason.Attribute("path").Value, double.Parse(xSeason.Attribute("size").Value));
                    foreach (XElement xEpisode in xSeason.Descendants("episode"))
                    {
                        Episode episode = new Episode(xEpisode.Attribute("path").Value, double.Parse(xEpisode.Attribute("size").Value));
                        season.Episodes.Add(episode);
                    }
                    serie.Seasons.Add(season);
                }
                this.Series.Add(serie);
            }
        }

        public void WriteXML()
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(@"structure.xml", new XmlWriterSettings() { Indent = true, NewLineOnAttributes = true }))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("bundle");
                foreach (Serie serie in this.Series)
                {
                    xmlWriter.WriteStartElement("serie");
                    xmlWriter.WriteAttributeString("path", serie.Path);
                    xmlWriter.WriteAttributeString("size", serie.Size.ToString());
                    foreach (Season season in serie.Seasons)
                    {
                        xmlWriter.WriteStartElement("season");
                        xmlWriter.WriteAttributeString("path", season.Path);
                        xmlWriter.WriteAttributeString("size", season.Size.ToString());
                        foreach (Episode episode in season.Episodes)
                        {
                            xmlWriter.WriteStartElement("episode");
                            xmlWriter.WriteAttributeString("path", episode.Path);
                            xmlWriter.WriteAttributeString("size", episode.Size.ToString());
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }
    }
}
