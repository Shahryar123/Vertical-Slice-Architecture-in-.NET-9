using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Data;
using static Vertical_Slice_Architecture.Features.VideoGames.GetAllVideoGames;

namespace Vertical_Slice_Architecture.Features.VideoGames
{
    public class GetAllVideoGames
    {
        public record Query : IRequest<IEnumerable<Response>>;
        
        public record Response(int Id, string Title, string Genre, int ReleaseYear);

        public class Handler(VideoGameDBContext _context) : IRequestHandler<Query, IEnumerable<Response>>
        {
            public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var videoGames = await _context.VideoGames.ToListAsync(cancellationToken);
                return videoGames.Select(vg => new Response(vg.Id, vg.Title, vg.Genre, vg.ReleaseYear));
            }
        }

    }
    [ApiController]
    [Route("api/games")]
    public class GetAllGamesController(ISender _sender) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Response>>> GetAllGames()
        {
            var response = await _sender.Send(new Query());
            return Ok(response);
        }
    }
}
