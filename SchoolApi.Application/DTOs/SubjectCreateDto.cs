// SubjectCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Application.DTOs
{
    public class SubjectCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title can't be more than 100 characters")]
        public string Title { get; set; } = string.Empty;
    }
}
