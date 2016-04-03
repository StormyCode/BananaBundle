using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BananaBundle.models;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls;

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
            using (XmlWriter xmlWriter = XmlWriter.Create(@"C:\Users\"+Environment.UserName+@"\Google Drive\BananaBundle\"+Environment.UserName+".xml", new XmlWriterSettings() { Indent = true, NewLineOnAttributes = true }))
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

        public BundleHandler Compare(BundleHandler compareBundle)
        {
            for (int i = 0; i < this.Series.Count; i++)
            {
                for (int j = 0; j < compareBundle.Series.Count; j++)
                {
                    if (this.Series[i].Id == compareBundle.Series[j].Id)
                    {
                        for (int o = 0; o < this.Series[i].Seasons.Count; o++)
                        {
                            for (int k = 0; k < compareBundle.Series[j].Seasons.Count; k++)
                            {
                                if (this.Series[i].Seasons[o].Id == compareBundle.Series[j].Seasons[k].Id &&
                                    this.Series[i].Seasons[o].Episodes.Count == compareBundle.Series[j].Seasons[k].Episodes.Count &&
                                    this.Series[i].Seasons[o].Size > compareBundle.Series[j].Seasons[k].Size * 0.9 &&
                                    this.Series[i].Seasons[o].Size < compareBundle.Series[j].Seasons[k].Size * 1.1)
                                {
                                    compareBundle.Series[j].Seasons.RemoveAt(k);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            // Removes the series without any seasons
            compareBundle.Series = compareBundle.Series.Where(x => x.Seasons.Count != 0).ToList();
            return compareBundle;
        }

        public TreeViewItem[] GetTree()
        {
            List<TreeViewItem> list = new List<TreeViewItem>();
            foreach (Serie serie in this.Series)
            {
                TreeViewItem tvi = new TreeViewItem() { Header = serie.Name, Tag = serie.Id };
                foreach (Season season in serie.Seasons)
                {
                    TreeViewItem tvi2 = new TreeViewItem() { Header = season.Name, Tag = tvi.Tag+"/"+season.Id };
                    foreach (Episode episode in season.Episodes)
                    {
                        TreeViewItem tvi3 = new TreeViewItem() { Header = episode.Name, Tag = tvi2.Tag+"/"+episode.Id };
                        tvi2.Items.Add(tvi3);
                    }
                    tvi.Items.Add(tvi2);
                }
                list.Add(tvi);
            }
            return list.ToArray();
        }

        public TreeViewItem[] AddHighlighted(BundleHandler addition)
        {
            // todo implement this
            return null;
        }

        public IBananaObject GetElementById(string id)
        {
            string[] args = id.Split('/');
            IBananaObject result = null;
            if (args.Length > 0)
            {
                foreach (Serie serie in this.Series)
                {
                    if (args[0] == serie.Id)
                    {
                        result = serie;
                        if (args.Length > 1)
                        {
                            foreach (Season season in serie.Seasons)
                            {
                                if (args[1] == season.Id)
                                {
                                    result = season;
                                    if (args.Length > 2)
                                    {
                                        foreach (Episode episode in season.Episodes)
                                        {
                                            if (args[2] == episode.Id)
                                            {
                                                result = episode;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            return result;
        }
    }
}
