using Glob;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNet.Format.Helpers
{
    public sealed class GlobbedFiles : IEnumerable<FileInfo>
    {
        private readonly IEnumerable<FileInfo> files;

        public GlobbedFiles(DirectoryInfo root, string includePattern, string excludePattern = null)
        {
            var included = root.GlobFiles(includePattern);
            var excluded = excludePattern == null ? Array.Empty<FileSystemInfo>() : root.GlobFileSystemInfos(excludePattern);

            files = included.Where(file => !excluded.Any(exclude => file.FullName.StartsWith(exclude.FullName)));
        }

        public IEnumerator<FileInfo> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
