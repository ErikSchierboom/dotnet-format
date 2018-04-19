using DotNet.Format.Parser;
using Newtonsoft.Json;
using Xunit;

namespace DotNet.Format.Tests.Parser
{
    public class EditorConfigDocumentParserTests
    {
        [Fact]
        public void ParsesDocumentWhenEditorConfigDocumentIsValid()
        {
            const string EditorConfigDocument = @"
# This is the root editorconfig file
root = true

[*]
indent_style = space
indent_size = 2

[*.cs]
indent_size = 4
csharp_prefer_braces = true:none
csharp_style_throw_expression = false
";

            var actual = EditorConfigDocumentParser.Parse(EditorConfigDocument);

            var expected = new EditorConfigDocument(
                new EditorConfigPropertyCollection(new[] { new EditorConfigProperty("root", "true") }),
                new EditorConfigSectionCollection(new[]
                {
                    new EditorConfigSection("*",
                        new EditorConfigPropertyCollection(new []
                        {
                            new EditorConfigProperty("indent_style", "space"),
                            new EditorConfigProperty("indent_size", "2")
                        })
                    ),
                    new EditorConfigSection("*.cs",
                        new EditorConfigPropertyCollection(new []
                        {
                            new EditorConfigProperty("indent_size", "4"),
                            new EditorConfigProperty("csharp_prefer_braces", "true"),
                            new EditorConfigProperty("csharp_style_throw_expression", "false")
                        })
                    )
                })
            );
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }
    }
}
