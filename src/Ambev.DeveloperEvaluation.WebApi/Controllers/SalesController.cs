using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSalesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    public SalesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command, ct));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateSaleCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest();
        return Ok(await _mediator.Send(command, ct));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new CancelSaleCommand { Id = id }, ct);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
        => Ok(await _mediator.Send(new GetSaleQuery(id), ct));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        => Ok(await _mediator.Send(new GetSalesListQuery { Page = page, PageSize = pageSize }, ct));
}