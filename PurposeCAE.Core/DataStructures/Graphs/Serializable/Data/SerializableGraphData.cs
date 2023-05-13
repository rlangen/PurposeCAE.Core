// Ignore Spelling: Serializer Json Serializable Uid

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;

internal class SerializableGraphData<T, U> where T : IEquatable<T>
{
    public ICollection<SerializableNode<T, U>> Nodes { get; init; } = new HashSet<SerializableNode<T, U>>();
    public int UidCounter { get; set; } = 0;
}