using DotNet.Format.Formatting;
using System.IO;
using System.Threading.Tasks;

namespace DotNet.Format
{
    internal sealed class Program
    {
        internal static async Task Main() => await new Formatter(GetCurrentDirectory()).FormatAllFiles();

        private static DirectoryInfo GetCurrentDirectory() => new DirectoryInfo(Directory.GetCurrentDirectory());        
    }
}
