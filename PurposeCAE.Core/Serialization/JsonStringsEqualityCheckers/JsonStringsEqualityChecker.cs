using System.Text.Json;

namespace PurposeCAE.Core.Serialization.JsonStringsEqualityCheckers;

/// <summary>
/// Provides methods to check if two json strings are equal.
/// All methods work on a text level, so they don't deserialize to specific objects.
/// </summary>
public static class JsonStringsEqualityChecker
{
    /// <summary>
    /// Allows to check if two json strings are equal.
    /// 
    /// The method doesn't deserialize to specific objects, but to json element types like dictionaries, arrays, strings, numbers, etc.
    /// If only the orders of items in enumerations and dictionaries differ in the two strings, "true" is still returned.
    /// </summary>
    public static bool AreEqual(string json1, string json2)
    {
        JsonElement? jsonElement1 = JsonSerializer.Deserialize<JsonElement>(json1);
        JsonElement? jsonElement2 = JsonSerializer.Deserialize<JsonElement>(json2);

        if (jsonElement1 is null || jsonElement2 is null)
            return jsonElement1 is null && jsonElement2 is null;

        return AreEqual(jsonElement1, jsonElement2);
    }

    private static bool AreEqual(object obj1, object obj2)
    {
        if (obj1.GetType() != obj2.GetType())
            return false;

        if (obj1 is JsonElement jsonElement1)
        {
            if (obj2 is not JsonElement jsonElement2)
                return false;

            if (jsonElement1.ValueKind != jsonElement2.ValueKind)
                return false;

            switch (jsonElement1.ValueKind)
            {
                // This value kind seems to be only dictionaries.
                case JsonValueKind.Object:
                    try
                    {
                        Dictionary<string, object>? desDict1 = JsonSerializer
                            .Deserialize<Dictionary<string, object>>(jsonElement1.GetRawText());
                        Dictionary<string, object>? desDict2 = JsonSerializer
                            .Deserialize<Dictionary<string, object>>(jsonElement2.GetRawText());

                        if (desDict1 is not null && desDict2 is not null)
                            return AreEqual(desDict1, desDict2);
                    }
                    catch (Exception) { }

                    return jsonElement1.GetRawText() == jsonElement2.GetRawText();

                case JsonValueKind.Array:
                    foreach (var item1 in jsonElement1.EnumerateArray())
                    {
                        bool areItemsEqual = false;

                        foreach (var item2 in jsonElement2.EnumerateArray())
                        {
                            if (AreEqual(item1, item2))
                            {
                                areItemsEqual = true;
                                break;
                            }
                        }

                        if (!areItemsEqual)
                            return false;
                    }
                    return true;

                case JsonValueKind.String:
                    return jsonElement1.GetString() == jsonElement2.GetString();

                case JsonValueKind.Number:
                    return jsonElement1.GetDouble() == jsonElement2.GetDouble();

                case JsonValueKind.True:
                    return true;

                case JsonValueKind.False:
                    return true;

                case JsonValueKind.Null:
                    return true;

                case JsonValueKind.Undefined:
                    return true;

                default:
                    throw new InvalidOperationException($"Unexpected value kind: {jsonElement1.ValueKind}");
            }
        }
        else if (obj1 is Dictionary<string, object> dict1 && obj2 is Dictionary<string, object> dict2)
        {
            if (dict1.Count != dict2.Count)
                return false;

            foreach (var key in dict1.Keys)
                if (!dict2.ContainsKey(key) || !AreEqual(dict1[key], dict2[key]))
                    return false;
        }
        else if (obj1 is IEnumerable<object> list1 && obj2 is IEnumerable<object> list2)
        {
            if (list1.Count() != list2.Count())
                return false;

            for (int i = 0; i < list1.Count(); i++)
                if (!AreEqual(list1.ElementAt(i), list2.ElementAt(i)))
                    return false;
        }
        else
            return obj1.Equals(obj2);

        return true;
    }
}