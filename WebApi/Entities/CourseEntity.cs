﻿namespace WebApi.Entities;

public class CourseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string? ImageName { get; set; }
    public string? Author { get; set; }
    public bool IsBestseller { get; set; }
    public int Hours { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal LikesInPercent { get; set; }
    public decimal LikesInNumbers { get; set; }


}
