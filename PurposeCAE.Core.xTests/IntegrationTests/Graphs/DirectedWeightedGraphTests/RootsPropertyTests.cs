using PurposeCAE.Core.DataStructures.Graphs;
using PurposeCAE.Core.DataStructures.Graphs.Factories;
using PurposeCAE.Core.xTests.TestObjects;
using Xunit;

namespace PurposeCAE.Core.xTests.IntegrationTests.Graphs.DirectedWeightedGraphTests;

public class RootsPropertyTests
{
    [Fact]
    public void Roots_OneRootAndOneChildNode_ReturnsCorrectRoots()
    {
        // Arrange
        NodeData rootNodeData = new("rootNodeData");
        NodeData childNodeData = new("childNodeData");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();

        INode<NodeData, EdgeData> rootNode = graph.AddNode(rootNodeData);
        graph.AddNode(childNodeData);

        graph.AddEdge(rootNodeData, childNodeData, new EdgeData("edgeData"));

        // Act
        IEnumerable<INode<NodeData, EdgeData>> roots = graph.Roots;

        // Assert
        Assert.Single(roots);
        Assert.Equal(rootNode, roots.First());
    }
    [Fact]
    public void Roots_TwoRootAndOneChildNode_ReturnsCorrectRoots()
    {
        // Arrange
        NodeData rootNodeData1 = new("rootNodeData1");
        NodeData rootNodeData2 = new("rootNodeData2");
        NodeData childNodeData = new("childNodeData");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();

        INode<NodeData, EdgeData> rootNode1 = graph.AddNode(rootNodeData1);
        INode<NodeData, EdgeData> rootNode2 = graph.AddNode(rootNodeData2);
        graph.AddNode(childNodeData);

        graph.AddEdge(rootNodeData1, childNodeData, new EdgeData("edgeData"));

        // Act
        IEnumerable<INode<NodeData, EdgeData>> roots = graph.Roots;

        // Assert
        Assert.Equal(2, roots.Count());
        Assert.Contains(rootNode1, roots);
        Assert.Contains(rootNode2, roots);
    }
}