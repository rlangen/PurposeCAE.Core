﻿using PurposeCAE.Core.DataStructures.Graphs;
using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Factories;
using PurposeCAE.Core.DataStructures.Graphs.Graphs;
using PurposeCAE.Core.Serialization.JsonStringsEqualityCheckers;
using PurposeCAE.Core.xTests.TestObjects;
using System.Reflection;
using System.Text.Json;

namespace PurposeCAE.Core.PurposeCAE.Core.xTests.Graphs.DirectedWeightedGraphTests;

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
}