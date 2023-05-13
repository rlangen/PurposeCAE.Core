using PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable;

internal class Node<T, U> : INode<T, U> where T : IEquatable<T>
{
    public Node(IGraphComponentRegistry<T, U> graphComponentFactory, SerializableGraphData<T, U> graphData, SerializableNode<T, U> node)
    {
        Data = node.Data;
        SerializableNode = node;

        foreach (SerializableEdge<U> edge in node.Children)
            _children.Add(new Edge<T, U>(graphComponentFactory, graphData, edge));
    }
    public T Data { get; }

    public IEnumerable<IEdge<T, U>> Parents { get; }

    public IEnumerable<IEdge<T, U>> Children { get { return _children; } }
    private readonly ICollection<IEdge<T, U>> _children = new List<IEdge<T, U>>();
    internal void AddChild(IEdge<T, U> edge) => _children.Add(edge);

    internal SerializableNode<T, U> SerializableNode { get; }
}