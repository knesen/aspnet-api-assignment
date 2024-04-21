using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs;

public class CourseDTO
{
    [Required]
    public string Title { get; set; } = null!;
    public string? ImageName { get; set; }
    public string? Author { get; set; }
    public bool IsBestseller { get; set; } = false;
    public int Hours { get; set; }

    [Required]
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; } = 0!;
    public decimal LikesInPercent { get; set; }
    public decimal LikesInNumbers { get; set; }

}
