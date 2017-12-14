using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.IO.Compression;
using System.Xml;

namespace TwicePower.Homeate.Binding.Knx.Parsing
{
    public static class KnxprojReader
    {
        const string KnxMasterXml = "knx_master.xml";

        /// <summary>
        /// Reads the ZIP, decomresses in memory and extracts the master, project and installtions details. Catalog info is ignored for now.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static v13.KNX ReadKnxproject(Stream file)
        {
            System.IO.Compression.ZipArchive z = new System.IO.Compression.ZipArchive(file);
            var masterFileStream = z.Entries.FirstOrDefault(w => w.Name == KnxMasterXml)?.Open();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Knx.Parsing.v13.KNX));
            var master = (v13.KNX)xmlSerializer.Deserialize(masterFileStream);
            masterFileStream.Dispose();
            
            List<v13.Project_t> knxProjects = new List<v13.Project_t>();
            foreach (var projectXmlEntry in z.Entries.Where(w => w.Name.EndsWith("project.xml", System.StringComparison.InvariantCultureIgnoreCase)).ToArray())
            {
                var projectFileStream = projectXmlEntry.Open();
                var knxProject = (v13.KNX)xmlSerializer.Deserialize(projectFileStream);
                knxProjects.AddRange(knxProject.Project);
                masterFileStream.Dispose();
                List<v13.Project_tInstallation> installations = new List<v13.Project_tInstallation>();
                foreach (var installationEntry in z.Entries.Where(w => w.Name.EndsWith(".xml", System.StringComparison.InvariantCultureIgnoreCase) && w.FullName != projectXmlEntry.FullName && w.FullName.StartsWith(knxProject.Project[0].Id, System.StringComparison.Ordinal)).ToArray())
                {
                    var installationStream = installationEntry.Open();
                    var knxInstallation = (v13.KNX)xmlSerializer.Deserialize(installationStream);
                    installations.AddRange(knxInstallation.Project.First().Installations);
                }
                knxProject.Project[0].Installations = installations.ToArray();
            }

            master.Project = knxProjects.ToArray();

            return master;
        }

        public static void GetFunctions(v13.KNX knx)
        {
        }

        private static IEnumerable<T> SelectNested<T>(this T source, System.Func<T, T> selector) where T : class 
        {
            var current = source;
            while (current != null) {
                yield return current;
            current = selector(current);
        }
}

    }
}
