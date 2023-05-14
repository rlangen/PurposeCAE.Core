using Autofac;
using PurposeCAE.Core.DataStructures.Graphs.Factories;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries;
using PurposeCAE.Core.DataStructures.Graphs.Serializable.Registries.Factories;
using PurposeCAE.Core.Serialization;

namespace PurposeCAE.Core.DataStructures.Graphs.DI;

public static class SerializableGraph
{
    public static void Inject(ContainerBuilder builder)
    {
        builder.RegisterType<GraphComponentRegistryFactory>().As<IGraphComponentRegistryFactory>()
            .SingleInstance();

        builder.RegisterType<GraphComponentRegistryFactory>().As<IGraphComponentRegistryFactory>()
            .SingleInstance();

        builder.RegisterType<GraphFactory>().As<IGraphFactory>()
            .SingleInstance();
    }
}