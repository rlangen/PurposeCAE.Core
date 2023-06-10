namespace PurposeCAE.Core.DataStructures.Graphs.Graphs.SameComparer;

internal interface IGraphSameComparer
{
    bool IsSameAs<T, U>(IGraph<T, U> graphLeft, IGraph<T, U>  graphRight) where T : IEquatable<T>;
}