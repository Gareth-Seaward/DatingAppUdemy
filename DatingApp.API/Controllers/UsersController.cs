using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
  [ServiceFilter(typeof(LogUserActivity))]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IDatingRepository repo;
    private readonly IMapper mapper;
    public UsersController(IDatingRepository repo, IMapper mapper)
    {
      this.mapper = mapper;
      this.repo = repo;

    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
    {
      var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


      var userFromRepo = await repo.GetUser(currentUserId, false);
      userParams.UserId = currentUserId;

      if (string.IsNullOrEmpty(userParams.Gender))
      {
        userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
      }
      
      var users = await repo.GetUsers(userParams);
      var usersToReturn = mapper.Map<IEnumerable<UserForListDto>>(users);

      Response.AddPaginations(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
      return Ok(usersToReturn);
    }

    [HttpGet("{id}", Name="GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
      var isCurrentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == id;

      var user = await repo.GetUser(id, isCurrentUser);

      var userToReturn = mapper.Map<UserForDetailedDto>(user);

      return Ok(userToReturn);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserForUpdatesDto userForupdateDto)
    {
      if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
      return Unauthorized();

      var userFromRepo = await repo.GetUser(id, true);

      mapper.Map(userForupdateDto, userFromRepo);

      if(await repo.SaveAll())
        return NoContent();

        throw new Exception($"Updaing user {id} failed on save");
    }

    [HttpPost("{id}/like/{recipientId}")]
    public async Task<IActionResult> LikeUser(int id, int recipientId)
    {
      if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
      return Unauthorized();

      var like = await repo.GetLike(id, recipientId);

      if(!ReferenceEquals(like, null)) 
      return BadRequest("You already like this person");

      if(await repo.GetUser(recipientId, false) == null)
      return NotFound();

      like = new Like{LikerId = id, LikeeId = recipientId};

      repo.Add<Like>(like);

      if(await repo.SaveAll())
      return Ok();

      return BadRequest("Failed to like person"); 
    }
  }
}