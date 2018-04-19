namespace DotNet.Format
{
    public sealed class EditorConfigProperty
    {
        public EditorConfigProperty(string name, string value)
            => (Name, Value) = (name, value);

        public string Name { get; }
        public string Value { get; }
    }
}
