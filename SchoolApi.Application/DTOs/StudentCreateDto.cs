// StudentCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Application.DTOs
{
    public class StudentCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name can't be more than 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }
    }
}
