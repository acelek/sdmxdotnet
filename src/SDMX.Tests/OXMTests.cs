﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using SDMX.Parsers;
using System.Xml;
using System.IO;
using OXM;

namespace SDMX.Tests
{
    [TestFixture]
    public class OXMTests
    {
        public static StringBuilder sb;
        
        [Test]
        public void KeyFamily_ToXml()
        {
            var keyFamily = new KeyFamily("KeyFamilyName", "KeyID", "agencyID");

            var concept = new Concept("FREQ", "agencyID");
            var conceptScheme = new ConceptScheme("SDMX", "SDMX");
            conceptScheme.Add(concept);
            var dimension = new Dimension(concept);
            dimension.TextFormat = new TextFormat();
            dimension.TextFormat.TextType = TextType.Double;
            keyFamily.AddDimension(dimension);
            var ref_area = new Concept("REF_AREA", "agencyID");
            conceptScheme.Add(ref_area);
            dimension = new Dimension(ref_area);
            keyFamily.AddDimension(dimension);

            var obsValue = new Concept("OBS_VALUE", "SDMX");
            conceptScheme.Add(obsValue);
            keyFamily.PrimaryMeasure = new PrimaryMeasure(obsValue);
            

            var annotation = new Annotation();
            annotation.Title = "Anno Title";
            annotation.Type = "Internal";
            annotation.Text[Language.English] = "English Text";

            keyFamily.Annotations.Add(annotation);

            XNamespace structure = Namespaces.Structure;
            var map = new FragmentMap<KeyFamily>("KeyFamily", new KeyFamilyMap(new DSD()));

            OXMTests.sb = new StringBuilder();
            

            var settings = new XmlWriterSettings() { Indent = true, ConformanceLevel = ConformanceLevel.Auto };
            using (var writer = XmlWriter.Create(OXMTests.sb, settings))
            {                
                map.WriteXml(writer, keyFamily);
            }

            KeyFamily keyFamily2;
            using (var reader = XmlReader.Create(new StringReader(sb.ToString())))
            {
                keyFamily2 = map.ReadXml(reader);
            }
           
            XElement element = XElement.Parse(sb.ToString());
            Console.Write(sb);

            Assert.NotNull(element.Element("Components"));
            Assert.AreEqual(2, element.Element("Components").Elements("Dimension").Count());
            
        }
     
        [Test]
        public void StructureSampleTest()
        {   
            string dsdPath = Utility.GetPathFromProjectBase("lib\\StructureSample.xml");

            StructureMessageMap map = new StructureMessageMap();
            StructureMessage message;

            var settings = new XmlReaderSettings() { IgnoreWhitespace = true };
            using (var reader = XmlReader.Create(dsdPath, settings))
            {               
                message = map.ReadXml(reader);
            }

            var output = new StringBuilder();

            using (var writer = XmlWriter.Create(output))
            {
                map.WriteXml(writer, message);
            }
        }

        


    }



}