namespace PurposeCAE.Core.DataStructures.Graphs.Factories;

public interface IGraphFactory
{
    IGraph<T, U> Create<T, U>() where T : IEquatable<T>;
}