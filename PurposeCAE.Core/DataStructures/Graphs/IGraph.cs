namespace PurposeCAE.Core.DataStructures.Graphs;

public interface IGraph<T, U> where T : IEquatable<T>
{
    IEnumerable<INode<T, U>> Nodes { get; }
    IEnumerable<INode<T, U>> Roots { get; }
    INode<T, U> AddNode(T data);
}