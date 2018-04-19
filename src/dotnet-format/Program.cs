using DotNet.Format.Formatting;
using System.IO;
using System.Threading.Tasks;

namespace DotNet.Format
{
    internal sealed class Program
    {
        internal static async Task Main() 
            => await GlobbingFormatter.Format(GetCurrentDirectory());

        private static DirectoryInfo GetCurrentDirectory() => new DirectoryInfo(Directory.GetCurrentDirectory());        
    }
}
