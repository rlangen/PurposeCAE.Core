using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Factories;

public interface IGraphFactory
{
    IGraph<T, U> Create<T, U>() where T : IEquatable<T>;
    IGraph<T, U> Create<T, U>(Stream stream, IJsonSerializationSettings serializationSettings) where T : IEquatable<T>;
}