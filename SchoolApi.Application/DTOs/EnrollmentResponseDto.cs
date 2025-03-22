namespace SchoolApi.Application.DTOs;

public class EnrollmentResponseDto
{
    public int Id { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
}
