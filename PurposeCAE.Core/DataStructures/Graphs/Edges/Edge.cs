using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Edges;

internal class Edge<T, U> : IEdge<T, U> where T : IEquatable<T>
{
    public Edge(INode<T, U> sourceNode, INode<T, U> targetNode, U edgeData)
    {
        SourceNode = sourceNode;
        TargetNode = targetNode;
        EdgeData = edgeData;
    }
    public U EdgeData { get; }

    public INode<T, U> SourceNode { get; }
    public INode<T, U> TargetNode { get; }
}