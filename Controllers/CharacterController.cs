global using dotnetRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private static Character Knight = new Character();

        [HttpGet(Name = "GetCharacter")]
        public ActionResult<Character> Get()
        {
            return Ok(Knight);
        }
    }
}