namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.NodeGetters;

internal class NodeGetter : INodeGetter
{
    public bool TryGetNode<T, U>(IDictionary<T, INode<T, U>> nodeStorage, T data, out INode<T, U> node) where T : IEquatable<T>
    {
#pragma warning disable CS8601
        if (nodeStorage.TryGetValue(data, out node))
#pragma warning restore CS8601
        {
            if (node is null)
                throw new NullReferenceException($"A node was returnedd by the '{nameof(nodeStorage)}' which is null.");

            return true;
        }
        return false;
    }
}