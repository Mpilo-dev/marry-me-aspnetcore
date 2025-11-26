using Marry_Me.DTOs;
using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marry_Me.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarriageController : ControllerBase
    {
        private readonly IMarriageService _marriageService;

        public MarriageController(IMarriageService marriageService)
        {
            _marriageService = marriageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarriageResponseDTO>>> GetAllMarriages()
        {
            var marriages = await _marriageService.GetAllMarriagesAsync();

            var result = marriages.Select(m => new MarriageResponseDTO
            {
                Id = m.Id,
                UserId = m.UserId,
                Female = new SimplePersonDTO
                {
                    Id = m.Female?.Id ?? 0,
                    FirstName = m.Female?.FirstName,
                    LastName = m.Female?.LastName
                },
                Male = new SimplePersonDTO
                {
                    Id = m.Male?.Id ?? 0,
                    FirstName = m.Male?.FirstName,
                    LastName = m.Male?.LastName
                }
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarriageResponseDTO>> GetMarriageById(int id)
        {
            var m = await _marriageService.GetMarriageByIdAsync(id);
            if (m == null)
                return NotFound();

            var response = new MarriageResponseDTO
            {
                Id = m.Id,
                UserId = m.UserId,
                Female = new SimplePersonDTO
                {
                    Id = m.Female?.Id ?? 0,
                    FirstName = m.Female?.FirstName,
                    LastName = m.Female?.LastName
                },
                Male = new SimplePersonDTO
                {
                    Id = m.Male?.Id ?? 0,
                    FirstName = m.Male?.FirstName,
                    LastName = m.Male?.LastName
                }
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<MarriageResponseDTO>> CreateMarriage([FromBody] MarriageDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var marriage = new Marriage
            {
                UserId = dto.UserId,
                FemaleId = dto.FemaleId,
                MaleId = dto.MaleId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _marriageService.CreateMarriageAsync(marriage);

            var response = new MarriageResponseDTO
            {
                Id = created.Id,
                UserId = created.UserId,
                Female = new SimplePersonDTO
                {
                    Id = created.Female?.Id ?? 0,
                    FirstName = created.Female?.FirstName,
                    LastName = created.Female?.LastName
                },
                Male = new SimplePersonDTO
                {
                    Id = created.Male?.Id ?? 0,
                    FirstName = created.Male?.FirstName,
                    LastName = created.Male?.LastName
                }
            };

            return CreatedAtAction(nameof(GetMarriageById), new { id = created.Id }, response);
        }

    }
}