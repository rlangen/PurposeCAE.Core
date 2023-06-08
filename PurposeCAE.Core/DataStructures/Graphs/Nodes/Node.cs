using PurposeCAE.Core.DataStructures.Graphs.Data;

namespace PurposeCAE.Core.DataStructures.Graphs.Nodes;

internal class Node<T, U> : INode<T, U> where T : IEquatable<T>
{
    public Node(SerializableNode<T, U> node)
    {
        Data = node.Data;
        SerializableNode = node;
    }
    public T Data { get; }

    public IEnumerable<IEdge<T, U>> Parents { get { return _parents; } }
    private readonly ICollection<IEdge<T, U>> _parents = new List<IEdge<T, U>>();
    public void AddParent(IEdge<T, U> parentEdge)
    {
        _parents.Add(parentEdge);
    }

    public IEnumerable<IEdge<T, U>> Children { get { return _children; } }
    private readonly ICollection<IEdge<T, U>> _children = new List<IEdge<T, U>>();
    public void AddChild(IEdge<T, U> childEdge)
    {
        Node<T, U> childNode = childEdge.TargetNode as Node<T, U> ?? throw new NotImplementedException();
        int childNodeUid = childNode.SerializableNode.Uid;

        SerializableEdge<U> serializableEdge = new(childEdge.EdgeData, childNodeUid);
        SerializableNode.Children.Add(serializableEdge);

        _children.Add(childEdge);
    }

    internal SerializableNode<T, U> SerializableNode { get; }
}