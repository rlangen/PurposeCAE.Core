using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PurposeCAE.Core.Serialization;

/// <summary>
/// See <see cref="IJsonSerializationSettings"/>.
/// </summary>
public abstract class JsonSerializationSettingsBase : IJsonSerializationSettings
{
    public JsonSerializationSettingsBase()
    {
        _polymorphicTypeResolver = new();

        JsonSerializerOptions = new()
        {
            TypeInfoResolver = _polymorphicTypeResolver,
            WriteIndented = true
        };
    }
    public JsonSerializerOptions JsonSerializerOptions { get; }

    public void AddDerivedTypes<T>(params Type[] derivedTypes)
    {
        _polymorphicTypeResolver.AddDerivedTypes<T>(derivedTypes);
    }

    private readonly PolymorphicTypeResolver _polymorphicTypeResolver;

    private class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

            if (_derivedTypesRegistry.TryGetValue(type, out IList<JsonDerivedType>? derivedTypes))
            {
                jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "$type",
                    IgnoreUnrecognizedTypeDiscriminators = true,
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
                };

                foreach (JsonDerivedType derivedType in derivedTypes)
                    jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(derivedType);
            }

            return jsonTypeInfo;
        }
        public void AddDerivedTypes<T>(params Type[] derivedTypes)
        {
            Type baseType = typeof(T);
            if (_derivedTypesRegistry.TryGetValue(baseType, out IList<JsonDerivedType>? derivedTypesList))
            {
                HashSet<Type> registeredDerivedTypes = new(derivedTypesList.Select(x => x.DerivedType));

                foreach (Type derivedType in derivedTypes)
                {
                    if (registeredDerivedTypes.Contains(derivedType))
                        continue;

                    derivedTypesList.Add(new JsonDerivedType(derivedType, derivedType.FullName ?? throw new NullReferenceException()));
                }
            }
            else
            {
                _derivedTypesRegistry.Add(baseType, new List<JsonDerivedType>());
                AddDerivedTypes<T>(derivedTypes);
            }
        }
        private readonly IDictionary<Type, IList<JsonDerivedType>> _derivedTypesRegistry = new Dictionary<Type, IList<JsonDerivedType>>();
    }
}