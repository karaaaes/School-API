namespace SchoolApi.Domain.Entities;
public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public int ClassId { get; set; }

    public Student? Student { get; set; }
    public Subject? Subject { get; set; }
    public Teacher? Teacher { get; set; }
    public Class? Class { get; set; }
}
