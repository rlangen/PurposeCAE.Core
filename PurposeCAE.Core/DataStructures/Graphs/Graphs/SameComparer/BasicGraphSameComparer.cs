namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.SameComparer;

/// <summary>
/// Provides a simple implementation of <see cref="IGraphSameComparer"/> which compares the node data, edge data and structure of two graphs.
/// The methods are only capable of small graphs, because they are not optimized.
/// For optimization dictionaries should be prefered before loops.
/// </summary>
internal class BasicGraphSameComparer : IGraphSameComparer
{
    public bool IsSameAs<T, U>(IGraph<T, U> graphLeft, IGraph<T, U> graphRight)
        where T : IEquatable<T>
    {
        IEnumerable<INode<T, U>> nodesLeft = graphLeft.Nodes;
        IEnumerable<INode<T, U>> nodesRight = graphRight.Nodes;
        if (nodesLeft.Count() != nodesRight.Count())
            return false;

        IEnumerable<INode<T, U>> rootsLeft = graphLeft.Roots;
        IEnumerable<INode<T, U>> rootsRight = graphRight.Roots;
        if (rootsLeft.Count() != rootsRight.Count())
            return false;

        if(!IsSameAs<T, U>(nodesLeft, nodesRight))
            return false;

        foreach (INode<T, U> rootLeft in rootsLeft)
            if(GetCorrectNode(rootLeft, rootsRight) is null)
                return false;

        return true;
    }

    private bool IsSameAs<T, U>(IEnumerable<INode<T, U>> nodesLeft, IEnumerable<INode<T, U>> nodesRight)
        where T : IEquatable<T>
    {
        foreach (INode<T, U> nodeLeft in nodesLeft)
        {
            INode<T, U>? nodeRight = GetCorrectNode(nodeLeft, nodesRight);
            if (nodeRight is null) // Node not found.
                return false;

            if (!IsSameAs<T, U>(nodeLeft.Parents, nodeRight.Parents))
                return false;

            if (!IsSameAs<T, U>(nodeLeft.Children, nodeRight.Children))
                return false;
        }
        return true;
    }
    private INode<T, U>? GetCorrectNode<T, U>(INode<T, U> searchObject, IEnumerable<INode<T, U>> nodes)
        where T : IEquatable<T>
    {
        foreach (INode<T, U> node in nodes)
            if (searchObject.Data.Equals(node.Data))
                return node;

        return null;
    }

    private bool IsSameAs<T, U>(IEnumerable<IEdge<T, U>> edgesLeft, IEnumerable<IEdge<T, U>> edgesRight)
        where T : IEquatable<T>
    {
        if (edgesLeft.Count() != edgesRight.Count())
            return false;

        foreach (IEdge<T, U> edgeLeft in edgesLeft)
        {
            bool edgeFound = false;
            for (int i = 0; i < edgesRight.Count(); i++)
            {
                IEdge<T, U> edgeRight = edgesRight.ElementAt(i);

                // The node sameness is already checked. Here we only need to fish the correct edge.
                if (edgeLeft.SourceNode.Data.Equals(edgeRight.SourceNode.Data)
                 && edgeLeft.TargetNode.Data.Equals(edgeRight.TargetNode.Data))
                {
                    edgeFound = true;

                    // TODO: Check edge data.
                    // The edge data should be checked memberwise, because the data type may not implement IEquatable.

                    break;
                }
            }
            if (!edgeFound)
                return false;
        }
        return true;
    }
}