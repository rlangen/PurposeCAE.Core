using Moq;
using PurposeCAE.Core.DataStructures;
using PurposeCAE.Core.DataStructures.Graphs;

namespace PurposeCAE.Core.xTests.TestObjects;

public class NodeData : CustomEqualityComparerBase<NodeData>
{
    public NodeData(string name)
    {
        Name = name;
    }
    public string Name { get; init; }

    public static INode<NodeData, EdgeData> CreateNode(NodeData nodeData)
    {
        Mock<INode<NodeData, EdgeData>> mock = new();

        mock.Setup(x => x.Data).Returns(nodeData);
        mock.Setup(x => x.Parents).Returns(new List<IEdge<NodeData, EdgeData>>());
        mock.Setup(x => x.Children).Returns(new List<IEdge<NodeData, EdgeData>>());

        return mock.Object;
    }

    public override bool IsEqual(NodeData? other)
    {
        if (other is null) 
            return false;

        return Name.Equals(other.Name);
    }

    protected override int GetHash()
    {
        return Name.GetHashCode();
    }
}