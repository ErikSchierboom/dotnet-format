using System.Collections.Generic;
using System.Linq;

namespace DotNet.Format.Parser
{
    public sealed class EditorConfigDocument
    {
        public EditorConfigDocument(IReadOnlyList<EditorConfigProperty> properties, IReadOnlyList<EditorConfigSection> sections)
            => (Properties, Sections) = (properties, sections);

        public IReadOnlyList<EditorConfigProperty> Properties { get; }
        public IReadOnlyList<EditorConfigSection> Sections { get; }

        public IEnumerable<EditorConfigProperty> GetMatchingProperties(string fileName) 
            => Sections
                .Where(section => Glob.Glob.IsMatch(fileName, section.Name))
                .SelectMany(section => section.Properties);

        public IEnumerable<EditorConfigProperty> GetMergedMatchingProperties(string fileName) 
            => GetMatchingProperties(fileName)
                .ToLookup(property => property.Name)
                .Select(grouping => grouping.Last())
                .ToList();
    }
}
