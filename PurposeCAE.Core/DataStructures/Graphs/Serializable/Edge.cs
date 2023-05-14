using PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable;

internal class Edge<T, U> : IEdge<T, U> where T : IEquatable<T>
{
    public Edge(IGraphComponentRegistry<T,U> graphComponentRegistry, SerializableGraphData<T,U> graphData, SerializableEdge<U> edge, INode<T, U> sourceNode)
    {
        EdgeData = edge.EdgeData;

        SerializableNode<T, U> targetNode = graphComponentRegistry.GetNode(edge.TargetUid, graphData);

        SourceNode = sourceNode;
        TargetNode = graphComponentRegistry.CreateNode(graphData, targetNode);
        TargetNode.AddParent(this);
    }
    public U EdgeData { get; }

    public INode<T, U> SourceNode { get; }
    public INode<T, U> TargetNode { get; }
}