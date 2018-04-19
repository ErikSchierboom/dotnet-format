namespace DotNet.Format.Parser.Syntax
{
    public sealed class EditorConfigPropertySyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigPropertySyntaxNode(string name, string value) => (Name, Value) = (name, value);

        public string Name { get; }
        public string Value { get; }

        public override string ToString() => $"{Name} = {Value}";
    }
}
