﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using Olive;
using Olive.Entities;
using Olive.Mvc;
using Olive.Web;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using vm = ViewModel;
using Olive.Microservices.Hub;
using ViewModel;

namespace Controllers
{

#pragma warning disable
    public partial class UnauthorizedController : BaseController
    {
        [Route("Unauthorized/{feature}")]
        public async Task<ActionResult> Index(Guid? feature)
        {
            ViewData["LeftMenu"] = "FeaturesSideMenu";
            var item = Feature.All.FirstOrDefault(x => x.ID == feature);
            return View(item);
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class UnauthorizedAccess : IViewModel
    {
        [ValidateNever]
        public Feature Item { get; set; }
    }
}