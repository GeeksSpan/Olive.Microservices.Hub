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

namespace ViewComponents
{
    
    #pragma warning disable
    public partial class BreadcrumbWrapper : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(vm.BreadcrumbWrapper info)
        {
            return View(await Bind<vm.BreadcrumbWrapper>(info));
        }
    }
}

namespace Controllers
{
    
    #pragma warning disable
    public partial class BreadcrumbWrapperController : BaseController
    {
    }
}

namespace ViewModel
{
    
    #pragma warning disable
    [BindingController(typeof(Controllers.BreadcrumbWrapperController))]
    public partial class BreadcrumbWrapper : IViewModel
    {
    }
}