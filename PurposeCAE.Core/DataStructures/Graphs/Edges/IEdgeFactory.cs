namespace PurposeCAE.Core.DataStructures.Graphs.Edges;

internal interface IEdgeFactory
{
    IEdge<T, U> CreateEdge<T, U>(INode<T, U> source, INode<T, U> target, U edgeData) where T : IEquatable<T>;
}