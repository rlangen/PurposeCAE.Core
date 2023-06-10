using PurposeCAE.Core.DataStructures.Graphs;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.NodeGetters;
using PurposeCAE.Core.xTests.TestObjects;

namespace PurposeCAE.Core.xTests.UnitTests.DataStructures.Graphs.Graphs.Registries.NodeGetters;

public class NodeGetterTest
{
    [Fact]
    public void TryGetNode_NodeInStorage_ReturnsTrueAndNode()
    {
        // Arrange
        NodeData nodeData = new("node");
        INode<NodeData, EdgeData> node = NodeData.CreateNode(nodeData);

        IDictionary<NodeData, INode<NodeData, EdgeData>> nodeStorage = new Dictionary<NodeData, INode<NodeData, EdgeData>>() 
        {
            { nodeData, node }
        };

        NodeGetter nodeGetter = new();

        // Act
        bool result = nodeGetter.TryGetNode(nodeStorage, nodeData, out INode<NodeData, EdgeData>? foundNode);

        // Assert
        Assert.True(result);
        Assert.Equal(node, foundNode);
    }

    [Fact]
    public void TryGetNode_OtherNodeNotInStorage_ReturnsFalseAndNull()
    {
        // Arrange
        NodeData storedNodeData = new("storedNodeData");
        INode<NodeData, EdgeData> node = NodeData.CreateNode(storedNodeData);
        IDictionary<NodeData, INode<NodeData, EdgeData>> nodeStorage = new Dictionary<NodeData, INode<NodeData, EdgeData>>()
        {
            { storedNodeData, node }
        };
        NodeGetter nodeGetter = new();

        NodeData otherNode = new("otherNode");

        // Act
        bool result = nodeGetter.TryGetNode(nodeStorage, otherNode, out INode<NodeData, EdgeData>? foundNode);

        // Assert
        Assert.False(result);
        Assert.Null(foundNode);
    }

    [Fact]
    public void TryGetNode_NoNodeInStorage_ReturnsFalseAndNull()
    {
        // Arrange
        IDictionary<NodeData, INode<NodeData, EdgeData>> nodeStorage = new Dictionary<NodeData, INode<NodeData, EdgeData>>();

        NodeGetter nodeGetter = new();

        NodeData node = new("node");

        // Act
        bool result = nodeGetter.TryGetNode(nodeStorage, node, out INode<NodeData, EdgeData>? foundNode);

        // Assert
        Assert.False(result);
        Assert.Null(foundNode);
    }
}