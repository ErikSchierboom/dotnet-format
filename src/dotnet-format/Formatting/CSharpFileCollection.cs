using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNet.Format.Formatting
{
    public sealed class CSharpFileCollection : IEnumerable<FileInfo>
    {
        private readonly IEnumerable<FileInfo> files;

        public CSharpFileCollection(DirectoryInfo root) => files = new GlobbedFiles(root, "**/*.cs").Where(file => IsValidFile(file, root));

        public IEnumerator<FileInfo> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static bool IsValidFile(FileInfo file, DirectoryInfo root)
        {
            return true;
        }
    }
}
