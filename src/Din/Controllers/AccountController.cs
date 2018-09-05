﻿using System;
using System.IO;
using System.Threading.Tasks;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AccountController : BaseController
    {
        #region fields

        private readonly IAccountService _service;

        #endregion fields

        #region constructors

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints

        [Authorize, HttpGet]
        public async Task<IActionResult> GetUserViewAsync()
        {
            return PartialView("~/Views/Account/_Account.cshtml",
                await _service.GetAccountDataAsync(GetCurrentSessionId(), HttpContext.Request.Headers["User-Agent"].ToString()));
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> UploadAccountImageAsync(IFormFile file)
        {
            var ms = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(ms);
            return PartialView("~/Views/Main/Partials/_Result.cshtml",
                await _service.UploadAccountImageAsync(GetCurrentSessionId(), file.Name, ms.ToArray()));
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> GetMovieCalendarAsync()
        {
            return Ok(await _service.GetMovieCalendarAsync());
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> GetTvShowCalendarAsync()
        {
            throw new NotImplementedException();
        }

        #endregion endpoints
    }
}
