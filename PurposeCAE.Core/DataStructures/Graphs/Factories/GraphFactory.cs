using PurposeCAE.Core.DataStructures.Graphs.Serializable;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries.Factories;
using PurposeCAE.Core.Serialization;
using System.Text.Json;

namespace PurposeCAE.Core.DataStructures.Graphs.Factories;

internal class GraphFactory : IGraphFactory
{
    private readonly IGraphComponentRegistryFactory _graphComponentRegistryFactory;

    public GraphFactory(IGraphComponentRegistryFactory graphComponentRegistryFactory)
    {
        _graphComponentRegistryFactory = graphComponentRegistryFactory;
    }
    public IGraph<T, U> Create<T, U>() where T : IEquatable<T>
    {
        return new Graph<T, U>(_graphComponentRegistryFactory);
    }

    public IGraph<T, U> Create<T, U>(Stream stream, IJsonSerializationSettings serializationSettings) where T : IEquatable<T>
    {
        stream.Position = 0;
        using StreamReader reader = new(stream);
        string serializedGraph = reader.ReadToEnd();

        SerializableGraphData<T, U>? graphData = JsonSerializer.Deserialize<SerializableGraphData<T, U>>(serializedGraph, serializationSettings.JsonSerializerOptions);
        if (graphData is null)
            throw new JsonException("The deserialization returned a null reference.");

        SerializableGraphOrganizer<T, U> graphDataOrganizer = new(graphData);

        return new Graph<T, U>(_graphComponentRegistryFactory, graphDataOrganizer);
    }
}