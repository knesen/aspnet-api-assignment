using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.DTOs;
using WebApi.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    #region CREATE
    [HttpPost]
    public async Task<IActionResult> Create(CourseDTO dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var course = new CourseEntity
                {
                    Title = dto.Title,
                    ImageName = dto.ImageName,
                    Author = dto.Author,
                    IsBestseller = dto.IsBestseller,
                    Hours = dto.Hours,
                    OriginalPrice = dto.OriginalPrice,
                    DiscountPrice = dto.DiscountPrice,
                    LikesInPercent = dto.LikesInPercent,
                    LikesInNumbers = dto.LikesInNumbers
                };
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }
        return BadRequest();
    }
    #endregion

    #region READ
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _context.Courses.ToListAsync());


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (course != null)
        {
            return Ok(course);
        }

        return NotFound();
    }
    #endregion

    #region UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOne(CourseEntity entity)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (course != null)
        {

            course.Title = entity.Title;
            course.ImageName = entity.ImageName;
            course.Author = entity.Author;
            course.IsBestseller = entity.IsBestseller;
            course.Hours = entity.Hours;
            course.OriginalPrice = entity.OriginalPrice;
            course.DiscountPrice = entity.DiscountPrice;
            course.LikesInPercent = entity.LikesInPercent;
            course.LikesInNumbers = entity.LikesInNumbers;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }
        return NotFound();
    }
    #endregion

    #region DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok();
        }
        return NotFound();
    }
    #endregion
}
