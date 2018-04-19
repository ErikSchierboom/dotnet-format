using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Format
{
    public sealed class EditorConfigSectionCollection : IEnumerable<EditorConfigSection>
    {
        public static readonly EditorConfigSectionCollection Empty = new EditorConfigSectionCollection(Array.Empty<EditorConfigSection>());

        private readonly IEnumerable<EditorConfigSection> sections;

        public EditorConfigSectionCollection(IEnumerable<EditorConfigSection> sections)
        {
            this.sections = sections;
        }

        public EditorConfigSectionCollection Merge(EditorConfigSectionCollection other)
        {   
            var thisAsDictionary = sections.ToDictionary(property => property.Name, property => property.Properties);
            var otherAsDictionary = other.ToDictionary(property => property.Name, property => property.Properties);

            var leftOnlySectionNames = new HashSet<string>(thisAsDictionary.Keys.Except(otherAsDictionary.Keys));
            var rightOnlySectionNames = new HashSet<string>(otherAsDictionary.Keys.Except(thisAsDictionary.Keys));

            var mergedSections = new List<EditorConfigSection>();

            foreach (var section in sections)
            {
                if (leftOnlySectionNames.Contains(section.Name))
                    mergedSections.Add(new EditorConfigSection(section.Name, section.Properties));
                else
                    mergedSections.Add(new EditorConfigSection(section.Name, section.Properties.Merge(otherAsDictionary[section.Name])));
            }   

            foreach (var otherSection in rightOnlySectionNames)
                mergedSections.Add(new EditorConfigSection(otherSection, otherAsDictionary[otherSection]));

            return new EditorConfigSectionCollection(mergedSections);
        }

        public IEnumerator<EditorConfigSection> GetEnumerator() => sections.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
