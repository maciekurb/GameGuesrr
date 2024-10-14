using System.Net;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Questions.Queries;
using GameGuessr.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameGuessr.Api.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{mode}")]
        [ProducesResponseType(typeof(GamesQuestion), (int)HttpStatusCode.OK)]
        public Task<IActionResult> Get(GameMode mode, CancellationToken cancellationToken)
            => _mediator.Send(new GetSingleQuestionQuery(mode), cancellationToken)
               .Finally(HandleCommandResult);
    }
}