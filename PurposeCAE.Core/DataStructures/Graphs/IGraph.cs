using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs;

public interface IGraph<T, U> where T : IEquatable<T>
{
    IEnumerable<INode<T, U>> Nodes { get; }
    IEnumerable<INode<T, U>> GetRoots();
    INode<T, U> AddNode(T data);
    IEdge<T, U> AddEdge(T source, T target, U data);
    void Serialize(Stream stream, IJsonSerializationSettings serializationSettings);
}