using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DotNet.Format
{
    public sealed class CSharpFileCollection : IEnumerable<FileInfo>
    {
        private readonly IEnumerable<FileInfo> files;

        public CSharpFileCollection(DirectoryInfo root) => files = new GlobbedFiles(root, "**/*.cs", "{bin,obj}");

        public IEnumerator<FileInfo> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
