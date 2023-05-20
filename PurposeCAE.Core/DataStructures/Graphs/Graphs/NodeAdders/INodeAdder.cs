using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeAdders;

internal interface INodeAdder
{
    INode<T, U> AddNode<T, U>(T data, IGraphComponentRegistry<T, U> graphComponentRegistry, ICollection<INode<T, U>> nodes, SerializableGraphData<T, U> graphData) where T : IEquatable<T>;
}