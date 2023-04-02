using Data.Entities;
using Data.Enums;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using Models;

namespace Week7.Controllers
{
    //[ApiController]
    [Route("api/[controller]")] // localhost:7042/api/User/add
    [Authorize] // Authorization in ASP.NET - Does not allow access without authentication
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserController(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [AllowAnonymous]  // allow access without authentication required
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto model)
        {
            try
            {
                // perform vaidation on this model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if(userExists != null)
                {
                    return BadRequest("Email already exists!");
                }

                // check if attendance type is valid
                AttendanceStatus status;
                if (!AttendanceStatus.TryParse(model.AttendanceStatus, out status))
                {
                    return BadRequest("Invalid attendance type");
                }

                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    AttendanceStatus = model.AttendanceStatus,
                    UserName = model.Email
                };

                //var result = _userRepository.AddNew(user);
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
                }

                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }

                return BadRequest(result);

            }
            catch(Exception ex)
            {
                // handle error thrown to file
                return BadRequest();
            }
        }

        [HttpGet("list")]
        public ActionResult GetAll(int page, int perpage)
        {

            var result = _userRepository.GetAll();
            if (result != null && result.Count() > 0)
            {
                var paged = _userRepository.Paginate(result.ToList(), perpage, page);
                return Ok(paged);
            }

            return Ok(result);

        }


        [HttpGet("single/{id}")]
        public ActionResult GetUser(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("Id provided must not be empty");
                }

                var user = _userRepository.GetById(id);
                if(user == null) {
                    return NotFound("No record found!");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // handle error thrown to file
                return BadRequest();
            }
            
        }

        [HttpPatch("update/{id}")]
        public ActionResult UpdateUser(string id, [FromBody] UpdateUserDto model)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("Id provided must not be empty");
                }

                // perform vaidation on this model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != model.Id)
                {
                    return BadRequest("Id mismatch!");
                }

                var user = _userRepository.GetById(id);
                if (user == null)
                {
                    return NotFound("No record found!");
                }

                // map model to existing user record
                user.AttendanceStatus = model.AttendanceStatus;

                var result = _userRepository.Update(user);
                if(result != null)
                    return Ok(result);

                return BadRequest("Failed to update user record");
            }
            catch (Exception ex)
            {
                // handle error thrown to file
                return BadRequest();
            }

        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteUser(string id)
        {
           
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("Id provided must not be empty");
                }

                var user = _userRepository.GetById(id);
                if (user == null)
                {
                    return NotFound("No record found!");
                }

                var result = _userRepository.Delete(user);
                if (result)
                    return Ok($"User with id {user.Id} has been deleted");

                return BadRequest("Failed to delete user record");
            }
            catch (Exception ex)
            {
                // handle error thrown to file
                return BadRequest();
            }
        }

        [HttpDelete("delete/list")]
        public ActionResult DeleteMany([FromBody] DeleteManyUsersDto model)
        {

            try
            {
                if (model.Ids.Count() < 1)
                {
                    return BadRequest("No id was provided");
                }

                var list = new List<User>();
                foreach(var id in model.Ids)
                {
                    var user = _userRepository.GetById(id);
                    if(user != null)
                        list.Add(user); 
                }

                if(list.Count > 0)
                {
                    var result = _userRepository.Delete(list);
                    if (result)
                        return Ok("List of users deleted");
                }

                return BadRequest("Failed to delete users' records");
            }
            catch (Exception ex)
            {
                // handle error thrown to file
                return BadRequest();
            }
        }

        //[HttpPatch]
        //public ActionResult AddUser()
        //{

        //}
    }
}
