using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs;

public interface IGraph<T, U> where T : IEquatable<T>
{
    IEnumerable<INode<T, U>> Nodes { get; }
    IEnumerable<INode<T, U>> Roots { get; }
    INode<T, U> AddNode(T data);
    IEdge<T, U> AddEdge(T source, T target, U data);
    void RemoveNode(T data);
    void Serialize(Stream stream, IJsonSerializationSettings serializationSettings);

    /// <summary>
    /// Returns true if this graph and the other graph have exactly the same node data, edge data and structure.
    /// </summary>
    /// <param name="otherGraph">The graph which should be compared with this graph.</param>
    /// <returns></returns>
    bool IsSameAs(IGraph<T, U> otherGraph);
}