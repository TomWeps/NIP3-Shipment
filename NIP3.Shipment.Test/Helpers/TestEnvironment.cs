﻿using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace NIP3.Shipment.Test.Helpers
{
    public static class TestEnvironment
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string ProjectDirectory
        {
            get
            {
                var assemblyDirectory = AssemblyDirectory;
                var path = assemblyDirectory.Substring(0, assemblyDirectory.IndexOf("\\bin"));
                return path;
            }
        }

        public static string PactsDirectory
        {
            get { return Path.Combine(ProjectDirectory, "Pacts"); }
        }

        public static string LogsDirectory
        {
            get { return Path.Combine(ProjectDirectory, "PactLogs"); }
        }

        public static Uri ShipmentServiceUri
        {
            get
            {
                string shipmentServiceUri = ConfigurationManager.AppSettings["ShipmentServiceUri"];
                if (string.IsNullOrWhiteSpace(shipmentServiceUri))
                {
                    throw new ApplicationException("ShipmentServiceUri");
                }
                return new Uri(shipmentServiceUri);
            }
        }
    }
}
