using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Edges;

internal class Edge<T, U> : IEdge<T, U> where T : IEquatable<T>
{
    public Edge(IGraphComponentRegistry<T, U> graphComponentRegistry, SerializableGraphData<T, U> graphData, SerializableEdge<U> edge, INode<T, U> sourceNode)
    {
        EdgeData = edge.EdgeData;

        SerializableNode<T, U> targetNode = graphComponentRegistry.GetNode(edge.TargetUid, graphData);

        SourceNode = sourceNode;
        TargetNode = graphComponentRegistry.CreateNode(graphData, targetNode);
        TargetNode.AddParent(this);
    }
    public Edge(INode<T, U> sourceNode, INode<T, U> targetNode, U edgeData)
    {
        SourceNode = sourceNode;
        TargetNode = targetNode;
        EdgeData = edgeData;

        SourceNode.AddChild(this);
        TargetNode.AddParent(this);
    }
    public U EdgeData { get; }

    public INode<T, U> SourceNode { get; }
    public INode<T, U> TargetNode { get; }
}