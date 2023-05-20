using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Edges;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Builders;

internal class GraphBuilder : IGraphBuilder
{
    private readonly IEdgeFactory _edgeFactory;

    public GraphBuilder(IEdgeFactory edgeFactory)
    {
        _edgeFactory = edgeFactory;
    }
    public void Build<T, U>
        (
            SerializableGraphData<T, U> graphData,
            IGraphComponentRegistry<T, U> graphComponentRegistry,
            ICollection<INode<T, U>> nodes,
            ICollection<INode<T, U>> roots
        ) where T : IEquatable<T>
    {
        // Translate the serializable graph data into the graph data structure
        // Furthermore, create a registry of all nodes and their UIDs. This is needed for performance reasons.
        IDictionary<int, INode<T, U>> uidNodePairs = new Dictionary<int, INode<T, U>>();
        foreach (SerializableNode<T, U> node in graphData.Nodes)
        {
            INode<T, U> translatedNode = graphComponentRegistry.CreateNode(graphData, node);
            nodes.Add(translatedNode);
            uidNodePairs.Add(node.Uid, translatedNode);
        }

        // Build the structure of the graph
        foreach (SerializableNode<T, U> node in graphData.Nodes)
        {
            INode<T, U> parentNode = uidNodePairs[node.Uid];
            foreach (SerializableEdge<U> edge in node.Children)
            {
                INode<T, U> childNode = uidNodePairs[edge.TargetUid];
                _edgeFactory.CreateEdge(parentNode, childNode, edge.EdgeData);
            }
        }

        // Build the roots storage
        foreach (INode<T, U> node in nodes)
            if (!node.Parents.Any())
                roots.Add(node);
    }
}