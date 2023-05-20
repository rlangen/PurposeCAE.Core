namespace PurposeCAE.Core.DataStructures.Graphs.Edges;

internal class EdgeFactory : IEdgeFactory
{
    public IEdge<T, U> CreateEdge<T, U>(INode<T, U> source, INode<T, U> target, U edgeData) where T : IEquatable<T>
    {
        return new Edge<T, U>(source, target, edgeData);
    }
}