using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.JsonSerializers;

internal interface IGraphToJsonSerializer
{
    void Serialize<T, U>(Stream stream, IJsonSerializationSettings serializationSettings, SerializableGraphData<T, U> graphData) where T : IEquatable<T>;
}