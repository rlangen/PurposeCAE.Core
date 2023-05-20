using PurposeCAE.Core.DataStructures.Graphs.Data;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

internal interface IGraphComponentRegistry<T, U> where T : IEquatable<T>
{
    IDictionary<T, INode<T, U>> NodeStorage { get; }
    INode<T, U> CreateNode(SerializableGraphData<T, U> graphData, SerializableNode<T, U> node);
    bool GetOrCreateNode(SerializableGraphData<T, U> graphData, T data, out INode<T, U> node);
    SerializableNode<T, U> GetNode(int uid, SerializableGraphData<T, U> graphData);
}