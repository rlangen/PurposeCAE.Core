// Ignore Spelling: Serializer Json Serializable Uid

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;

internal class SerializableEdge<U>
{
    public SerializableEdge(U edgeInformation, int targetUid)
    {
        EdgeData = edgeInformation;
        TargetUid = targetUid;
    }
    public U EdgeData { get; set; }
    public int TargetUid { get; set; }
}