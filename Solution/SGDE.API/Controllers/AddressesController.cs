﻿namespace SGDE.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Supervisor;
    using Domain.ViewModels;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Authorization;

    #endregion

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(ILogger<AddressesController> logger, ISupervisor supervisor)
        {
            _logger = logger;
            _supervisor = supervisor;
        }

        [HttpGet]
        [Produces(typeof(List<AddressViewModel>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                return new ObjectResult(await _supervisor.GetAllAddressAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }
    }
}