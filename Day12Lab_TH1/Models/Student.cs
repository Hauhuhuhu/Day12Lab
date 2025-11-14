using System.ComponentModel.DataAnnotations;

namespace Day12Lab_TH1.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string? Email { get; set; }
        [StringLength(100, MinimumLength = 8)]
        [Required]
        public string? Password { get; set; }
        [Required]
        public Branch? Branch { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        public bool IsRegular { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string? Address { get; set; }
        //[Range(typeof(DateTime),"1/1/1963","31/12/2025")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateofBirth { get; set; }
        public string? Avatar { get; set; }
        [Required(ErrorMessage = "Điểm là bắt buộc")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong khoảng từ 0.0 đến 10.0")]
        public double Score { get; set; }
    }
}
