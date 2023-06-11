using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.Serialization;
using System.Text.Json;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.JsonSerializers;

internal class GraphToJsonSerializer : IGraphToJsonSerializer
{
    // TODO: Implement tests for polymorphic types
    public void Serialize<T, U>
        (
            Stream stream,
            IJsonSerializationSettings serializationSettings,
            SerializableGraphData<T, U> graphData
        ) where T : IEquatable<T>
    {
        JsonSerializer.Serialize(stream, graphData, serializationSettings.JsonSerializerOptions);
        stream.Flush();
    }
}