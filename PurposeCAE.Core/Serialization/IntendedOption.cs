using System.Text.Json;

namespace PurposeCAE.Core.Serialization;

public class IntendedOption : ISerializationOptions
{
    public JsonSerializerOptions JsonSerializerOptions
    {
        get
        {
            JsonSerializerOptions options = new();
            options.WriteIndented = true;
            return options;
        }
    }
}