﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    private readonly DataContext context;

    public ValuesController(DataContext context)
    {
      this.context = context;

    }
    // GET api/values
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetValues()
    {
      var values = await context.Values.ToListAsync();

      return Ok(values);
    }

    // GET api/values/5
    [Authorize(Roles = "Member")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetValue(int id)
    {
      var val = await context.Values.FirstOrDefaultAsync(v => v.Id == id);
      return Ok(val);
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
