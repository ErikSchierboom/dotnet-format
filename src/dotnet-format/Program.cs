using DotNet.Format.Formatting;
using DotNet.Format.Parser;
using Glob;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet.Format
{
    internal sealed class Program
    {
        private const string CSharpFilePath = @"C:\Code\playground\hackdays\2018-1\dotnet-format\samples\csharp-sample\Class1.cs";
        private const string EditorConfigFilePath = @"C:\Code\playground\hackdays\2018-1\dotnet-format\samples\csharp-sample\.editorconfig";

        internal static async Task Main(string[] args)
        {
            var editorConfigDocument = ParseEditorConfigDocument();
          
            var root = GetRootDirectory();
            var options = GetProgramOptions();

            //foreach (var file in new FormattingFiles(root, options.GlobbingPattern))

            var file = new FileInfo(CSharpFilePath);
            var formattingOptions = FormattingOptions.Create(file, editorConfigDocument);

            await Formatter.Format(file, formattingOptions);
        }

        private static DirectoryInfo GetRootDirectory() => new DirectoryInfo(Directory.GetCurrentDirectory());

        private static ProgramOptions GetProgramOptions() => new ProgramOptions();

        private static EditorConfigDocument ParseEditorConfigDocument() => EditorConfigDocumentParser.Parse(File.ReadAllText(EditorConfigFilePath));
    }

    public sealed class ProgramOptions
    {
        public string GlobbingPattern { get; set; } = "**/*.cs";
    }

    public class FormattingFiles : IEnumerable<FileInfo>
    {
        private readonly DirectoryInfo root;
        private readonly string globbingPattern;

        public FormattingFiles(DirectoryInfo root, string globbingPattern) 
            => (this.root, this.globbingPattern) = (root, globbingPattern);

        public IEnumerator<FileInfo> GetEnumerator() => root.GlobFiles(globbingPattern).Where(file => file.Extension == ".cs").GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
