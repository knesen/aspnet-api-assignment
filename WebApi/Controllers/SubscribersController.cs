using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Contexts;
using WebApi.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;


    #region CREATE
    [HttpPost]
    public async Task<IActionResult> Create(string email)
    {
        
        if (ModelState.IsValid)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (!await _context.Subscribers.AnyAsync(x => x.Email == email))
                {
                    try
                    {
                        var subscriberEntity = new SubscriberEntity { Email = email };
                        _context.Subscribers.Add(subscriberEntity);
                        await _context.SaveChangesAsync();


                        return Created("", null);
                    }
                    catch (Exception ex) { return Problem(ex.Message); }
                    }
                return Conflict("Your email adress is already subscribed");

            }

        }
        return BadRequest();
    }

    #endregion

    #region READ
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        
        if (ModelState.IsValid)
        {
            var subscribers = await _context.Subscribers.ToListAsync();
            if (subscribers.Count != 0)
                return Ok(subscribers);

            return NotFound();
        }
        return BadRequest();
    }

    [HttpGet("{id}" )]
    public async Task<IActionResult> GetOne(int id)
    {
        
        if (ModelState.IsValid)
        {
            var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
            if (subscriber != null)
                return Ok(subscriber);
            return NotFound();
        }
        return BadRequest();
    }

    #endregion

    #region UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOne(int id, string email)
    {
        
        if (ModelState.IsValid)
        {
            var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
            if (subscriber != null)
            {
            subscriber.Email = email;
            _context.Subscribers.Update(subscriber);
            await _context.SaveChangesAsync();

            return Ok(subscriber);
            }
            return NotFound();

        }
        return BadRequest();
    }
    #endregion

    #region DELETE
    [HttpDelete("{email}")]
    public async Task<IActionResult> Delete(string email)
    {
        
        if (ModelState.IsValid)
        {
            var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email == email);
            if (subscriber != null)
            {
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();

        }
        return BadRequest();
    }

    #endregion
}
