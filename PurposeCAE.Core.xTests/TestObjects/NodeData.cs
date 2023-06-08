using Moq;
using PurposeCAE.Core.DataStructures.Graphs;

namespace PurposeCAE.Core.xTests.TestObjects;

public class NodeData : IEquatable<NodeData>
{
    public NodeData(string name)
    {
        Name = name;
    }
    public string Name { get; init; }

    public bool Equals(NodeData? other)
    {
        return other != null && Name == other.Name;
    }

    public static INode<NodeData, EdgeData> CreateNode(NodeData nodeData)
    {
        Mock<INode<NodeData, EdgeData>> mock = new();

        mock.Setup(x => x.Data).Returns(nodeData);
        mock.Setup(x => x.Parents).Returns(new List<IEdge<NodeData, EdgeData>>());
        mock.Setup(x => x.Children).Returns(new List<IEdge<NodeData, EdgeData>>());

        return mock.Object;
    }
}