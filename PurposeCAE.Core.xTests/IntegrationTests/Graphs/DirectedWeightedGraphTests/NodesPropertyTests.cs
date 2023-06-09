using PurposeCAE.Core.DataStructures.Graphs;
using PurposeCAE.Core.DataStructures.Graphs.Factories;
using PurposeCAE.Core.xTests.TestObjects;

namespace PurposeCAE.Core.xTests.IntegrationTests.Graphs.DirectedWeightedGraphTests;

public class NodesPropertyTests
{
    [Fact]
    public void Nodes_OneRootAndOneChildNode_ReturnsCorrectRoots()
    {
        // Arrange
        NodeData rootNodeData = new("rootNodeData");
        NodeData childNodeData = new("childNodeData");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();

        INode<NodeData, EdgeData> rootNode = graph.AddNode(rootNodeData);
        INode<NodeData, EdgeData> childNode = graph.AddNode(childNodeData);

        graph.AddEdge(rootNodeData, childNodeData, new EdgeData("edgeData"));

        // Act
        IEnumerable<INode<NodeData, EdgeData>> nodes = graph.Nodes;

        // Assert
        Assert.Equal(2, nodes.Count());
        Assert.Contains(rootNode, nodes);
        Assert.Contains(childNode, nodes);
    }
    [Fact]
    public void Nodes_TwoRootAndOneChildNode_ReturnsCorrectRoots()
    {
        // Arrange
        NodeData rootNodeData1 = new("rootNodeData1");
        NodeData rootNodeData2 = new("rootNodeData2");
        NodeData childNodeData = new("childNodeData");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();

        INode<NodeData, EdgeData> rootNode1 = graph.AddNode(rootNodeData1);
        INode<NodeData, EdgeData> rootNode2 = graph.AddNode(rootNodeData2);
        INode<NodeData, EdgeData> childNode = graph.AddNode(childNodeData);

        graph.AddEdge(rootNodeData1, childNodeData, new EdgeData("edgeData"));

        // Act
        IEnumerable<INode<NodeData, EdgeData>> nodes = graph.Nodes;

        // Assert
        Assert.Equal(3, nodes.Count());
        Assert.Contains(rootNode1, nodes);
        Assert.Contains(rootNode2, nodes);
        Assert.Contains(childNode, nodes);
    }
}