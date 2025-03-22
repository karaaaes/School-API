using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Domain.Entities;

public class Student
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime BirthDate { get; set; }
}
