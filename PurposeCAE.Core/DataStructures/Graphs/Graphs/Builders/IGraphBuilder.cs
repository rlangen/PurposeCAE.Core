using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.Builders;

internal interface IGraphBuilder
{
    void Build<T, U>(SerializableGraphData<T, U> graphData, IGraphComponentRegistry<T, U> graphComponentRegistry, ICollection<INode<T, U>> nodes, ICollection<INode<T, U>> roots) where T : IEquatable<T>;
}