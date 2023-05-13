namespace PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries.Factories;

internal class GraphComponentRegistryFactory : IGraphComponentRegistryFactory
{
    public IGraphComponentRegistry<T, U> Create<T, U>() where T : IEquatable<T>
    {
        return new GraphComponentRegistry<T, U>();
    }
}