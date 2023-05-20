namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.Factories;

internal interface IGraphComponentRegistryFactory
{
    IGraphComponentRegistry<T, U> Create<T, U>() where T : IEquatable<T>;
}