using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("employees")]
public class Employee : User
{
    [Required]
    [Key]
    [Column("eid")]
    public override int Tid { get; init; }

    [Required]
    [ForeignKey("User")]
    public int Uid { get; init; }

    // Navigation property for User
    public User User { get; set; }

    [Required]
    [Column("grade", TypeName = "varchar")]
    [EnumDataType(typeof(string))]
    public Grade Grade { get; init; }

    [Required]
    [Column("salary")]
    public float Salary { get; init; }

    [Column("present_time")]
    public DateTime? PresentTime { get; set; }

    [NotMapped]
    private IEnumerable<Task> Tasks { get; } = new List<Task>();

    private bool Equals(Employee other)
    {
        return base.Equals(other) && Grade == other.Grade && Salary.Equals(other.Salary) && PresentTime.Equals(other.PresentTime);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Employee)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), (int)Grade, Salary);
    }

    public override string ToString()
    {
        return
            $"{base.ToString()}, {nameof(Grade)}: {Grade}, {nameof(Salary)}: {Salary}, {nameof(PresentTime)}: {PresentTime}";
    }
}