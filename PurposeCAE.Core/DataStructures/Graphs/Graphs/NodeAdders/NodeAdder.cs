using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeAdders;

internal class NodeAdder : INodeAdder
{
    public INode<T, U> AddNode<T, U>
        (
            T data,
            IGraphComponentRegistry<T, U> graphComponentRegistry,
            ICollection<INode<T, U>> nodes,
            ICollection<INode<T, U>> roots,
            SerializableGraphData<T, U> graphData
        ) where T : IEquatable<T>
    {
        if (graphComponentRegistry.GetOrCreateNode(graphData, data, out INode<T, U> foundOrCreatedNode))
            return foundOrCreatedNode;

        nodes.Add(foundOrCreatedNode);

        // A new node has no edges, yet. Therefore, it is a root node.
        roots.Add(foundOrCreatedNode);

        return foundOrCreatedNode;
    }
}