namespace PurposeCAE.Core.DataStructures.Graphs;

public interface IEdge<T, U> where T : IEquatable<T>
{
    U EdgeData { get; }
    INode<T, U> SourceNode { get; }
    INode<T, U> TargetNode { get; }
}