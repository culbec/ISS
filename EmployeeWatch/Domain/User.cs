using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("users")]
public class User : Entity<int>
{
    [Required]
    [Key]
    [Column("uid")]
    public new virtual int Tid { get; init; }

    [Required]
    [StringLength(50)]
    [Column("name")]
    public string? Name { get; init; }

    [Required]
    [StringLength(13)]
    [Column("cnp")]
    public string? Cnp { get; init; }

    [Required]
    [StringLength(30)]
    [Column("username")]
    public string? Username { get; init; }

    [Required]
    [StringLength(255)]
    [Column("password")]
    public string? Password { get; init; }

    [Required]
    [Column("role", TypeName = "varchar")]
    [EnumDataType(typeof(string))]
    public Role Role { get; init; }

    private bool Equals(User other)
    {
        return base.Equals(other) && Name == other.Name && Cnp == other.Cnp && Username == other.Username &&
               Password == other.Password && Role == other.Role;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((User)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Name, Cnp, Username, Password, Role);
    }

    public override string ToString()
    {
        return
            $"{base.ToString()}, {nameof(Name)}: {Name}, {nameof(Cnp)}: {Cnp}, {nameof(Username)}: {Username}, {nameof(Role)}: {Role}";
    }
}