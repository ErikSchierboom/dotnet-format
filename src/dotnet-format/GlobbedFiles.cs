using Glob;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DotNet.Format
{
    public class GlobbedFiles : IEnumerable<FileInfo>
    {
        private readonly DirectoryInfo root;
        private readonly string globbingPattern;

        public GlobbedFiles(DirectoryInfo root, string globbingPattern) 
            => (this.root, this.globbingPattern) = (root, globbingPattern);

        public IEnumerator<FileInfo> GetEnumerator() => root.GlobFiles(globbingPattern).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
