using PurposeCAE.Core.DataStructures.Graphs.Serializable;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries.Factories;
using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Factories;

internal class GraphFactory : IGraphFactory
{
    private readonly IGraphComponentRegistryFactory _graphComponentRegistryFactory;
    private readonly ISerializationOptions _serializationOptions;

    public GraphFactory(IGraphComponentRegistryFactory graphComponentRegistryFactory, ISerializationOptions serializationOptions)
    {
        _graphComponentRegistryFactory = graphComponentRegistryFactory;
        _serializationOptions = serializationOptions;
    }
    public IGraph<T, U> Create<T, U>() where T : IEquatable<T>
    {
        return new Graph<T, U>(_graphComponentRegistryFactory, _serializationOptions);
    }
}