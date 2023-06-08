using PurposeCAE.Core.Serialization.JsonStringsEqualityCheckers;

namespace PurposeCAE.Core.xTests.IntegrationTests;

public class JsonStringsEqualityCheckerTest
{
    [Fact]
    public void NestedDictionaries_AreEqual()
    {
        // Arrange
        string json1 = """
            {
                "Key2": {
                    "Key4": [
                        "Value4",
                        {
                            "Key5": "Value5"
                        }
                    ]
                }
            }
            """;
        string json2 = json1;

        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        // Act
        bool areEqual = jsonStringsEqualityChecker.AreEqual(json1, json2);

        // Assert
        Assert.True(areEqual);
    }
    [Fact]
    public void NestedDictionaries_UnequalValue_AreNotEqual()
    {
        // Arrange
        string json1 = """
            {
                "Key2": {
                    "Key4": [
                        "Value4",
                        {
                            "Key5": "Value5"
                        }
                    ]
                }
            }
            """;
        string json2 = """
            {
                "Key2": {
                    "Key4": [
                        "Value4",
                        {
                            "Key5": "UNEQUAL"
                        }
                    ]
                }
            }
            """;

        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        // Act
        bool areEqual = jsonStringsEqualityChecker.AreEqual(json1, json2);

        // Assert
        Assert.False(areEqual);
    }
    [Fact]
    public void NestedDictionaries_UnequalKey_AreNotEqual()
    {
        // Arrange
        string json1 = """
            {
                "Key2": {
                    "Key4": [
                        "Value4",
                        {
                            "Key5": "Value5"
                        }
                    ]
                }
            }
            """;
        string json2 = """
            {
                "Key2": {
                    "Key4": [
                        "Value4",
                        {
                            "UNEQUAL": "Value5"
                        }
                    ]
                }
            }
            """;

        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        // Act
        bool areEqual = jsonStringsEqualityChecker.AreEqual(json1, json2);

        // Assert
        Assert.False(areEqual);
    }
    [Fact]
    public void DifferentOrder_List_AreEqual()
    {
        // Arrange
        string json1 = """
            {
                "Key2": [
                    "Value2",
                    "Value3"
                ]
            }
            """;
        string json2 = """
            {
                "Key2": [
                    "Value3",
                    "Value2"
                ]
            }
            """;

        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        // Act
        bool areEqual = jsonStringsEqualityChecker.AreEqual(json1, json2);

        // Assert
        Assert.True(areEqual);
    }
    [Fact]
    public void DifferentOrder_Dictionary_AreEqual()
    {
        // Arrange
        string json1 = """
            {
                "Key1": "Value1",
                "Key2": "Value2"
            }
            """;
        string json2 = """
            {
                "Key2": "Value2",
                "Key1": "Value1"
            }
            """;

        JsonStringsEqualityChecker jsonStringsEqualityChecker = new();

        // Act
        bool areEqual = jsonStringsEqualityChecker.AreEqual(json1, json2);

        // Assert
        Assert.True(areEqual);
    }
}