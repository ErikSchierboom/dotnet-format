namespace DotNet.Format.Parser
{
    public sealed class EditorConfigSectionSyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigSectionSyntaxNode(string name) => Name = name;

        public string Name { get; }

        public override string ToString() => $"[{Name}]";
    }
}
