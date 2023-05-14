namespace PurposeCAE.Core.DataStructures.Graphs;

public interface INode<T, U> where T : IEquatable<T>
{
    T Data { get; }
    IEnumerable<IEdge<T, U>> Parents { get; }
    IEnumerable<IEdge<T, U>> Children { get; }
    internal void AddParent(IEdge<T, U> parentEdge);
}