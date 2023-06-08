using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.NodeGetters;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.Factories;

internal class GraphComponentRegistryFactory : IGraphComponentRegistryFactory
{
    private readonly INodeGetter _nodeGetter;

    public GraphComponentRegistryFactory(INodeGetter nodeGetter)
    {
        _nodeGetter = nodeGetter;
    }
    public IGraphComponentRegistry<T, U> Create<T, U>() where T : IEquatable<T>
    {
        return new GraphComponentRegistry<T, U>(_nodeGetter);
    }
}