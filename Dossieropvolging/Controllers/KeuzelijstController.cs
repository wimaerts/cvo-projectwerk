﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dossieropvolging.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KeuzelijstController : Controller
    {
        // GET: Keuzelijst
        public ActionResult Index()
        {
            return View();
        }
    }
}