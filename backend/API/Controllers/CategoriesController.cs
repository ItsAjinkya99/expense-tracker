using Application.Features.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ISender sender) : ControllerBase
{
    public readonly ISender _sender;
    
    [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var categoryId = await _sender.Send(command, cancellationToken);
            
            return CreatedAtAction(nameof(GetCategory), new { id = categoryId }, null);
        }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetCategory(Guid userId)
    {
        var categories  = await _sender.Send(new GetUserCategoriesQuery(userId));
        
        return Ok(categories);
    }
        
}