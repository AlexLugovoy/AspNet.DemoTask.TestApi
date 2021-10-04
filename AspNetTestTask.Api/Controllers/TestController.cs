using AspNetTestTask.Api.Data;
using AspNetTestTask.Api.DTO;
using AspNetTestTask.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace AspNetTestTask.Api.Controllers
{
    [Route("api/test")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITestRepo _repository;
        private readonly IMapper _mapper;

        public TestController(IMapper mapper, UserManager<IdentityUser> user, ITestRepo repo)
        {
            _userManager = user;
            _repository = repo;
            _mapper = mapper;
        }

        //GET api/test/currentuser
        [HttpGet("currentuser")]
        public ActionResult GetCurrentUser()
        {
            var respone = new CurrentUserDTO();
            var email = User.FindFirst("Email").Value;
            if (email == null)
                return NoContent();
            respone.Email = email;
            return Ok(respone);
        }

        //GET api/test
        [HttpGet]
        public ActionResult<IEnumerable<Test>> GetAllTests()
        {
            var items = _repository.GetAllTests();
            if (items == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<TestReadDTO>>(items));
        }

        //GET api/test/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Question>> GetQuestionById(int id)
        {
            var items = _repository.GetQuestionsByTestId(id);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<QuestionReadDTO>>(items));
        }

        //GET api/test/questions/{id}
        [HttpGet("questions/{id}")]
        public ActionResult<IEnumerable<Answer>> GetAnswersById(int id)
        {
            var items = _repository.GetAnswersByQuestId(id);
            if (items == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<AnswerReadDTO>>(items));
        }

        [HttpPost("result")]
        public ActionResult GetResult(Answer[] answers)
        {
            var response = new ResultResponseDTO();
            response.CountOfAnswer = answers.Length;
            var items = _mapper.Map<IEnumerable<Answer>>(answers);
            foreach (var answer in items)
            {
                if (_repository.CheckAnswer(answer).Status)
                    response.CountOfRightAnswers++;
            }
            return Ok(response);
        }
    }
}
