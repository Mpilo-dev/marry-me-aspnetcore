using Marry_Me.DTOs;
using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marry_Me.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Route("GetAllPersons")]
        public async Task<ActionResult<DataResponseDTO>> GetAllPersons()
        {
            var dataResponse = new DataResponseDTO();

            try
            {
                var allPersons = await _personService.GetAllPersonsAsync();

                dataResponse.Message = "All persons retrieved successfully";
                dataResponse.Data = allPersons;
                dataResponse.IsSuccessful = true;

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Message = $"Error retrieving persons: {ex.Message}";
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return StatusCode(500, dataResponse);
            }
        }

        [HttpGet]
        [Route("GetPersonById/{id}")]
        public async Task<ActionResult<DataResponseDTO>> GetPersonById([FromRoute] int id)
        {
            var dataResponse = new DataResponseDTO();

            try
            {
                var person = await _personService.GetPersonByIdAsync(id);

                dataResponse.Message = "Person retrieved successfully";
                dataResponse.Data = person;
                dataResponse.IsSuccessful = true;

                return Ok(dataResponse);
            }
            catch (KeyNotFoundException ex)
            {
                dataResponse.Message = ex.Message;
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return NotFound(dataResponse);
            }
            catch (ArgumentException ex)
            {
                dataResponse.Message = ex.Message;
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return BadRequest(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Message = $"Error retrieving person: {ex.Message}";
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return StatusCode(500, dataResponse);
            }
        }

        [HttpPost]
        [Route("CreatePerson")]
        public async Task<ActionResult<DataResponseDTO>> CreatePerson([FromBody] PersonDTO personDto)
        {
            var dataResponse = new DataResponseDTO();

            try
            {
                if (!ModelState.IsValid)
                {
                    dataResponse.Message = "Invalid model state";
                    dataResponse.Data = ModelState;
                    dataResponse.IsSuccessful = false;

                    return BadRequest(dataResponse);
                }
                var person = new Person
                {
                    UserId = personDto.UserId,
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    Gender = Enum.TryParse<Gender>(personDto.Gender, true, out var parsedGender) ? parsedGender : throw new ArgumentException("Invalid gender."),
                    IdNumber = personDto.IdNumber
                };

                var createdPerson = await _personService.CreatePersonAsync(person);

                dataResponse.Message = "Person created successfully";
                dataResponse.Data = createdPerson;
                dataResponse.IsSuccessful = true;

                return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Id }, dataResponse);
            }
            catch (ArgumentException ex)
            {
                dataResponse.Message = ex.Message;
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return BadRequest(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Message = $"ID number already exists";
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return StatusCode(500, dataResponse);
            }
        }

        [HttpPut]
        [Route("UpdatePerson/{id}")]
        public async Task<ActionResult<DataResponseDTO>> UpdatePerson([FromRoute] int id, [FromBody] Person person)
        {
            var dataResponse = new DataResponseDTO();

            try
            {
                if (!ModelState.IsValid)
                {
                    dataResponse.Message = "Invalid model state";
                    dataResponse.Data = ModelState;
                    dataResponse.IsSuccessful = false;

                    return BadRequest(dataResponse);
                }

                if (id != person.Id)
                {
                    dataResponse.Message = "ID in route does not match ID in body";
                    dataResponse.Data = null;
                    dataResponse.IsSuccessful = false;

                    return BadRequest(dataResponse);
                }

                await _personService.UpdatePersonAsync(person);

                dataResponse.Message = "Person updated successfully";
                dataResponse.Data = person;
                dataResponse.IsSuccessful = true;

                return Ok(dataResponse);
            }
            catch (KeyNotFoundException ex)
            {
                dataResponse.Message = ex.Message;
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return NotFound(dataResponse);
            }
            catch (ArgumentException ex)
            {
                dataResponse.Message = ex.Message;
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return BadRequest(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Message = $"Error updating person: {ex.Message}";
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return StatusCode(500, dataResponse);
            }
        }

        [HttpDelete]
        [Route("DeletePerson/{id}")]
        public async Task<ActionResult<DataResponseDTO>> DeletePerson([FromRoute] int id)
        {
            var dataResponse = new DataResponseDTO();

            try
            {
                await _personService.DeletePersonAsync(id);

                dataResponse.Message = "Person deleted successfully";
                dataResponse.Data = null;
                dataResponse.IsSuccessful = true;

                return Ok(dataResponse);
            }
            catch (KeyNotFoundException ex)
            {
                dataResponse.Message = ex.Message;
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return NotFound(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Message = $"Error deleting person: {ex.Message}";
                dataResponse.Data = null;
                dataResponse.IsSuccessful = false;

                return StatusCode(500, dataResponse);
            }
        }
    }
}