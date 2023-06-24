using PurposeCAE.Core.DataStructures.Graphs.Factories;
using PurposeCAE.Core.DataStructures.Graphs;
using PurposeCAE.Core.xTests.TestObjects;
using PurposeCAE.Core.Serialization.JsonStringsEqualityCheckers;

namespace PurposeCAE.Core.xTests.IntegrationTests.Graphs.DirectedWeightedGraphTests;

public class RemoveNodeTests
{
    [Fact]
    public void AddOneAndRemoveIt_Nodes_Empty()
    {
        // Arrange
        NodeData nodeData = new("node");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> node = graph.AddNode(nodeData);

        // Act
        graph.RemoveNode(nodeData);

        // Assert
        Assert.Empty(graph.Nodes);
    }
    [Fact]
    public void AddOneAndRemoveIt_Roots_Empty()
    {
        // Arrange
        NodeData nodeData = new("node");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> node = graph.AddNode(nodeData);

        // Act
        graph.RemoveNode(nodeData);
        
        // Assert
        Assert.Empty(graph.Roots);
    }
    [Fact]
    public void AddTwoNodesRemoveOne_Nodes_OnlyTheRightLeft()
    {
        // Arrange
        NodeData nodeData1 = new("node1");
        NodeData nodeData2 = new("node2");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> node1 = graph.AddNode(nodeData1);
        INode<NodeData, EdgeData> node2 = graph.AddNode(nodeData2);

        // Act
        graph.RemoveNode(nodeData1);

        // Assert
        Assert.Single(graph.Nodes);
        Assert.Equal(node2, graph.Nodes.First());
    }
    [Fact]
    public void AddTwoRelatedNodesRemoveOne_Children_NoChild()
    {
        // Arrange
        NodeData nodeData1 = new("node1");
        NodeData nodeData2 = new("node2");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> node1 = graph.AddNode(nodeData1);
        INode<NodeData, EdgeData> node2 = graph.AddNode(nodeData2);
        graph.AddEdge(nodeData2, nodeData1, new EdgeData(""));

        // Act
        graph.RemoveNode(nodeData1);

        // Assert
        Assert.Empty(node2.Children);
    }
    [Fact]
    public void AddTwoRelatedNodesRemoveOne_Parents_NoParent()
    {
        // Arrange
        NodeData nodeData1 = new("node1");
        NodeData nodeData2 = new("node2");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> node1 = graph.AddNode(nodeData1);
        INode<NodeData, EdgeData> node2 = graph.AddNode(nodeData2);
        graph.AddEdge(nodeData1, nodeData2, new EdgeData(""));

        // Act
        graph.RemoveNode(nodeData1);

        // Assert
        Assert.Empty(node2.Parents);
    }
    [Fact]
    public void AddTwoRelatedNodesRemoveOne_Roots_StaysRoot()
    {
        // Arrange
        NodeData nodeData1 = new("node1");
        NodeData nodeData2 = new("node2");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> node1 = graph.AddNode(nodeData1);
        INode<NodeData, EdgeData> node2 = graph.AddNode(nodeData2);
        graph.AddEdge(nodeData1, nodeData2, new EdgeData(""));

        // Act
        graph.RemoveNode(nodeData2);

        // Assert
        Assert.Equal(graph.Roots.First(), node1);
    }
    [Fact]
    public void AddTwoRelatedNodesRemoveOne_Roots_BecomesRoot()
    {
        // Arrange
        NodeData nodeData1 = new("node1");
        NodeData nodeData2 = new("node2");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> node1 = graph.AddNode(nodeData1);
        INode<NodeData, EdgeData> node2 = graph.AddNode(nodeData2);
        graph.AddEdge(nodeData1, nodeData2, new EdgeData(""));

        // Act
        graph.RemoveNode(nodeData1);

        // Assert
        Assert.Equal(graph.Roots.First(), node2);
    }
    [Fact]
    public void AddThreeRelatedNodesRemoveOne_Children_CorrectChildren()
    {
        // Arrange
        NodeData nodeData1 = new("node1");
        NodeData nodeData2 = new("node2");
        NodeData nodeData3 = new("node3");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> node1 = graph.AddNode(nodeData1);
        INode<NodeData, EdgeData> node2 = graph.AddNode(nodeData2);
        INode<NodeData, EdgeData> node3 = graph.AddNode(nodeData3);
        graph.AddEdge(nodeData1, nodeData2, new EdgeData(""));
        graph.AddEdge(nodeData1, nodeData3, new EdgeData(""));

        // Act
        graph.RemoveNode(nodeData2);

        // Assert
        Assert.Single(node1.Children);
        Assert.Equal(node1.Children.First().TargetNode, node3);

        Assert.Single(node3.Parents);
        Assert.Equal(node3.Parents.First().SourceNode, node1);
    }

    [Fact]
    public void AddNode_RemoveNode_EmptySerialized()
    {
        // Arrange
        NodeData nodeData = new("node");
        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();

        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        graph.AddNode(nodeData);

        string desiredValue = """
            {
                "Nodes": [],
                "NextFreeUid": 1
            }
            """;
        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        using MemoryStream serializedGraphStream = new();
        string serializedGraph;

        // Act
        graph.RemoveNode(nodeData);

        // Assert
        graph.Serialize(serializedGraphStream, new NonPolymorphicSerializationSettings());
        serializedGraphStream.Position = 0;
        using StreamReader reader = new(serializedGraphStream);
        serializedGraph = reader.ReadToEnd();

        Assert.True(jsonStringsEqualityChecker.AreEqual(desiredValue, serializedGraph));
    }
    [Fact]
    public void AddTwoRelatedNodes_RemoveChild_NoChildInSerialized()
    {
        // Arrange
        NodeData nodeData1 = new("node1");
        NodeData nodeData2 = new("node2");
        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();

        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        graph.AddNode(nodeData1);
        graph.AddNode(nodeData2);
        graph.AddEdge(nodeData1, nodeData2, new EdgeData(""));

        string desiredValue = """
            {
              "Nodes": [
                {
                  "Uid": 0,
                  "Data": {
                    "Name": "node1"
                  },
                  "Children": []
                }
              ],
              "NextFreeUid": 2
            }
            """;

        // Act
        graph.RemoveNode(nodeData2);

        // Assert
        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        using MemoryStream serializedGraphStream = new();
        graph.Serialize(serializedGraphStream, new NonPolymorphicSerializationSettings());
        serializedGraphStream.Position = 0;
        using StreamReader reader = new(serializedGraphStream);
        string serializedGraph = serializedGraph = reader.ReadToEnd();

        Assert.True(jsonStringsEqualityChecker.AreEqual(desiredValue, serializedGraph));
    }
}