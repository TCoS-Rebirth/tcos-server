﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PackageExtractor
{
  class Program
  {
    
    static void Main (string[] args)
    {
      Package.ReadLocalizedStrings(@"G:\TCoS0.9\data\static\descriptions.s");

      //Parse universe file
      Package pack = new Package(@"G:\TCoS0.9\data\universes\Complete_UniverseSBU.sbu");
    //  pack.Load(/*@"G:\TCoS0.9\PT_Hawksmouth"*/);
     // pack.ToXML(@"G:\TCoS0.9\UniverseFull.xml");

      //Parse Hawksmouth package
      Package hawksmouth = new Package(@"G:\TCoS0.9\data\environment\maps\PT_Hawksmouth.sbw");
      hawksmouth.Load(@"G:\TCoS0.9\test");
     // hawksmouth.ToXML(@"G:\TCoS0.9\HawksmouthFull.xml");//Full dump of the package in an XML file

      hawksmouth.ToXML(@"G:\TCoS0.9\test.xml");
      Console.ReadKey();
      return;
      //Parse Aldenvault package
      Package aldenvault = new Package(@"G:\TCoS0.9\data\environment\maps\PT_Aldenvault.sbw");
      aldenvault.Load();

      //Dump interesting data to XML
      XDocument xmlDump = new XDocument();
      xmlDump.Add(new XComment("Generated by TCoS-Rebirth PackageExtractor"));

      XElement root = new XElement("UniversesInformation");
      root.Add(ExtractWorlds(pack));
      root.Add(ExtractGameRoutes(pack));

      XElement hawksmouthXML = new XElement("Map");
      hawksmouthXML.SetAttributeValue("name", "PT_Hawksmouth");
      hawksmouthXML.Add(ExtractPlayerStarts(hawksmouth));
      hawksmouthXML.Add(ExtractSBWorldPortals(hawksmouth));
      root.Add(hawksmouthXML);

      XElement aldenvaultXML = new XElement("Map");
      aldenvaultXML.SetAttributeValue("name", "PT_Aldenvault");
      aldenvaultXML.Add(ExtractPlayerStarts(aldenvault));
      aldenvaultXML.Add(ExtractSBWorldPortals(aldenvault));
      root.Add(aldenvaultXML);

      xmlDump.Add(root);
      xmlDump.Save(@"G:\TCoS0.9\UniversesInformation.xml");

      Console.ReadKey ();
    }

    static XElement ExtractPlayerStarts(Package package)
    {
      XElement result = new XElement("PlayerStarts");
      SBObject[] playerStarts = Array.FindAll(package.Objects.ToArray(), o => o.Name.Contains("PlayerStart"));
      
      foreach (SBObject o in playerStarts)
      {
        XElement xmlPlayerStart = new XElement("PlayerStart");
        xmlPlayerStart.SetAttributeValue("name", o.Name);
        xmlPlayerStart.Add(new XElement("NavigationTag", o.Properties["NavigationTag"].Value));
        xmlPlayerStart.Add(new XElement("Location", o.Properties["Location"].Value));
        result.Add(xmlPlayerStart);
      }
      return result;
    }

    static XElement ExtractSBWorldPortals(Package package)
    {
      XElement result = new XElement("SBWorldPortals");
      SBObject[] worldPortals = Array.FindAll(package.Objects.ToArray(), o => o.Name.Contains("SBWorldPortal"));

      foreach (SBObject o in worldPortals)
      {
        XElement worldPortal = new XElement("SBWorldPortal");
        worldPortal.SetAttributeValue("name", o.Name);
        worldPortal.Add(new XElement("NavigationTag", o.Properties["NavigationTag"].Value));
        worldPortal.Add(new XElement("Location", o.Properties["Location"].Value));
        worldPortal.Add(new XElement("CollisionRadius", o.Properties["CollisionRadius"].Value));
        result.Add(worldPortal);
      }
      return result;
    }

    //Works only with universe packages
    static XElement ExtractGameRoutes(Package package)
    {
      XElement result = new XElement("GameRoutes");
      SBObject[] gameRoutes = Array.FindAll(package.Objects.ToArray(), o => o.ClassName.Contains("Game_Route"));

      foreach(SBObject o in gameRoutes)
      {
        XElement gameRoute = new XElement("Game_Route");
        gameRoute.SetAttributeValue("name", o.Name);
        if (o.Properties.ContainsKey("DeathWorld"))
         gameRoute.Add(new XElement("DeathWorld", o.Properties["DeathWorld"].Value));
        if (o.Properties.ContainsKey("DeathPortal"))
          gameRoute.Add(new XElement("DeathPortal", o.Properties["DeathPortal"].Value));
        if (o.Properties.ContainsKey("ShardName"))
          gameRoute.Add(new XElement("ShardName", o.Properties["ShardName"].Value));
        if (o.Properties.ContainsKey("TravelWorld"))
          gameRoute.Add(new XElement("TravelWorld", o.Properties["TravelWorld"].Value));
        if (o.Properties.ContainsKey("TravelPortal"))
          gameRoute.Add(new XElement("TravelPortal", o.Properties["TravelPortal"].Value));
        if (o.Properties.ContainsKey("DestinationWorld"))
          gameRoute.Add(new XElement("DestinationWorld", o.Properties["DestinationWorld"].Value));
        if (o.Properties.ContainsKey("DestinationPortal"))
          gameRoute.Add(new XElement("DestinationPortal", o.Properties["DestinationPortal"].Value));
        if (o.Properties.ContainsKey("AllowRentACabin"))
          gameRoute.Add(new XElement("AllowRentACabin", o.Properties["AllowRentACabin"].Value));
        if (o.Properties.ContainsKey("CabinCost"))
          gameRoute.Add(new XElement("CabinCost", o.Properties["CabinCost"].Value));
        if (o.Properties.ContainsKey("WorldPortalTag"))
          gameRoute.Add(new XElement("WorldPortalTag", o.Properties["WorldPortalTag"].Value));
        result.Add(gameRoute);
      }

      return result;
    }

    //Works only with universe packages
    static XElement ExtractWorlds(Package package)
    {
      XElement result = new XElement("Worlds");
      SBObject[] sbWorlds = Array.FindAll(package.Objects.ToArray(), o => o.ClassName.Equals("SBWorld"));

      foreach (SBObject o in sbWorlds)
      {
        XElement sbWorld = new XElement("SBWorld");
        sbWorld.SetAttributeValue("name", o.Name);

        sbWorld.Add(new XElement("worldID", o.Properties["worldID"].Value));
        sbWorld.Add(new XElement("WorldName", o.Properties["WorldName"].Value));
        sbWorld.Add(new XElement("WorldFile", o.Properties["WorldFile"].Value));
        if(o.Properties.ContainsKey("InstanceMaxPlayers"))
          sbWorld.Add(new XElement("InstanceMaxPlayers", o.Properties["InstanceMaxPlayers"].Value));
        if (o.Properties.ContainsKey("EntryPortals"))
          sbWorld.Add(new XElement("EntryPortals", o.Properties["EntryPortals"].Value));
      
        result.Add(sbWorld);
      }

      return result;
    }
  }
}
