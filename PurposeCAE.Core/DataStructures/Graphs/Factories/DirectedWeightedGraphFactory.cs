using Autofac;
using PurposeCAE.Core.DataStructures.Graphs.Data;
using PurposeCAE.Core.DataStructures.Graphs.Edges;
using PurposeCAE.Core.DataStructures.Graphs.Graphs;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Builders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.EdgeAdders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.JsonSerializers;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeAdders;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.NodeRemovers;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.Factories;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.Registries.NodeGetters;
using PurposeCAE.Core.DataStructures.Graphs.Graphs.SameComparer;
using PurposeCAE.Core.Serialization;
using System.Text.Json;

namespace PurposeCAE.Core.DataStructures.Graphs.Factories;

public class DirectedWeightedGraphFactory : IGraphFactory
{
    private readonly IContainer _container;
    public DirectedWeightedGraphFactory()
    {
        ContainerBuilder builder = new();

        builder.RegisterType<EdgeFactory>().As<IEdgeFactory>()
            .SingleInstance();

        builder.RegisterType<GraphBuilder>().As<IGraphBuilder>()
            .SingleInstance();

        builder.RegisterType<EdgeAdder>().As<IEdgeAdder>()
            .SingleInstance();

        builder.RegisterType<GraphToJsonSerializer>().As<IGraphToJsonSerializer>()
            .SingleInstance();

        builder.RegisterType<NodeAdder>().As<INodeAdder>()
            .SingleInstance();
        builder.RegisterType<NodeRemover>().As<INodeRemover>()
            .SingleInstance();

        builder.RegisterType<GraphComponentRegistryFactory>().As<IGraphComponentRegistryFactory>()
            .SingleInstance();

        builder.RegisterType<NodeGetter>().As<INodeGetter>()
            .SingleInstance();

        builder.RegisterType<BasicGraphSameComparer>().As<IGraphSameComparer>()
            .SingleInstance();

        _container = builder.Build();
    }

    public IGraph<T, U> Create<T, U>() where T : IEquatable<T>
    {
        return new Graph<T, U>
            (
                _container.Resolve<IGraphComponentRegistryFactory>(),
                _container.Resolve<INodeAdder>(),
                _container.Resolve<INodeRemover>(),
                _container.Resolve<IEdgeAdder>(),
                _container.Resolve<IGraphToJsonSerializer>(),
                _container.Resolve<IGraphSameComparer>()
            );
    }

    public IGraph<T, U> Create<T, U>(Stream stream, IJsonSerializationSettings serializationSettings) where T : IEquatable<T>
    {
        stream.Position = 0;
        using StreamReader reader = new(stream);
        string serializedGraph = reader.ReadToEnd();

        SerializableGraphData<T, U>? graphData = JsonSerializer.Deserialize<SerializableGraphData<T, U>>(serializedGraph, serializationSettings.JsonSerializerOptions);
        if (graphData is null)
            throw new JsonException("The deserialization returned a null reference.");

        return new Graph<T, U>
            (
                _container.Resolve<IGraphComponentRegistryFactory>(),
                _container.Resolve<IGraphBuilder>(),
                _container.Resolve<INodeAdder>(),
                _container.Resolve<INodeRemover>(),
                _container.Resolve<IEdgeAdder>(),
                _container.Resolve<IGraphToJsonSerializer>(),
                _container.Resolve<IGraphSameComparer>(),
                graphData
            );
    }
}