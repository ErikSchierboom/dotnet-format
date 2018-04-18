using System;
using System.IO;
using System.Threading.Tasks;
using DotNet.Format.Parser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Text;

namespace DotNet.Format
{
    class Program
    {
        private const string CSharpFilePath = @"C:\Code\playground\hackdays\2018-1\dotnet-format\samples\csharp-sample\Class1.cs";
        private const string EditorConfigFilePath = @"C:\Code\playground\hackdays\2018-1\dotnet-format\.editorconfig";

        static async Task Main(string[] args)
        {
            var text = File.ReadAllText(CSharpFilePath);
            var sourceText = SourceText.From(text);

            var editorConfigText = File.ReadAllText(EditorConfigFilePath);
            var editorConfigSourceText = SourceText.From(editorConfigText);

            var x = EditorConfigDocumentParser.Parse(editorConfigText);
            Console.WriteLine(x);

            ////Document document = default;
            ////var x = document.WithText(sourceText);


            //ProjectId projectId = ProjectId.CreateNewId();
            //DocumentId documentId = DocumentId.CreateNewId(projectId);

            //Solution solution = new AdhocWorkspace().CurrentSolution
            //      .AddProject(projectId, "TestProject", "TestProject", LanguageNames.CSharp)
            //      .AddDocument(DocumentId.CreateNewId(projectId), ".editorconfig", editorConfigSourceText)
            //      .AddDocument(documentId, "Class1.cs", sourceText);
            //Document document = solution.GetDocument(documentId);

            //var y = await Formatter.FormatAsync(document);

            //var z = await y.GetTextAsync();
            //Console.WriteLine(z);

            //Formatter.FormatAsync()

            //      Console.WriteLine("Hello World!");
        }

        //private SyntaxNode Format(Document document)
        //{
        //  Document updatedDocument = document.WithSyntaxRoot(document.GetSyntaxRootAsync().Result);
        //  return Formatter.FormatAsync(Simplifier.ReduceAsync(updatedDocument, Simplifier.Annotation).Result, Formatter.Annotation).Result.GetSyntaxRootAsync().Result;
        //}
    }
}
