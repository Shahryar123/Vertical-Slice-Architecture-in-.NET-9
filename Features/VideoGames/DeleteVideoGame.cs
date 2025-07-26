using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vertical_Slice_Architecture.Data;

namespace Vertical_Slice_Architecture.Features.VideoGames
{
    public class DeleteVideoGame
    {
        public record Command(int Id) : IRequest<bool>;
        public class Handler(VideoGameDBContext _context) : IRequestHandler<Command, bool>
        {
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = await _context.VideoGames.FindAsync(request.Id);
                if (videoGame == null)
                {
                    return false; // Not found
                }
                _context.VideoGames.Remove(videoGame);
                await _context.SaveChangesAsync(cancellationToken);
                return true; // Successfully deleted
            }
        }
    }
    [ApiController]
    [Route("api/games")]
    public class DeleteVideoGameController(ISender _sender) : ControllerBase
    {
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var command = new DeleteVideoGame.Command(id);
            var result = await _sender.Send(command);
            if (!result)
            {
                return NotFound($"Video game with ID {id} not found.");
            }
            return NoContent(); // Successfully deleted
        }
    }
}
