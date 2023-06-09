using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace PurposeCAE.Core.DataStructures.Comparison;

public abstract class CustomEqualityComparerBase<T>
    : IEqualityComparer<T?>, IEqualityComparer, IEquatable<T?>
    where T : CustomEqualityComparerBase<T>, IEquatable<T?>
{
    public override bool Equals(object? obj)
    {
        T typedThis = (T)this;

        if (obj is T typedObj)
            return typedThis.Equals(typedObj);

        return false;
    }

    public bool Equals(T? x, T? y)
    {
        return x == y;
    }

    public new bool Equals(object? x, object? y)
    {
        if (x is null || y is null)
            return x is null && y is null;

        else if (x is T typedX)
            return typedX.Equals(y);

        else if (y is T typedY)
            return typedY.Equals(x);

        else
            return false;
    }

    public bool Equals(T? other)
    {
        return IsEqual(other);
    }
    public abstract bool IsEqual(T? other);

    public override int GetHashCode()
    {
        return GetHash();
    }

    public int GetHashCode([DisallowNull] T? obj)
    {
        return obj.GetHashCode();
    }

    public int GetHashCode(object obj)
    {
        if (obj is null)
            return 0;
        else if (obj is T typedObj)
            return typedObj.GetHashCode();
        else
            return obj.GetHashCode();
    }

    protected abstract int GetHash();

    public static bool operator ==(CustomEqualityComparerBase<T>? left, CustomEqualityComparerBase<T>? right)
    {
        if (left is null)
            return right is null;

        T typedLeft = (T)left;

        return typedLeft.Equals((T?)right);
    }
    public static bool operator !=(CustomEqualityComparerBase<T>? left, CustomEqualityComparerBase<T>? right)
    {
        return !(left == right);
    }
}