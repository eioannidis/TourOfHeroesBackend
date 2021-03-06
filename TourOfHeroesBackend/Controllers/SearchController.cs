﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TourOfHeroesBackend.Models;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;



namespace TourOfHeroesBackend.Controllers
{

    [Route( "api/[controller]" )]
    public class SearchController : Controller
    {
        string database = "HeroDatabase.xml";
        string dirPath = @"C:\Users\eioannidis\source\repos\MyDatabase\";

        

        // GET api/search/superman
        [HttpGet( "{term}" )]
        public IEnumerable<Hero> Get( string term )
        {
            List<Hero> heroList = new List<Hero>();
            XmlDocument doc = new XmlDocument();

            doc.Load( database );

            foreach ( XmlNode node in doc.SelectNodes( "//Heroes/Hero" ) )
            {
                Hero hero = new Hero();
                if ( node.ChildNodes[1].InnerText.IndexOf( term, StringComparison.OrdinalIgnoreCase ) >= 0 )
                {
                    hero.Id = Convert.ToInt32( node.ChildNodes[0].InnerText );
                    hero.Name = node.ChildNodes[1].InnerText;
                    hero.Power = node.ChildNodes[2].InnerText;
                    hero.Identity = node.ChildNodes[3].InnerText;
                    heroList.Add( hero );
                }
            }

            return heroList;
        }
        
        
        [HttpPost]
        public void UpdateXmlFile([FromBody] Info fileInfo )
        {
            
            string xmlTargetPath = System.IO.Path.Combine( dirPath, fileInfo.XmlFileName );
            XmlDocument doc = new XmlDocument();
            doc.Load( xmlTargetPath );

            XmlNode root = doc.SelectSingleNode( "//File" );

            XmlElement element = doc.CreateElement( "Title" );
            element.InnerText = fileInfo.Title;
            root.AppendChild( element );
            element = doc.CreateElement( "UploaderUsername" );
            element.InnerText = fileInfo.UploaderInfo.Username;
            root.AppendChild( element );
            element = doc.CreateElement( "UploaderName" );
            element.InnerText = fileInfo.UploaderInfo.FirstName + " " + fileInfo.UploaderInfo.LastName;
            root.AppendChild( element );
            element = doc.CreateElement( "Description" );
            element.InnerText = fileInfo.Description;
            root.AppendChild( element );

            doc.Save( xmlTargetPath );
        }

        
    }
}
