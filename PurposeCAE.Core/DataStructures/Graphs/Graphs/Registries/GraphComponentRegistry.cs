using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Nodes;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

internal class GraphComponentRegistry<T, U> : IGraphComponentRegistry<T, U> where T : IEquatable<T>
{
    public INode<T, U> CreateNode(SerializableGraphData<T, U> graphData, SerializableNode<T, U> node)
    {
        if (NodeStorage.TryGetValue(node.Data, out INode<T, U>? foundNode))
            return foundNode ?? throw new NullReferenceException($"A node was returnedd by the '{nameof(NodeStorage)}' which is null.");

        Node<T, U> newNode = new(node);

        NodeStorage.Add(node.Data, newNode);

        return newNode;
    }

    public bool GetOrCreateNode(SerializableGraphData<T, U> graphData, T data, out INode<T, U> node)
    {
        if (NodeStorage.TryGetValue(data, out INode<T, U>? foundNode))
        {
            if (foundNode is null)
                throw new NullReferenceException($"A node was returnedd by the '{nameof(NodeStorage)}' which is null.");

            node = foundNode;

            return true;
        }

        SerializableNode<T, U> serializableNode = new(graphData.NextFreeUid++, data);
        graphData.Nodes.Add(serializableNode);

        Node<T, U> newNode = new(serializableNode);
        NodeStorage.Add(data, newNode);

        node = newNode;
        return true;
    }

    public IDictionary<T, INode<T, U>> NodeStorage { get; } = new Dictionary<T, INode<T, U>>();


    public SerializableNode<T, U> GetNode(int uid, SerializableGraphData<T, U> graphData)
    {
        if (_nodeUidStorage is null)
        {
            _nodeUidStorage = new Dictionary<int, SerializableNode<T, U>>();
            foreach (SerializableNode<T, U> node in graphData.Nodes)
                _nodeUidStorage.Add(node.Uid, node);
        }

        return _nodeUidStorage[uid];
    }

    private IDictionary<int, SerializableNode<T, U>>? _nodeUidStorage;
}