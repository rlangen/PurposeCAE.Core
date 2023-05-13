using System.Text.Json;

namespace PurposeCAE.Core.Serialization;

public interface ISerializationOptions
{
    JsonSerializerOptions JsonSerializerOptions { get; }
}