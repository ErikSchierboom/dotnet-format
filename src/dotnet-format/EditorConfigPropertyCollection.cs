using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Format
{
    public sealed class EditorConfigPropertyCollection : IEnumerable<EditorConfigProperty>, IReadOnlyDictionary<string, string>
    {
        public static readonly EditorConfigPropertyCollection Empty = new EditorConfigPropertyCollection(Array.Empty<EditorConfigProperty>());
        
        private readonly IEnumerable<EditorConfigProperty> properties;
        private readonly Dictionary<string, string> dictionary;

        public EditorConfigPropertyCollection(IEnumerable<EditorConfigProperty> properties)
        {
            this.properties = properties;
            dictionary = properties.ToDictionary(property => property.Name, property => property.Value);
        }

        public EditorConfigPropertyCollection Merge(EditorConfigPropertyCollection other)
        {
            var mergedProperties = properties.ToList();

            foreach (var otherProperty in other)
            {
                if (!dictionary.ContainsKey(otherProperty.Name))
                    mergedProperties.Add(otherProperty);
            }

            return new EditorConfigPropertyCollection(mergedProperties);
        }

        public IEnumerator<EditorConfigProperty> GetEnumerator() => properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool ContainsKey(string key) => dictionary.ContainsKey(key);

        public bool TryGetValue(string key, out string value) => dictionary.TryGetValue(key, out value);

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator() => dictionary.GetEnumerator();

        public IEnumerable<string> Keys => dictionary.Keys;

        public IEnumerable<string> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public string this[string key] => dictionary[key];
    }
}
