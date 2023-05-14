using PurposeCAE.Core.Serialization;
using System.Text.Json;

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;

internal class SerializableGraphOrganizer<T, U> where T : IEquatable<T>
{
    public SerializableGraphOrganizer()
    {
        GraphData = new();
    }

    public SerializableGraphOrganizer(SerializableGraphData<T, U> graphData)
    {
        GraphData = graphData;
    }

    public SerializableGraphData<T, U> GraphData { get; init; }

    public void Serialize(Stream stream, IJsonSerializationSettings serializationSettings)
    {
        JsonSerializer.Serialize(stream, GraphData, serializationSettings.JsonSerializerOptions);
        stream.Flush();
    }

    /// <summary>
    /// Checks if the graph contains a node with the given data. If so, the node is returned.
    /// Otherwise a new node is created and returned.
    /// </summary>
    /// <param name="data">Data which should be stored in the node.</param>
    /// <returns>Created or found node.</returns>
    public SerializableNode<T, U> AddNode(T data)
    {
        // Check if node already exists
        foreach (var node in GraphData.Nodes)
            if (node.Equals(data))
                return node;

        // Node does not exist, create new node
        SerializableNode<T, U> newNode = new(GraphData.NextFreeUid++, data);
        GraphData.Nodes.Add(newNode);

        return newNode;
    }

    /// <summary>
    /// Adds an edge between source and target node, if both nodes are part of the graph. 
    /// If the edge already exists, the existing edge is returned.
    /// </summary>
    /// <param name="source">The node from which the edge references.</param>
    /// <param name="target">The node which is referenced by the edge.</param>
    /// <param name="edgeData">Data like a weight, an amount, ...</param>
    /// <returns>The edge between source and target node.</returns>
    /// <exception cref="InvalidOperationException">If source or target are not part of this graph, this exception is thrown.</exception>
    public SerializableEdge<U> AddEdge(SerializableNode<T, U> source, SerializableNode<T, U> target, U edgeData)
    {
        if (!GraphData.Nodes.Contains(source))
            throw new InvalidOperationException("Source node does not exist in graph");
        if (!GraphData.Nodes.Contains(target))
            throw new InvalidOperationException("Target node does not exist in graph");

        // Check if edge already exists
        foreach (SerializableEdge<U> edge in source.Children)
            if (edge.TargetUid.Equals(target.Uid))
                return edge;

        // Edge does not exist, create new edge
        SerializableEdge<U> newEdge = new(edgeData, target.Uid);
        source.Children.Add(newEdge);

        return newEdge;
    }

    /// <summary>
    /// Removes the given edge from the graph.
    /// </summary>
    /// <param name="edge">The edge which should be removed.</param>
    /// <exception cref="InvalidOperationException">Is thrown if the given edge is not part of the graph.</exception>
    public void RemoveEdge(SerializableEdge<U> edge)
    {
        SerializableNode<T, U>? parentNode = null;

        // The edge don't reference the parent node, so we have to find it
        foreach (SerializableNode<T, U> node in GraphData.Nodes)
        {
            foreach (SerializableEdge<U> child in node.Children)
            {
                if (child == edge)
                {
                    parentNode = node;
                    break;
                }
            }
        }

        if (parentNode is null)
            throw new InvalidOperationException("Edge does not exist in graph");

        parentNode.Children.Remove(edge);
    }

    /// <summary>
    /// Removes the given node from the graph. 
    /// All edges that reference this node are also removed.
    /// </summary>
    /// <param name="node">The node which should be removed.</param>
    /// <exception cref="InvalidOperationException">Is thrown if the node isn't part of this graph.</exception>
    public void RemoveNode(SerializableNode<T, U> node)
    {
        if (!GraphData.Nodes.Contains(node))
            throw new InvalidOperationException("Node does not exist in graph");

        // Remove all edges that reference this node
        foreach (SerializableNode<T, U> otherNode in GraphData.Nodes)
        {
            foreach (SerializableEdge<U> edge in otherNode.Children)
            {
                if (edge.TargetUid.Equals(node.Uid))
                {
                    otherNode.Children.Remove(edge);
                    break;
                }
            }
        }

        // Remove node
        GraphData.Nodes.Remove(node);
    }
}