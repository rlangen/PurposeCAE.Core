namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.Factories;

internal class GraphComponentRegistryFactory : IGraphComponentRegistryFactory
{
    public IGraphComponentRegistry<T, U> Create<T, U>() where T : IEquatable<T>
    {
        return new GraphComponentRegistry<T, U>();
    }
}