
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscUtils;
using DiscUtils.Iso9660;

namespace IsoMakerCMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: CreateIso <source folder> <destination iso file>");
                return;
            }

            string sourceFolder = args[0];
            string destinationIsoFile = args[1];

            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine("Error: Source folder does not exist.");
                return;
            }

            try
            {
                CDBuilder builder = new CDBuilder
                {
                    UseJoliet = true,
                    VolumeIdentifier = "MyISO"
                };

                AddFiles(builder, sourceFolder, "");

                builder.Build(destinationIsoFile);

                Console.WriteLine("ISO file created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void AddFiles(CDBuilder builder, string directory, string pathInIso)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                builder.AddFile(pathInIso + Path.GetFileName(file), file);
            }

            foreach (var subDir in Directory.GetDirectories(directory))
            {
                AddFiles(builder, subDir, pathInIso + Path.GetFileName(subDir) + "/");
            }
        }
    }
}
