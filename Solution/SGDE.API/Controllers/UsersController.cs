namespace SGDE.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Domain.Supervisor;
    using Domain.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Models;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    #endregion

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<AddressesController> _logger;

        public UsersController(ILogger<AddressesController> logger, ISupervisor supervisor)
        {
            _logger = logger;
            _supervisor = supervisor;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(Login login)
        {
            try
            {
                var userAuthenticate = await _supervisor.Authenticate(login.username, login.password);

                if (userAuthenticate == null)
                {
                    _logger.LogWarning("Error in Authenticate: username [{Username}] not registered or incorrect password", login.username);
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                    
                return new ObjectResult(new Session
                {
                    user = new UserViewModel
                    {
                        id = userAuthenticate.id,
                        name = userAuthenticate.name,
                        username = userAuthenticate.username,
                        email = userAuthenticate.email,
                        surname = userAuthenticate.surname,
                        birthDate = userAuthenticate.birthDate
                    },
                    token = userAuthenticate.token
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAsyn()
        //{
        //    try
        //    {
        //        return new ObjectResult(await _caSupervisor.GetAllUserAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Exception: ");
        //        return StatusCode(500, ex);
        //    }
        //}

        [HttpGet]
        public object Get()
        {
            try
            {
                var data = _supervisor.GetAllUser().ToList();
                return new { Items = data, data.Count };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            try
            {
                return _supervisor.GetUserById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        // GET api/users/5/jesus
        [HttpGet("{id}/{name}")]
        public object Get(int id, string name)
        {
            try
            {
                return _supervisor.GetUserById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public object Post([FromBody]UserViewModel userViewModel)
        {
            try
            {
                var result = _supervisor.AddUser(userViewModel);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }            
        }

        [HttpPut]
        public object Put([FromBody]UserViewModel userViewModel)
        {
            try
            {
                if (_supervisor.UpdateUser(userViewModel) && userViewModel.id != null)
                {
                    return _supervisor.GetUserById((int) userViewModel.id);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        // DELETE: api/users/5
        [HttpDelete("{id:int}")]
        //[Route("users/{id:int}")]
        public object Delete(int id)
        {
            try
            {
                return _supervisor.DeleteUser(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }
    }
}