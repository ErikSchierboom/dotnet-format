namespace DotNet.Format
{
    public sealed class EditorConfigSection
    {
        public EditorConfigSection(string key, EditorConfigPropertyCollection properties)
            => (Name, Properties) = (key, properties);

        public string Name { get; }
        public EditorConfigPropertyCollection Properties { get; }
    }
}
