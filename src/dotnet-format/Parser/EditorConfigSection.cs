using System.Collections.Generic;

namespace DotNet.Format.Parser
{
    public sealed class EditorConfigSection
    {
        public EditorConfigSection(string key, IReadOnlyList<EditorConfigProperty> properties)
            => (Name, Properties) = (key, properties);

        public string Name { get; }
        public IReadOnlyList<EditorConfigProperty> Properties { get; }
    }
}
