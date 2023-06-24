using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeRemovers;

internal interface INodeRemover
{
    void RemoveNode<T, U>(T data, IGraphComponentRegistry<T, U> graphComponentRegistry, ICollection<INode<T, U>> nodes, ICollection<INode<T, U>> roots, SerializableGraphData<T, U> graphData) where T : IEquatable<T>;
}