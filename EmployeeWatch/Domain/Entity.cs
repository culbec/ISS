namespace Domain;

public class Entity<TId>
{
    public virtual TId? Tid { get; init; }

    protected Entity() {}

    private bool Equals(Entity<TId> other)
    {
        return EqualityComparer<TId?>.Default.Equals(Tid, other.Tid);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId?>.Default.GetHashCode(Tid!);
    }

    public override string ToString()
    {
        return $"{nameof(Tid)}: {Tid}";
    }
}