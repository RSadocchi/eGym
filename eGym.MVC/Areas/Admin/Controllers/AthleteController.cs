using AutoMapper;
using eGym.Application;
using eGym.Application.DTO;
using eGym.Application.Services;
using eGym.Core.Domain;
using eGym.Core.Security;
using eGym.MVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    [Authorize(Policy = Const_ClaimTypes.ADMINISTRATOR)]
    public class AthleteController : Controller
    {
        readonly IMapper _mapper;
        readonly IAnagService _anagService;

        public AthleteController(
            IMapper mapper,
            IAnagService anagService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _anagService = anagService ?? throw new ArgumentNullException(nameof(anagService));
        }


        public IActionResult Index() => View();
        
        [HttpPost("list")]
        public async Task<IActionResult> List(AnagFilters filters)
        {
            var entities = await _anagService.ListAsync(filters: filters);
            var dtos = _mapper.Map(entities, new List<AnagDTO>());
            return Ok(dtos);
        }

    }
}
