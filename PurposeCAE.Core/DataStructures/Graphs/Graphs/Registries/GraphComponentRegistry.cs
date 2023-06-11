using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.NodeGetters;
using PurposeCAE.Core.DataStructures.Graphs.Nodes;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

// TODO: Implement tests to guarantee functionality and performance
internal class GraphComponentRegistry<T, U> : IGraphComponentRegistry<T, U> where T : IEquatable<T>
{
    private readonly IDictionary<T, INode<T, U>> _nodeStorage = new Dictionary<T, INode<T, U>>();
    private readonly INodeGetter _nodeGetter;

    public GraphComponentRegistry(INodeGetter nodeGetter)
    {
        _nodeGetter = nodeGetter;
    }

    public INode<T, U> TranslateSerializableNode(SerializableNode<T, U> node)
    {
        if (TryGetNode(node.Data, out INode<T, U>? foundNode))
            return foundNode;

        return CreateNode(node);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graphData"></param>
    /// <param name="data"></param>
    /// <param name="node"></param>
    /// <returns>True, if the node is already part of the graph. False, if a new node was created.</returns>
    public bool GetOrCreateNode(SerializableGraphData<T, U> graphData, T data, out INode<T, U> node)
    {
        if(TryGetNode(data, out node))
            return true;

        SerializableNode<T, U> serializableNode = new(graphData.NextFreeUid++, data);
        graphData.Nodes.Add(serializableNode);

        node = CreateNode(serializableNode);

        return false;
    }

    public bool TryGetNode(T data, out INode<T, U> node)
    {
        return _nodeGetter.TryGetNode(_nodeStorage, data, out node);
    }
    private INode<T, U> CreateNode(SerializableNode<T, U> serializableNode)
    {
        Node<T, U> newNode = new(serializableNode);
        _nodeStorage.Add(serializableNode.Data, newNode);

        return newNode;
    }
}