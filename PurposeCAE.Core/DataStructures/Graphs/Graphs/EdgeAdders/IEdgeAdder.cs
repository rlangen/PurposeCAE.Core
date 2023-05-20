using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.EdgeAdders;

internal interface IEdgeAdder
{
    IEdge<T, U> AddEdge<T, U>(IGraphComponentRegistry<T, U> graphComponentRegistry, ICollection<INode<T, U>> roots, T source, T target, U data) where T : IEquatable<T>;
}