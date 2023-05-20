// Ignore Spelling: Serializer Json Serializable Uid

using System.Text.Json.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Data;

internal class SerializableNode<T, U> where T : IEquatable<T>
{
    public SerializableNode(int uid, T data)
    {
        Uid = uid;
        Data = data;
        Children = new HashSet<SerializableEdge<U>>();
    }
    [JsonConstructor]
    private SerializableNode(int uid, T data, HashSet<SerializableEdge<U>> children)
    {
        Uid = uid;
        Data = data;
        Children = children;
    }

    public int Uid { get; init; }
    public T Data { get; init; }

    public HashSet<SerializableEdge<U>> Children { get; init; }
}