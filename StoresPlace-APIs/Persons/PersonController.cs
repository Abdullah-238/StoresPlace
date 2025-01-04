using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;

namespace StoresPlace_APIs.Persons
{
    [Route("api/Persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpPost("AddPerson", Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> AddPerson( PersonDTO personDTO)
        {
            clsPerson newPerson = new clsPerson(personDTO);

            if (newPerson.Save())
            {
                return Ok(newPerson.PDTO);
            }
            else
            {
                return BadRequest("Failed to add the person.");
            }
        }

        [HttpPut("UpdatePerson/{PersonID}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonDTO> UpdatePerson(int PersonID, PersonDTO personDTO)
        {
            if (PersonID != personDTO.PersonID)
            {
                return BadRequest("Person ID mismatch.");
            }

            clsPerson existingPerson =  clsPerson.Find(PersonID);

            existingPerson.Email = personDTO.Email;
            existingPerson.Password = personDTO.Password;
            existingPerson.Phone = personDTO.Phone;
            existingPerson.IsActive = personDTO.IsActive;
            existingPerson.Name = personDTO.Name;

            if (existingPerson.Save())
            {
                    return Ok(existingPerson.PDTO);
            }
            else
            {
                return StatusCode(500, new { message = "Can't update this person" });
            }

        }

        [HttpDelete("DeletePerson/{PersonID}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeletePerson(int PersonID)
        {
            bool result = clsPerson.Delete(PersonID);

            if (result)
            {
                return Ok($"Person with ID {PersonID} deleted successfully.");
            }
            else
            {
                return NotFound($"Person with ID {PersonID} not found.");
            }
        }

        [HttpGet("GetPerson/{PersonID}", Name = "GetPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> GetPerson(int PersonID)
        {
            var person = clsPerson.Find(PersonID);

            if (person != null)
            {
                return Ok(person.PDTO);
            }
            else
            {
                return NotFound("Person not found.");
            }
        }

        [HttpGet("GetAllPersons", Name = "GetAllPersons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<PersonDTO>> GetAllPersons()
        {
            var persons = clsPerson.GetAll();

            if (persons != null && persons.Count > 0)
            {
                return Ok(persons);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("Exists/{PersonID}", Name = "IsPersonExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsPersonExists(int PersonID)
        {
            bool exists = clsPerson.IsExist(PersonID);

            return Ok(exists);
        }

        [HttpGet("ExistsByEmail/{Email}", Name = "IsPersonExistsByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsPersonExistsByEmail(string Email)
        {
            bool exists = clsPerson.IsPersonExistsByEmail(Email);

            return Ok(exists);
        }

        [HttpGet("ExistsByPhone/{Phone}", Name = "IsPersonExistsByPhone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsPersonExistsByPhone(string Phone)
        {
            bool exists = clsPerson.IsPersonExistsByPhone(Phone);

            return Ok(exists);
        }

        [HttpGet("FindPersonByEmailAndPassword/{Email}/{Password}", Name = "FindPersonByEmailAndPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> FindPersonByEmailAndPassword(string Email, string Password)
        {
            var person = clsPerson.FindPersonByEmailAndPassword(Email, Password);

            if (person != null)
            {
                return Ok(person.PDTO);
            }
            else
            {
                return NotFound("Person not found.");
            }
        }

        [HttpGet("UpdatePass/{Email}/{Password}", Name = "UpdatePass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult IsPersonExists(string Email, string Password)
        {
            bool exists = clsPerson.UpdatePassword(Email,Password);

            if (exists)
            return Ok();
            else
                return StatusCode(500, new { message = "Can't update this password" });

        }

    }
}

