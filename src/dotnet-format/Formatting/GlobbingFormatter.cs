using System.IO;
using System.Threading.Tasks;

namespace DotNet.Format.Formatting
{
    public static class GlobbingFormatter
    {
        public static async Task Format(DirectoryInfo root)
        {
            var editorConfigDocuments = new EditorConfigDocumentCollection(root);

            foreach (var csharpFile in new CSharpFileCollection(root))
            {
                var editorConfigDocument = editorConfigDocuments.GetForFile(csharpFile);
                var formattingOptions = FormattingOptions.Create(csharpFile, editorConfigDocument);
                await Formatter.Format(csharpFile, formattingOptions);
            }

            // TODO: do bulk change Task.WhenAll
        }
    }
}
