using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;
using PurposeCAE.Core.DataStructures.Graphs.Nodes;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeRemovers;

internal class NodeRemover : INodeRemover
{
    /// <summary>
    /// Conducts the removal of a node from the graph. It also removes the node from the <paramref name="graphData"/> and the <paramref name="graphComponentRegistry"/>.
    /// Therefore it handles the removal at the following places:
    /// - <paramref name="graphComponentRegistry"/>
    /// - <paramref name="nodes"/>
    /// - <paramref name="roots"/>
    /// - <paramref name="graphData"/>
    /// - as well as in the child and parent nodes.
    /// </summary>
    /// <typeparam name="T">Node data</typeparam>
    /// <typeparam name="U">Edge data</typeparam>
    /// <param name="data">The unique data of the node which should be removed from the graph</param>
    /// <param name="graphComponentRegistry">From <see cref="IGraph{T, U}"/></param>
    /// <param name="nodes">From <see cref="IGraph{T, U}"/></param>
    /// <param name="roots">From <see cref="IGraph{T, U}"/></param>
    /// <param name="graphData">From <see cref="IGraph{T, U}"/></param>
    /// <exception cref="NotImplementedException">Occurs when the <see cref="INode{T, U}"/> object is not an <see cref="Node{T, U}"/></exception>
    public void RemoveNode<T, U>
        (
            T data,
            IGraphComponentRegistry<T, U> graphComponentRegistry,
            ICollection<INode<T, U>> nodes,
            ICollection<INode<T, U>> roots,
            SerializableGraphData<T, U> graphData
        ) where T : IEquatable<T>
    {
        if (!graphComponentRegistry.TryGetNode(data, out INode<T, U> foundNode))
            return;

        if (foundNode is not Node<T, U> castedNode)
            throw new NotImplementedException($"The method '{nameof(RemoveNode)}' can't handle the type '{foundNode.GetType()}'!");

        #region Data
        graphData.Nodes.Remove(castedNode.SerializableNode);

        foreach (IEdge<T, U> childEdge in foundNode.Children)
        {
            childEdge.TargetNode.RemoveParent(childEdge);

            if (!childEdge.TargetNode.Parents.Any())
                roots.Add(childEdge.TargetNode);
        }

        foreach (IEdge<T, U> parentEdge in foundNode.Parents)
        {
            INode<T, U> parentNode = parentEdge.SourceNode;
            parentNode.RemoveChild(parentEdge);
        }
        #endregion Data

        #region Graph

        if (roots.Contains(foundNode))
            roots.Remove(foundNode);
        nodes.Remove(foundNode);

        graphComponentRegistry.RemoveNode(data);
        #endregion Graph
    }
}