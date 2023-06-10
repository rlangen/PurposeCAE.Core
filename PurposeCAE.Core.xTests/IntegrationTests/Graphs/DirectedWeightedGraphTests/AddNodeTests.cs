using PurposeCAE.Core.DataStructures.Graphs;
using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Factories;
using PurposeCAE.Core.DataStructures.Graphs.Graphs;
using PurposeCAE.Core.Serialization.JsonStringsEqualityCheckers;
using PurposeCAE.Core.xTests.TestObjects;

namespace PurposeCAE.Core.xTests.IntegrationTests.Graphs.DirectedWeightedGraphTests;

public class AddNodeTests
{
    [Fact]
    public void AddNode_OnlyOneNode_SerializedIsCorrect()
    {
        // Arrange
        NodeData nodeData = new("node");
        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();

        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();

        string desiredValue = """
            {
                "Nodes": [
                  {
                    "Uid": 0,
                    "Data": {
                      "Name": "node"
                    },
                    "Children": []
                  }
                ],
                "NextFreeUid": 1
            }
            """;
        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        using MemoryStream serializedGraphStream = new();
        string serializedGraph;

        // Act
        graph.AddNode(nodeData);

        graph.Serialize(serializedGraphStream, new NonPolymorphicSerializationSettings());
        serializedGraphStream.Position = 0;
        using StreamReader reader = new(serializedGraphStream);
        serializedGraph = reader.ReadToEnd();

        // Assert
        Assert.True(jsonStringsEqualityChecker.AreEqual(desiredValue, serializedGraph));
    }
    [Fact]
    public void AddNode_TwoNodes_SerializedIsCorrect()
    {
        // Arrange
        NodeData nodeData1 = new("node1");
        NodeData nodeData2 = new("node2");
        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();

        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();

        string desiredValue = """
            {
                "Nodes": [
                  {
                    "Uid": 0,
                    "Data": {
                      "Name": "node1"
                    },
                    "Children": [
                      {
                        "EdgeData": {
                          "Name": "edgeName"
                        },
                        "TargetUid": 1
                      }
                    ]
                  },
                  {
                    "Uid": 1,
                    "Data": {
                      "Name": "node2"
                    },
                    "Children": []
                  }
                ],
                "NextFreeUid": 2
            }
            """;
        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        using MemoryStream serializedGraphStream = new();
        string serializedGraph;

        // Act
        graph.AddNode(nodeData1);
        graph.AddNode(nodeData2);
        graph.AddEdge(nodeData1, nodeData2, new EdgeData("edgeName"));

        graph.Serialize(serializedGraphStream, new NonPolymorphicSerializationSettings());
        serializedGraphStream.Position = 0;
        using StreamReader reader = new(serializedGraphStream);
        serializedGraph = reader.ReadToEnd();

        // Assert
        Assert.True(jsonStringsEqualityChecker.AreEqual(desiredValue, serializedGraph));
    }

    [Fact]
    public void AddNode_TwoNodesWithSameNodeData_AddOnlyOnce()
    {
        // Arrange
        NodeData nodeData1 = new("node");
        NodeData nodeData2 = new("node");

        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> graph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();

        // Act
        INode<NodeData, EdgeData> node1 = graph.AddNode(nodeData1);
        INode<NodeData, EdgeData> node2 = graph.AddNode(nodeData2);

        // Assert
        Assert.Single(graph.Nodes);
        Assert.Equal(node1, node2);
        Assert.Same(node1, node2);
    }
}