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
    public static class ExtensionMethods
    {
        public static string GetFramework(this Assembly assembly)
        {
            try
            {
                var attributes = assembly.CustomAttributes;
                var framework = attributes.FirstOrDefault(x => x.AttributeType == typeof(TargetFrameworkAttribute));

                if (framework != null && framework.NamedArguments != null)
                {
                    return framework.NamedArguments[0].TypedValue.ToString();
                }
            }
            catch { }

            return "????";
        }

        public static string GetFileVersion(this Assembly assembly)
        {
            try
            {
                var attributes = assembly.CustomAttributes;
                var framework = attributes.FirstOrDefault(x => x.AttributeType == typeof(AssemblyFileVersionAttribute));

                if (framework != null && framework.ConstructorArguments[0] != null)
                {
                    return framework.ConstructorArguments[0].Value.ToString();
                }
            }
            catch { }

            return "????";
        }

        public static bool ValidPath(this string location)
        {

            var result = false;
            try
            {
                Directory.GetDirectories(location);
                result = true;
            }
            catch
            { }

            return result;
        }

    }
}
