using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamiliesWebAPI.Data;
using FamiliesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamiliesWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamiliesController : ControllerBase
    {
        private readonly IFamiliesService familiesService;

        public FamiliesController(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Family>>> GetFamilies()
        {
            try
            {
                IList<Family> families = await familiesService.GetFamiliesAsync();
                return Ok(families);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Family>> AddFamily([FromBody] Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Family addedFamily = await familiesService.AddFamilyAsync(family);
                return Created($"/{addedFamily.Id}", addedFamily);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Family>> UpdateFamily([FromBody] Family family)
        {
            try
            {
                Family updateFamily = await familiesService.UpdateFamilyAsync(family);
                return Ok(updateFamily);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteFamily([FromRoute] int id)
        {
            try
            { 
                await familiesService.RemoveFamilyAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
}
}