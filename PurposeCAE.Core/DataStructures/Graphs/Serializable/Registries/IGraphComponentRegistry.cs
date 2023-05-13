using PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries;

internal interface IGraphComponentRegistry<T, U> where T : IEquatable<T>
{
    IDictionary<T, INode<T, U>> NodeStorage { get; }
    INode<T, U> CreateNode(SerializableGraphData<T, U> graphData, SerializableNode<T, U> node);
    SerializableNode<T, U> GetNode(int uid, SerializableGraphData<T, U> graphData);
}