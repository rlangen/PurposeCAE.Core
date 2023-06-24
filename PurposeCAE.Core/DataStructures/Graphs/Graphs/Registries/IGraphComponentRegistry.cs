using PurposeCAE.Core.DataStructures.Graphs.Data;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

internal interface IGraphComponentRegistry<T, U> where T : IEquatable<T>
{
    bool TryGetNode(T data, out INode<T, U> node);
    INode<T, U> TranslateSerializableNode(SerializableNode<T, U> node);
    bool GetOrCreateNode(SerializableGraphData<T, U> graphData, T data, out INode<T, U> node);

    /// <summary>
    /// Removes a node from the registry. It doesn't influance the graph or its data substructure.
    /// </summary>
    /// <param name="data">The key for node which should be removed.</param>
    /// <exception cref="ArgumentException">Occurs when there is no node registered with the <paramref name="data"/> key.</exception>
    void RemoveNode(T data);
}