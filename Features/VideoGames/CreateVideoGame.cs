using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vertical_Slice_Architecture.Data;
using Vertical_Slice_Architecture.Entities;

namespace Vertical_Slice_Architecture.Features.VideoGames
{
    public class CreateVideoGame
    {
        public record Command(string Title, string Genre, int ReleaseYear) : IRequest<Response>;
        public record Response(int Id, string Title, string Genre, int ReleaseYear);
        public class Handler(VideoGameDBContext _context) : IRequestHandler<Command, Response>
        {
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = new VideoGame
                {
                    Title = request.Title,
                    Genre = request.Genre,
                    ReleaseYear = request.ReleaseYear
                };
                _context.VideoGames.Add(videoGame);
                await _context.SaveChangesAsync(cancellationToken);
                return new Response(videoGame.Id, videoGame.Title, videoGame.Genre, videoGame.ReleaseYear);
            }
        }
    }

    [ApiController]
    [Route("api/games")]
    public class CreateVideoGameController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateVideoGame.Response>> CreateVideoGame(CreateVideoGame.Command command)
        {
            var response = await _sender.Send(command);
            return CreatedAtAction(nameof(CreateVideoGame), new { id = response.Id }, response);
        }
    }
}
