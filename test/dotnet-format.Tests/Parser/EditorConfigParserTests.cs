﻿using DotNet.Format.Parser;
using Newtonsoft.Json;
using System;
using Xunit;

namespace DotNet.Format.Tests.Parser
{
    public class EditorConfigParserTests
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
csharp_style_throw_expression = false:suggestion
";

            var actual = EditorConfigDocumentParser.Parse(EditorConfigDocument);

            var expected = new EditorConfigDocument(
                new[] { new EditorConfigProperty("root", "true") },
                new[]
                {
                    new EditorConfigSection("*",
                        new []
                        {
                            new EditorConfigProperty("indent_style", "space"),
                            new EditorConfigProperty("indent_size", "2")
                        }),
                    new EditorConfigSection("*.cs", new []
                        {
                            new EditorConfigProperty("indent_size", "4"),
                            new EditorConfigProperty("csharp_prefer_braces", "true", EditorConfigPropertySeverity.None),
                            new EditorConfigProperty("csharp_style_throw_expression", "false", EditorConfigPropertySeverity.Suggestion)
                        })
                }
            );
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }
    }
}
