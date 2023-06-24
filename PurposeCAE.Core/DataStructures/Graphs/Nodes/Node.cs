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
    public void RemoveParent(IEdge<T, U> parentEdge)
    {
        _parents.Remove(parentEdge);
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
    public void AddChildWithoutSerializableEdgeCreation(IEdge<T, U> childEdge)
    {
        _children.Add(childEdge);
    }

    /// <summary>
    /// Removes a child from this node.
    /// </summary>
    /// <param name="childEdge">The edge which should be removed.</param>
    /// <exception cref="NotImplementedException">Occurs when the target node of the <paramref name="childEdge"/> is not an <see cref="Node{T, U}"/>. </exception>
    /// <exception cref="ArgumentException">Occurs when a <paramref name="childEdge"/> tried to be deleted, which was not part of <see cref="SerializableNode{T, U}.Children"/></exception>
    public void RemoveChild(IEdge<T, U> childEdge)
    {
        _children.Remove(childEdge);

        #region Data
        if (childEdge.TargetNode is not Node<T, U> childNode)
            throw new NotImplementedException($"The method '{nameof(RemoveChild)}' doesn't support the type '{childEdge.TargetNode.GetType()}'");

        int childNodeUid = childNode.SerializableNode.Uid;
        SerializableEdge<U>? edgeToBeRemoved = SerializableNode.Children.FirstOrDefault(edge => edge.TargetUid == childNodeUid);
        if (edgeToBeRemoved is default(SerializableEdge<U>?))
            throw new ArgumentException("Tried to delete a child, but it was not in the children property!");

        SerializableNode.Children.Remove(edgeToBeRemoved); 
        #endregion
    }

    internal SerializableNode<T, U> SerializableNode { get; }
}