﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BananaBundle.models;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Windows.Media;

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
            using (XmlWriter xmlWriter = XmlWriter.Create(@"C:\Users\" + Environment.UserName + @"\Google Drive\BananaBundle\" + Environment.UserName + ".xml", new XmlWriterSettings() { Indent = true, NewLineOnAttributes = true }))
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
                TreeViewItem tvi = new TreeViewItem() { Header = serie.Name, Tag = serie, Uid = serie.Id };
                foreach (Season season in serie.Seasons)
                {
                    TreeViewItem tvi2 = new TreeViewItem() { Header = season.Name, Tag = season, Uid = season.Id };
                    foreach (Episode episode in season.Episodes)
                    {
                        TreeViewItem tvi3 = new TreeViewItem() { Header = episode.Name, Tag = episode, Uid = episode.Id };
                        tvi2.Items.Add(tvi3);
                    }
                    tvi.Items.Add(tvi2);
                }
                list.Add(tvi);
            }
            return list.ToArray();
        }

        public TreeViewItem[] Combine(BundleHandler add, Brush b)
        {
            List<TreeViewItem> combined = this.GetTree().ToList();
            foreach (TreeViewItem serie in add.GetTree())
            {
                bool foundSerie = false;
                foreach (TreeViewItem oldserie in combined)
                {
                    if (serie.Header.ToString() == oldserie.Header.ToString())
                    {
                        oldserie.Foreground = b;
                        foundSerie = true;
                        foreach (TreeViewItem season in serie.Items)
                        {
                            bool foundSeason = false;
                            foreach (TreeViewItem oldSeason in oldserie.Items)
                            {
                                if (season.Header.ToString() == oldSeason.Header.ToString())
                                {
                                    oldSeason.Foreground = b;
                                    foundSeason = true;

                                    foreach (TreeViewItem episode in season.Items)
                                    {
                                        bool foundEpi = false;
                                        foreach (TreeViewItem oldEpisode in oldSeason.Items)
                                        {
                                            if (episode.Header.ToString() == oldEpisode.Header.ToString())
                                            {
                                                oldEpisode.Foreground = b;
                                                foundEpi = true;

                                            }
                                        }
                                        if (foundEpi == false)
                                        {
                                            TreeViewItem t3 = new TreeViewItem() { Header = episode.Header, Tag = episode.Tag, ItemsSource = episode.Items, Foreground = b, IsSelected= true };
                                            oldSeason.Items.Add(t3);
                                            oldSeason.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Header", System.ComponentModel.ListSortDirection.Ascending));
                                        }
                                    }
                                }
                            }
                            if (foundSeason == false)
                            {
                                TreeViewItem t2 = new TreeViewItem() { Header = season.Header, Tag = season.Tag, ItemsSource = season.Items, Foreground = b };
                                foreach (TreeViewItem t_episode in t2.Items)
                                {
                                    t_episode.Foreground = b;
                                    t_episode.IsHitTestVisible = true;
                                }
                                oldserie.Items.Add(t2);
                                oldserie.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Header", System.ComponentModel.ListSortDirection.Ascending));
                            }
                        }
                    }
                }
                if (foundSerie == false)
                {
                    TreeViewItem t1 = new TreeViewItem() { Header = serie.Header, Tag = serie.Tag, ItemsSource = serie.Items, Foreground = b };
                    foreach (TreeViewItem t_season in t1.Items)
                    {
                        t_season.Foreground = b;
                        t_season.IsHitTestVisible = true;
                        foreach (TreeViewItem t_episode in t_season.Items)
                        {
                            t_episode.Foreground = b;
                            t_episode.IsHitTestVisible = true;
                        }
                    }
                    combined.Add(t1);
                    combined = combined.OrderBy(x => x.Header).ToList();
                }
            }
            return combined.OrderBy(x => x.Header).ToArray();
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
