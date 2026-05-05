using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;
using ShareTracker.Domain.Enums;

namespace ShareTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchasesController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePurchaseRequest request)
    {
        var id = await _purchaseService.CreateAsync(request);
        return CreatedAtAction(nameof(Create), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var purchases = await _purchaseService.GetAllAsync();
        return Ok(purchases);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var purchase = await _purchaseService.GetByIdAsync(id);
        if (purchase == null) return NotFound();
        return Ok(purchase);
    }

    
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<UpdatePurchaseRequest> patchDoc)
    {
        if (patchDoc == null) return BadRequest();

        var existing = await _purchaseService.GetByIdAsync(id);
        if (existing == null) return NotFound();

        var updateRequest = new UpdatePurchaseRequest
        {
            SecurityId = existing.SecurityId,
            BrokerId = existing.BrokerId,
            Date = existing.Date,
            PricePerShare = existing.PricePerShare,
            Quantity = existing.Quantity
        };

        patchDoc.ApplyTo(updateRequest, ModelState);
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!TryValidateModel(updateRequest)) return BadRequest(ModelState);

        var (result, data) = await _purchaseService.UpdateAsync(id, updateRequest);
        return result switch
        {
            UpdateResult.Success          => Ok(data),
            UpdateResult.NotFound         => NotFound(),
            UpdateResult.InvalidReference => BadRequest("Invalid SecurityId or BrokerId."),
            _ => throw new InvalidOperationException()
        };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _purchaseService.DeleteAsync(id);
        return result switch
        {
            DeleteResult.Success  => NoContent(),
            DeleteResult.NotFound => NotFound(),
            _ => throw new InvalidOperationException()
        };
    }
}