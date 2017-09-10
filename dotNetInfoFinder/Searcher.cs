using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace dotNetVersionFinder
{
    public class Searcher
    {
        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Resolving...");
            return typeof(Type).Assembly;
        }
        public static StringBuilder DLLInfoCSV(string baseLocation, bool useReflection = false)
        {

            var dave = Directory.EnumerateFiles(baseLocation, "*.*", SearchOption.AllDirectories).Where(x => x.ToLower().Contains("xss")).ToList();

            var directories = Directory.EnumerateFiles(baseLocation, "*.*", SearchOption.AllDirectories)
                .Where(x => x.ToLower().EndsWith(".dll") || x.ToLower().EndsWith(".exe"));
                //.Where(y => (new FileInfo(y)).Name.ToLower().Contains("sandpiper"));
                //.Where(y => (new FileInfo(y)).Name.ToLower().Contains("sandpiper.core.dll"));
            //.Where(y => (new FileInfo(y)).Name.ToLower() == "entityframework.dll" || (new FileInfo(y)).Name.ToLower() == "sandpiper.core.dll");

            Console.WriteLine("Files found {0}",directories.Count());

            var sb = new StringBuilder();
            sb.AppendLine(" File , Processor Architecture, Framework, Framework (Reflection), File Version (Reflection) , Full Path");
            var count = 0;

            foreach (var directory in directories)
            {
                if (++count % 100 == 0)
                {
                    Console.Write("\rFiles Processed {0}", count.ToString());
                }
                
                var file = new FileInfo(directory);
                sb.Append(file.Name).Append(",");

                try
                {
                    var reflectionFramework = string.Empty;
                    var reflectionFileVersion = string.Empty;

                    if (useReflection)
                    {
                        try
                        {
                            var assembly = Assembly.LoadFrom(directory);
                            reflectionFramework = assembly.GetFramework();
                            reflectionFileVersion = assembly.GetFileVersion();
                        }
                        catch(Exception ex)
                        {
                            reflectionFramework = ex.Message;
                        }
                    }
                    
                    var text = string.Empty;
                    var framework = string.Empty;
                    var dllByteArray = File.ReadAllBytes(directory);
                    var frameworkDisplayNameByteArray = Encoding.Default.GetBytes("FrameworkDisplayName");
                    var corFlagsReader = CorFlagsReader.ReadAssemblyMetadata(directory);

                    var startingPoint = ByteSearcher.Find(frameworkDisplayNameByteArray, dllByteArray);

                    if (startingPoint > 0)
                    {
                        var bytes = ByteSearcher.ReturnBytes(dllByteArray, startingPoint + 35, 30);
                        text = Encoding.Default.GetString(bytes);
                        framework = TextHelper.GetFramework(text);
                    }


                    sb.Append(corFlagsReader.ProcessorArchitecture)
                        .Append(",")
                        .Append(framework)
                        .Append(",")
                        .Append(reflectionFramework)
                        .Append(",")
                        .Append(reflectionFileVersion)
                        .Append(",")
                        .AppendLine(directory);

                }
                catch(Exception ex)
                {
                    sb.AppendLine(ex.Message);
                    continue;
                }
            }

            return sb;
        }

    }
}
