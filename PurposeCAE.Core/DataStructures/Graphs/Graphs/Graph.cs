using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Builders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.EdgeAdders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.JsonSerializers;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeAdders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.Factories;
using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs;

internal class Graph<T, U> : IGraph<T, U> where T : IEquatable<T>
{
    private readonly INodeAdder _nodeAdder;
    private readonly IEdgeAdder _edgeAdder;
    private readonly IGraphToJsonSerializer _graphToJsonSerializer;
    private readonly IGraphComponentRegistry<T, U> _graphComponentRegistry;
    private readonly SerializableGraphData<T, U> _graphData = new();

    #region Construction and Deconstruction
    public Graph
        (
            IGraphComponentRegistryFactory graphComponentRegistryFactory,
            INodeAdder nodeAdder,
            IEdgeAdder edgeAdder,
            IGraphToJsonSerializer graphToJsonSerializer
        )
    {
        _graphComponentRegistry = graphComponentRegistryFactory.Create<T, U>();
        _nodeAdder = nodeAdder;
        _edgeAdder = edgeAdder;
        _graphToJsonSerializer = graphToJsonSerializer;
        _graphData = new SerializableGraphData<T, U>();
    }
    public Graph
        (
            IGraphComponentRegistryFactory graphComponentRegistryFactory,
            IGraphBuilder graphBuilder,
            INodeAdder nodeAdder,
            IEdgeAdder edgeAdder,
            IGraphToJsonSerializer graphToJsonSerializer,
            SerializableGraphData<T, U> graphData
        ) 
        : this(graphComponentRegistryFactory, nodeAdder, edgeAdder, graphToJsonSerializer)
    {
        _graphData = graphData;

        graphBuilder.Build<T, U>(_graphData, _graphComponentRegistry, _nodes, _roots);
    }
    #endregion Construction and Deconstruction

    public IEnumerable<INode<T, U>> Nodes { get { return _nodes; } }
    private readonly ICollection<INode<T, U>> _nodes = new List<INode<T, U>>();

    public IEnumerable<INode<T, U>> Roots { get { return _roots; } }
    private readonly ICollection<INode<T, U>> _roots = new HashSet<INode<T, U>>();

    public INode<T, U> AddNode(T data)
    {
        return _nodeAdder.AddNode(data, _graphComponentRegistry, _nodes, _roots, _graphData);
    }

    public IEdge<T, U> AddEdge(T source, T target, U data)
    {
        return _edgeAdder.AddEdge(_graphComponentRegistry, _roots, source, target, data);
    }

    public void Serialize(Stream stream, IJsonSerializationSettings serializationSettings)
    {
        _graphToJsonSerializer.Serialize(stream, serializationSettings, _graphData);
    }
}