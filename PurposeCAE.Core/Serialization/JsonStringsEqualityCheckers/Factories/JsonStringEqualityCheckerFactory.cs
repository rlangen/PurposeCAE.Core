using PurposeCAE.Core.DI;

namespace PurposeCAE.Core.Serialization.JsonStringsEqualityCheckers.Factories;

public static class JsonStringEqualityCheckerFactory
{
    public static IJsonStringsEqualityChecker Create()
    {
        return DIContainer.Instance.GetService<IJsonStringsEqualityChecker>();
    }
}