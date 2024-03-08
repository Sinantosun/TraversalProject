using BussinessLayer.AbstractValidator;
using BussinessLayer.ValidationRules.CommentValidator;
using DtoLayer.CommentDtos;
using DtoLayer.GenericNotificationDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
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
        [HttpGet]
        public IActionResult GetCommentList()
        {
            var value = _commentService.TgetAllCommentWithDestination();
            return Ok(value);
        }

        [HttpPost]
        public IActionResult AddComment(CreateCommandDto createCommandDto)
        {

            CommentValidators validationRules = new CommentValidators();
            ValidationResult valideRules = validationRules.Validate(createCommandDto);
            if (valideRules.IsValid)
            {
                Comment comment = new Comment()
                {
                    CommentContent = createCommandDto.CommentContent,
                    CommentDate = Convert.ToDateTime(DateTime.Now.ToString("d")),
                    CommentState = true,
                    CommentUser = createCommandDto.CommentUser,
                    DestinationID = createCommandDto.DestinationID,

                };
                _commentService.TInsert(comment);
                return Ok("Eklendi..");
            }
            else
            {
                List<ResultNotificationDto> ErrorList = new List<ResultNotificationDto>();
                foreach (var item in valideRules.Errors)
                {
                    ResultNotificationDto resultNotificationDto = new ResultNotificationDto();
                    resultNotificationDto.Description = item.ErrorMessage;
                    resultNotificationDto.PropertyName = item.PropertyName;
                    ErrorList.Add(resultNotificationDto);
                }
                return BadRequest(ErrorList);
            }


         
        }

        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var find = _commentService.TGetById(id);
            _commentService.TDelete(find);
            return Ok();
        }
    }
}
