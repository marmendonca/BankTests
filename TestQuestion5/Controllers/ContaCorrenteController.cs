using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestQuestion5.Application.Commands;
using TestQuestion5.Application.Queries;
using TestQuestion5.Domain.Exceptions;

namespace TestQuestion5.Controllers;

[ApiController]
[Route("api/v1/[controller]/")]
public class ContaCorrenteController : ControllerBase
{

    private readonly ILogger<ContaCorrenteController> _logger;
    private readonly IMediator _mediator;

    public ContaCorrenteController(ILogger<ContaCorrenteController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Endpoint utilizado para efetuar uma movimentação em uma conta corrente.
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Id da movimentação</returns>
    [HttpPost("movimentacao")]
    public async Task<IActionResult> MovimentarContaCorrenteAsync([FromBody] MovimentarContaCorrenteCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        catch (DomainException ex)
        {

            return BadRequest(new { Message = ex.Message, Type = ex.Type });
        }
    }

    /// <summary>
    /// Endpoint utilizado para consultar saldo atual da conta corrente.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>Retorna dados da conta juntamente com o saldo atual</returns>
    [HttpGet("saldo")]
    public async Task<IActionResult> ConsultarSaldoContaCorrenteAsync([FromQuery] ConsultarSaldoContaCorrenteQuery query)
    {
        try
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
        catch (DomainException ex)
        {

            return BadRequest(new { Message = ex.Message, Type = ex.Type });
        }
    }
}