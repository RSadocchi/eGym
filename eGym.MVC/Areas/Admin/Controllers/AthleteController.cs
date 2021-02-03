using AutoMapper;
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
    public class AthleteController : Controller
    {

        public AthleteController()
        {

        }


        public IActionResult Index() => View();
        


    }
}
