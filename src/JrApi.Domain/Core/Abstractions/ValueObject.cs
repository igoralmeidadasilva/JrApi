namespace JrApi.Domain.Core.Abstractions;

public abstract record ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            return false;

        return ReferenceEquals(left, right) || left!.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        => !(EqualOperator(left, right));
    

    public override int GetHashCode()
        => GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
}