using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("tasks")]
public sealed class Task : Entity<int>
{
    [Required]
    [Key]
    [Column("tid")]
    public override int Tid { get; init; }

    [Required]
    [StringLength(50)]
    [Column("title")]
    public string? Title { get; init; }

    [Required]
    [StringLength(120)]
    [Column("description")]
    public string? Description { get; init; }

    [Required]
    [Column("start_time")]
    public DateTime StartTime { get; init; }

    [Required]
    [StringLength(30)]
    [Column("username")]
    public string? Username { get; init; }

    private bool Equals(Task other)
    {
        return base.Equals(other) && Tid == other.Tid && Title == other.Title && Description == other.Description && StartTime.Equals(other.StartTime) && Username == other.Username;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Task)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Tid, Title, Description, StartTime, Username);
    }

    public override string ToString()
    {
        return
            $"{base.ToString()}, {nameof(Tid)}: {Tid}, {nameof(Title)}: {Title}, {nameof(Description)}: {Description}, {nameof(StartTime)}: {StartTime}, {nameof(Username)}: {Username}";
    }
}