// Ignore Spelling: Serializer Json Serializable Uid

using System.Text.Json.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;

internal class SerializableGraphData<T, U> where T : IEquatable<T>
{
    public SerializableGraphData()
    {
        Nodes = new HashSet<SerializableNode<T, U>>();
        NextFreeUid = 0;
    }
    [JsonConstructor]
    private SerializableGraphData(HashSet<SerializableNode<T, U>> nodes, int nextFreeUid)
    {
        Nodes = nodes;
        NextFreeUid = nextFreeUid;
    }
    public HashSet<SerializableNode<T, U>> Nodes { get; init; }
    public int NextFreeUid { get; set; }
}