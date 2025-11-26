using Marry_Me.DTOs;
using Marry_Me.Services.Abstraction.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Marry_Me.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivorceController : ControllerBase
    {
        private readonly IDivorceService _divorceService;

        public DivorceController(IDivorceService divorceService)
        {
            _divorceService = divorceService;
        }

        [HttpPost]
        public async Task<ActionResult<DataResponseDTO>> CreateDivorce([FromBody] DivorceDTO divorceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _divorceService.CreateDivorceAsync(divorceDto);

                return Ok(new DataResponseDTO
                {
                    IsSuccessful = true,
                    Message = "Divorce created successfully.",
                    Data = result
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new DataResponseDTO { IsSuccessful = false, Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new DataResponseDTO { IsSuccessful = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DataResponseDTO
                {
                    IsSuccessful = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }
    }
}
