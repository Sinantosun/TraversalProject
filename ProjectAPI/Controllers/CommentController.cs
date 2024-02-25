using BussinessLayer.AbstractValidator;
using DtoLayer.CommentDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public IActionResult FilterComment(int id)
        {
            var value = _commentService.TGetDestinationByID(id);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult AddComment(CreateCommandDto createCommandDto)
        {
            Comment comment = new Comment()
            {
                CommentContent = createCommandDto.CommentContent,
                CommentDate = Convert.ToDateTime(DateTime.Now.ToString("d")),
                CommentState=true,
                CommentUser=createCommandDto.CommentUser,
                DestinationID=createCommandDto.DestinationID,
                
            };
            _commentService.TInsert(comment);
            return Ok("Eklendi..");
        }
    }
}
