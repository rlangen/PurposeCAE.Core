using PurposeCAE.Core.DataStructures.Graphs.Serializable.Data;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries.Factories;
using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Serializable;

internal class Graph<T, U> : IGraph<T, U> where T : IEquatable<T>
{
    public Graph(IGraphComponentRegistryFactory graphComponentRegistryFactory)
    {
        _graphComponentRegistry = graphComponentRegistryFactory.Create<T, U>();
        _graph = new();
    }
    public Graph(IGraphComponentRegistryFactory graphComponentRegistryFactory, SerializableGraphOrganizer<T, U> graph)
    {
        _graph = graph;

        _graphComponentRegistry = graphComponentRegistryFactory.Create<T, U>();
        foreach (SerializableNode<T,U> node in graph.GraphData.Nodes)
            CreateNewNode(node);
    }

    public IEnumerable<INode<T, U>> Nodes { get { return _nodes; } }
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
        if(_graphComponentRegistry.NodeStorage.TryGetValue(data, out var node))
            return node ?? throw new NullReferenceException($"A node was returnedd by the '{nameof(_graphComponentRegistry)}' which is null.");

        SerializableNode<T, U> newSerializableNode = _graph.AddNode(data);

        return CreateNewNode(newSerializableNode);
    }
    public IEdge<T, U> AddEdge(T source, T target, U data)
    {
        if (!_graphComponentRegistry.NodeStorage.TryGetValue(source, out INode<T, U>? foundSource) || foundSource is null)
            throw new ArgumentException("The source node wasn't found while creating an edge. Was the node added before?");
        if(!_graphComponentRegistry.NodeStorage.TryGetValue(target, out INode<T, U>? foundTarget) || foundTarget is null)
            throw new ArgumentException("The target node wasn't found while creating an edge. Was the node added before?");

        if (foundSource is not Node<T, U> castedSource)
            throw new NotImplementedException($"There is no implementation for the type '{foundSource.GetType()}'");
        if(foundTarget is not Node<T, U> castedTarget)
            throw new NotImplementedException($"There is no implementation for the type '{foundTarget.GetType()}'");

        SerializableEdge<U> newSerializableEdge = _graph.AddEdge(castedSource.SerializableNode, castedTarget.SerializableNode, data);

        Edge<T, U> newEdge = new(_graphComponentRegistry, _graph.GraphData, newSerializableEdge);
        castedSource.AddChild(newEdge);

        // TODO: Implement Add Parent

        return newEdge;
    }

    public void Serialize(Stream stream, IJsonSerializationSettings serializationSettings)
    {
        _graph.Serialize(stream, serializationSettings);
    }

    private readonly SerializableGraphOrganizer<T, U> _graph;
    private readonly IGraphComponentRegistry<T, U> _graphComponentRegistry;

    private INode<T, U> CreateNewNode(SerializableNode<T, U> serializableNode)
    {
        INode<T, U> newNode = _graphComponentRegistry.CreateNode(_graph.GraphData, serializableNode);
        _nodes.Add(newNode);

        if (_roots is not null)
            if (!newNode.Parents.Any())
                _roots.Add(newNode);

        return newNode;
    }
}