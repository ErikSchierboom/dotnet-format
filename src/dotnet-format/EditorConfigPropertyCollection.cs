using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Format
{
    public sealed class EditorConfigPropertyCollection : IReadOnlyCollection<EditorConfigProperty>
    {
        public static readonly EditorConfigPropertyCollection Empty = new EditorConfigPropertyCollection(Array.Empty<EditorConfigProperty>());
        
        private readonly IReadOnlyCollection<EditorConfigProperty> properties;

        public EditorConfigPropertyCollection(IEnumerable<EditorConfigProperty> properties) 
            => this.properties = properties.ToArray();

        public EditorConfigPropertyCollection Merge(EditorConfigPropertyCollection other)
        {
            var propertyNames = new HashSet<string>(properties.Select(property => property.Name));
            var mergedProperties = properties.ToList();

            foreach (var otherProperty in other)
            {
                if (!propertyNames.Contains(otherProperty.Name))
                    mergedProperties.Add(otherProperty);
            }

            return new EditorConfigPropertyCollection(mergedProperties);
        }

        public int Count => properties.Count;

        public IEnumerator<EditorConfigProperty> GetEnumerator() => properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
