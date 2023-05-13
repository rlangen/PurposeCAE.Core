using PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries.Factories;

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable;

internal class Graph<T, U> : IGraph<T, U> where T : IEquatable<T>
{
    public Graph(IGraphComponentRegistryFactory graphComponentRegistryFactory, SerializableGraphOrganizer<T, U> graph)
    {
        _graph = graph;

        IGraphComponentRegistry<T, U> graphComponentRegistry = graphComponentRegistryFactory.Create<T, U>();
        foreach (SerializableNode<T,U> node in graph.GraphData.Nodes)
            _nodes.Add(graphComponentRegistry.CreateNode(graph.GraphData, node));
    }
    public IEnumerable<INode<T, U>> Nodes { get; }
    private readonly ICollection<INode<T, U>> _nodes = new List<INode<T, U>>();

    public IEnumerable<INode<T, U>> Roots 
    { 
        get
        {
            if(_roots is null)
                _roots = GetRootes();
            return _roots;
        } 
    }
    private ICollection<INode<T, U>>? _roots;
    private ICollection<INode<T, U>> GetRootes()
    {
        throw new NotImplementedException();
    }

    public INode<T, U> AddNode(T data)
    {
        throw new NotImplementedException();
    }

    private readonly SerializableGraphOrganizer<T, U> _graph;
}