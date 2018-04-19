using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Format
{
    public sealed class EditorConfigDocument
    {
        public static readonly EditorConfigDocument Empty = new EditorConfigDocument(Array.Empty<EditorConfigProperty>(), Array.Empty<EditorConfigSection>());

        public EditorConfigDocument(IReadOnlyList<EditorConfigProperty> properties, IReadOnlyList<EditorConfigSection> sections)
            => (Properties, Sections) = (properties, sections);

        // TODO: create separate entities
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

        public EditorConfigDocument Merge(EditorConfigDocument other) 
            => new EditorConfigDocument(Merge(Properties, other.Properties), Merge(Sections, other.Sections));

        private static IReadOnlyList<EditorConfigProperty> Merge(IReadOnlyList<EditorConfigProperty> left, IReadOnlyList<EditorConfigProperty> right)
        {
            var leftProperties = left.ToDictionary(property => property.Name, property => property.Value);
            var rightProperties = right.ToDictionary(property => property.Name, property => property.Value);

            foreach (var rightProperty in rightProperties)
            {
                if (!leftProperties.ContainsKey(rightProperty.Key))
                    leftProperties.Add(rightProperty.Key, rightProperty.Value);
            }

            return leftProperties.Select(keyValue => new EditorConfigProperty(keyValue.Key, keyValue.Value)).ToList();
        }

        private static IReadOnlyList<EditorConfigSection> Merge(IReadOnlyList<EditorConfigSection> left, IReadOnlyList<EditorConfigSection> right)
        {
            var leftSections = left.ToDictionary(property => property.Name, property => property.Properties);
            var rightSections = right.ToDictionary(property => property.Name, property => property.Properties);

            foreach (var rightSection in rightSections)
            {
                if (leftSections.ContainsKey(rightSection.Key))
                    leftSections[rightSection.Key] = Merge(leftSections[rightSection.Key], rightSections[rightSection.Key]);
                else
                    leftSections.Add(rightSection.Key, rightSection.Value);
            }

            return leftSections.Select(keyValue => new EditorConfigSection(keyValue.Key, keyValue.Value)).ToList();
        }
    }
}
