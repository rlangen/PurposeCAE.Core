using System.Text.Json.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Data;

internal class SerializableEdge<U>
{
    [JsonConstructor]
    public SerializableEdge(U edgeData, int targetUid)
    {
        EdgeData = edgeData;
        TargetUid = targetUid;
    }
    public U EdgeData { get; set; }
    public int TargetUid { get; set; }
}