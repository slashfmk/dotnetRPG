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

        private static List<Character> charcters = new List<Character> {
        new Character {Name = "Yannick"},
        new Character()
        };


        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get()
        {
            return Ok(charcters);
        }


        [HttpGet("GetCharacter")]
        public ActionResult<Character> GetCharacter()
        {

            return Ok(charcters.ElementAt(0));
        }
    }
}