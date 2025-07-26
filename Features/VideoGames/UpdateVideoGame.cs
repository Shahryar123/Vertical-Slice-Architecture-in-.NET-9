using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vertical_Slice_Architecture.Data;

namespace Vertical_Slice_Architecture.Features.VideoGames
{
    public class UpdateVideoGame
    {
        public record Command(int Id, string Title, string Genre, int ReleaseYear) : IRequest<Response?>;
        public record Response(int Id, string Title, string Genre, int ReleaseYear);
        public class Handler(VideoGameDBContext _context) : IRequestHandler<Command, Response?>
        {
            public async Task<Response?> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = await _context.VideoGames.FindAsync(request.Id);
                if (videoGame == null)
                {
                    return null;
                }
                videoGame.Title = request.Title;
                videoGame.Genre = request.Genre;
                videoGame.ReleaseYear = request.ReleaseYear;

                await _context.SaveChangesAsync(cancellationToken);
                return new Response(videoGame.Id, videoGame.Title, videoGame.Genre, videoGame.ReleaseYear);
            }
        }
    }

    [ApiController]
    [Route("api/games")]
    public class UpdateVideoGameController(ISender _sender) : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateVideoGame.Response>> UpdateVideoGame(int id, UpdateVideoGame.Command command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the request body.");
            }
            var response = await _sender.Send(command);
            if (response == null)
            {
                return NotFound($"Video game with ID {id} not found.");
            }
            return CreatedAtAction(nameof(UpdateVideoGame), response);
        }
    }
}