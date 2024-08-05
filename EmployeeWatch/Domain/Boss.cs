using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("bosses")]
public class Boss : User
{
    [Required]
    [Key]
    [Column("bid")]
    public override int Tid { get; init; }

    [Required]
    [ForeignKey("User")]
    public int Uid { get; init; }

    // Navigation property for User
    public User User { get; set; }

    [Required]
    [Column("position", TypeName = "varchar")]
    [EnumDataType(typeof(string))]
    public Position? Position { get; init; }

    [NotMapped] private IEnumerable<Employee> PresentEmployees { get; } = new List<Employee>();

    private bool Equals(Boss other)
    {
        return base.Equals(other) && Position == other.Position;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Boss)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Position);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(Position)}: {Position}";
    }
}