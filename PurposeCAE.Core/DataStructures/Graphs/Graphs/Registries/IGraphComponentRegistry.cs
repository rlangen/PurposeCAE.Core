using PurposeCAE.Core.DataStructures.Graphs.Data;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

internal interface IGraphComponentRegistry<T, U> where T : IEquatable<T>
{
    bool TryGetNode(T data, out INode<T, U> node);
    INode<T, U> TranslateSerializableNode(SerializableNode<T, U> node);
    bool GetOrCreateNode(SerializableGraphData<T, U> graphData, T data, out INode<T, U> node);
}