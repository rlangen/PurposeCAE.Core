using PurposeCAE.Core.DataStructures.Graphs;
using PurposeCAE.Core.DataStructures.Graphs.Factories;
using PurposeCAE.Core.xTests.TestObjects;
using Xunit;

namespace PurposeCAE.Core.xTests.IntegrationTests.Graphs.DirectedWeightedGraphTests;

public class SerializationTests
{
    [Fact]
    public void Deserialize_GraphWithOneNode_DeserializedIsCorrect()
    {
        // Arrange
        string serializedGraph = """
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
        using Stream serializedGraphStream = new MemoryStream();
        using StreamWriter streamWriter = new(serializedGraphStream, System.Text.Encoding.UTF8);
        streamWriter.Write(serializedGraph);
        streamWriter.Flush();
        serializedGraphStream.Position = 0;

        NodeData desiredNodeData = new("node");
        DirectedWeightedGraphFactory directedWeightedGraphFactory = new();
        IGraph<NodeData, EdgeData> desiredGraph = directedWeightedGraphFactory.Create<NodeData, EdgeData>();
        INode<NodeData, EdgeData> desiredNode = desiredGraph.AddNode(desiredNodeData);

        // Act
        IGraph<NodeData, EdgeData> actualGraph = 
            directedWeightedGraphFactory.Create<NodeData, EdgeData>(serializedGraphStream, new NonPolymorphicSerializationSettings());

        // Assert
        Assert.True(actualGraph.IsSameAs(desiredGraph));
    }
}