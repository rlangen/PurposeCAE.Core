using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Builders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.EdgeAdders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.JsonSerializers;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeAdders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeRemovers;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.Factories;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.SameComparer;
using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.Graphs;

internal class Graph<T, U> : IGraph<T, U> where T : IEquatable<T>
{
    private readonly INodeAdder _nodeAdder;
    private readonly INodeRemover _nodeRemover;
    private readonly IEdgeAdder _edgeAdder;
    private readonly IGraphToJsonSerializer _graphToJsonSerializer;
    private readonly IGraphSameComparer _graphSameComparer;
    private readonly IGraphComponentRegistry<T, U> _graphComponentRegistry;
    private readonly SerializableGraphData<T, U> _graphData = new();

    #region Construction and Deconstruction
    public Graph
        (
            IGraphComponentRegistryFactory graphComponentRegistryFactory,
            INodeAdder nodeAdder,
            INodeRemover nodeRemover,
            IEdgeAdder edgeAdder,
            IGraphToJsonSerializer graphToJsonSerializer,
            IGraphSameComparer graphSameComparer
        )
    {
        _graphComponentRegistry = graphComponentRegistryFactory.Create<T, U>();
        _nodeAdder = nodeAdder;
        _nodeRemover = nodeRemover;
        _edgeAdder = edgeAdder;
        _graphToJsonSerializer = graphToJsonSerializer;
        _graphSameComparer = graphSameComparer;
        _graphData = new SerializableGraphData<T, U>();
    }
    public Graph
        (
            IGraphComponentRegistryFactory graphComponentRegistryFactory,
            IGraphBuilder graphBuilder,
            INodeAdder nodeAdder,
            INodeRemover nodeRemover,
            IEdgeAdder edgeAdder,
            IGraphToJsonSerializer graphToJsonSerializer,
            IGraphSameComparer graphSameComparer,
            SerializableGraphData<T, U> graphData
        ) 
        : this(graphComponentRegistryFactory, nodeAdder, nodeRemover, edgeAdder, graphToJsonSerializer, graphSameComparer)
    {
        _graphSameComparer = graphSameComparer;
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

    public bool IsSameAs(IGraph<T, U> otherGraph)
    {
        return _graphSameComparer.IsSameAs(this, otherGraph);
    }

    public void RemoveNode(T data)
    {
        _nodeRemover.RemoveNode(data, _graphComponentRegistry, _nodes, _roots, _graphData);
    }
}