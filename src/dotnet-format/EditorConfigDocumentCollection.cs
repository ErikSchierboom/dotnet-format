using DotNet.Format.Parser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNet.Format
{
    public sealed class EditorConfigDocumentCollection : IEnumerable<FileInfo>
    {
        private readonly DirectoryInfo root;
        private readonly GlobbedFiles files;
        private readonly Dictionary<string, FileInfo> filesByDirectory;
        private readonly Dictionary<string, EditorConfigDocument> editorConfigDocumentsPerDirectory;

        public EditorConfigDocumentCollection(DirectoryInfo root)
        {
            this.root = root;
            files = new GlobbedFiles(root, "**/*.editorconfig");
            filesByDirectory = files.ToDictionary(file => file.DirectoryName);
            editorConfigDocumentsPerDirectory = new Dictionary<string, EditorConfigDocument>();
        }

        public EditorConfigDocument GetForFile(FileInfo file)
        {
            var editorConfigDocuments = GetEditorConfigDocumentsInPath(file.Directory);
            return editorConfigDocuments.Aggregate(EditorConfigDocument.Empty, (acc, document) => acc.Merge(document));
        }

        private IEnumerable<EditorConfigDocument> GetEditorConfigDocumentsInPath(DirectoryInfo directory)
        {
            var currentDirectory = directory;
            var editorConfigDocuments = new List<EditorConfigDocument>();

            do
            {
                if (editorConfigDocumentsPerDirectory.TryGetValue(currentDirectory.FullName, out var editorConfigDocument))
                {
                    editorConfigDocuments.Add(editorConfigDocument);
                }
                else if (filesByDirectory.TryGetValue(currentDirectory.FullName, out var editorConfigFile))
                {
                    editorConfigDocument = EditorConfigDocumentParser.Parse(File.ReadAllText(editorConfigFile.FullName));
                    editorConfigDocuments.Add(editorConfigDocument);
                    editorConfigDocumentsPerDirectory[currentDirectory.FullName] = editorConfigDocument;
                }

                if (editorConfigDocument != null && editorConfigDocument.IsRoot)
                    break;

                currentDirectory = currentDirectory.Parent;
            } while (currentDirectory.FullName.StartsWith(root.FullName));

            return editorConfigDocuments;
        }

        public IEnumerator<FileInfo> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
