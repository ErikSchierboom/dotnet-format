using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DotNet.Format
{
    public sealed class SourceFileCollection : IEnumerable<FileInfo>
    {
        private readonly IEnumerable<FileInfo> files;

        public SourceFileCollection(DirectoryInfo root) => files = new GlobbedFiles(root, "**/*.cs", "{bin,obj}");

        public IEnumerator<FileInfo> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
