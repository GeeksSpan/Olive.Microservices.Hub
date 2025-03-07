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
    public partial class SideBarTopModule : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(vm.SideBarTopModule info)
        {
            return View(await Bind<vm.SideBarTopModule>(info));
        }
    }
}

namespace Controllers
{
    
    #pragma warning disable
    public partial class SideBarTopModuleController : BaseController
    {
    }
}

namespace ViewModel
{
    
    #pragma warning disable
    [BindingController(typeof(Controllers.SideBarTopModuleController))]
    public partial class SideBarTopModule : IViewModel
    {
    }
}