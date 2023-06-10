using PurposeCAE.Core.DataStructures.Graphs.Edges;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.EdgeAdders;

internal class EdgeAdder : IEdgeAdder
{
    private readonly IEdgeFactory _edgeFactory;

    public EdgeAdder(IEdgeFactory edgeFactory)
    {
        _edgeFactory = edgeFactory;
    }
    public IEdge<T, U> AddEdge<T, U>
        (
            IGraphComponentRegistry<T, U> graphComponentRegistry,
            ICollection<INode<T, U>> roots,
            T source,
            T target,
            U data
        ) where T : IEquatable<T>
    {
        // Check if source and target nodes are in the graph
        if (!graphComponentRegistry.TryGetNode(source, out INode<T, U> foundSource))
            throw new ArgumentException("The source node wasn't found while creating an edge. Was the node added before?");
        if (!graphComponentRegistry.TryGetNode(target, out INode<T, U> foundTarget))
            throw new ArgumentException("The target node wasn't found while creating an edge. Was the node added before?");

        // Check if the edge already exists
        foreach (IEdge<T, U> edge in foundSource.Children)
            if (edge.TargetNode.Equals(foundTarget))
                return edge;

        // A new edge object needs to be created
        IEdge<T, U> newEdge = _edgeFactory.CreateEdge(foundSource, foundTarget, data);
        foundSource.AddChild(newEdge);
        foundTarget.AddParent(newEdge);

        // The edge points to the target node, now. Therefore, the target node can't be a root node anymore.
        if (roots.Contains(foundTarget))
            roots.Remove(foundTarget);

        return newEdge;
    }
}