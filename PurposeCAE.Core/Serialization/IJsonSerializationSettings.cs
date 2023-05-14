using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace PurposeCAE.Core.Serialization;

/// <summary>
/// Aggregates the <see cref="JsonSerializerOptions"/> and the derived types registry.
/// You can control things like intended write with the <see cref="JsonSerializerOptions"/>.
/// The derived types registry is imporant, if you want to serialize a polymorphic type.
/// To use this interface:
/// 1. Create an empty interface for your object which you want to serialize. E.g. 'IImportantDataSerializationSettings'. This is important, that the derived types are specific for the object and an other object serialization don't use the same (see point 5).
/// 2. Create a sealed class which implements the interface and inherits from <see cref="JsonSerializationSettingsBase"/>. E.g. 'ImportantDataSerializationSettings'.
/// 3. Create an object before the dependency injection.
/// 4. Register the derived types with the <see cref="AddDerivedTypes{T}(Type[])"/>.
/// 5. Register the object with the interface ('IImportantDataSerializationSettings') in the dependency injection.
/// </summary>
public interface IJsonSerializationSettings
{
    JsonSerializerOptions JsonSerializerOptions { get; }
    void AddDerivedTypes<T>(params Type[] derivedTypes);
}