namespace DotNet.Format
{
    public sealed class EditorConfigDocument
    {
        public static readonly EditorConfigDocument Empty = new EditorConfigDocument(EditorConfigPropertyCollection.Empty, EditorConfigSectionCollection.Empty);

        public EditorConfigDocument(EditorConfigPropertyCollection properties, EditorConfigSectionCollection sections)
            => (Properties, Sections) = (properties, sections);

        public EditorConfigPropertyCollection Properties { get; }
        public EditorConfigSectionCollection Sections { get; }

        public bool IsRoot => Properties.TryGetValue("root", out var root) && bool.TryParse(root, out var isRoot) && isRoot;

        public EditorConfigDocument Merge(EditorConfigDocument other) 
            => new EditorConfigDocument(Properties.Merge(other.Properties), Sections.Merge(other.Sections));
    }
}
