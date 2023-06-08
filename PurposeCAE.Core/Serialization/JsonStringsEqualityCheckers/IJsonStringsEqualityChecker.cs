namespace PurposeCAE.Core.Serialization.JsonStringsEqualityCheckers;

/// <summary>
/// Provides methods to check if two json strings are equal.
/// All methods work on a text level, so they don't deserialize to specific objects.
/// </summary>
public interface IJsonStringsEqualityChecker
{
    /// <summary>
    /// Allows to check if two json strings are equal.
    /// 
    /// The method doesn't deserialize to specific objects, but to json element types like dictionaries, arrays, strings, numbers, etc.
    /// If only the orders of items in enumerations and dictionaries differ in the two strings, "true" is still returned.
    /// </summary>
    bool AreEqual(string json1, string json2);
}