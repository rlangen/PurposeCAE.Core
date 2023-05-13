// Ignore Spelling: Serializer Json Serializable Uid

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;

internal class SerializableNode<T, U> where T : IEquatable<T>
{
    public SerializableNode(int uid, T data)
    {
        Uid = uid;
        Data = data;
    }

    public int Uid { get; init; }
    public T Data { get; init; }

    public ICollection<SerializableEdge<U>> Children { get; init; } = new HashSet<SerializableEdge<U>>();
}