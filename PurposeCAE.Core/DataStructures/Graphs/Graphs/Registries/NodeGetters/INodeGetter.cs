namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.NodeGetters;

internal interface INodeGetter
{
    bool TryGetNode<T, U>(IDictionary<T, INode<T, U>> nodeStorage, T data, out INode<T, U> node) where T : IEquatable<T>;
}