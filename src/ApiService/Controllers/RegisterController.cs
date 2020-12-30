﻿using ApiInABox.Contexts;
using ApiInABox.Logic;
using ApiInABox.Models;
using ApiInABox.Models.Auth;
using ApiInABox.Models.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ApiInABox.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly DatabaseContext _dbContext;
        private readonly RegisterLogic _registerLogic;        

        public RegisterController(DatabaseContext dbContext, RegisterLogic registerLogic, 
            ILogger<RegisterController> logger)
        {
            _registerLogic = registerLogic;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("User")]
        public async Task<User> CreateUser([FromBody] RegisterUserRequest regUserObj)
        {
            return await _registerLogic.CreateUser(_dbContext, regUserObj);
        }

        [HttpGet]
        [Route("ActivateUser/{temporarySecret}")]
        public async Task<User> CreateUser(string temporarySecret)
        {
            return await _registerLogic.ActivateUser(_dbContext, temporarySecret);
        }

        [HttpGet]
        [Route("ResendActivationEmail/{activationEmail}")]
        public async Task<User> ResendActivationEmail(string activationEmail)
        {
            return await _registerLogic.ResendActivationEmail(_dbContext, activationEmail);
        }

        [HttpPost]
        [Route("ApiKey")]
        [Authorize(Roles = "user")]
        public async Task<ApiKey> CreateApiKey([FromBody] RegisterApiKeyRequest apiKey)
        {
            return await _registerLogic.CreateApiKey(_dbContext, apiKey);
        }
    }
}
