﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySvc.Framework.Infrastructure.Crosscutting.ViewModel;

namespace Catalog.API.ViewModels
{
    public class CreateProductViewModel : IViewModel
    {
        public string SKU { get; set; }
        public string HeadLine { get; set; }
        public decimal Price { get; set; }
    }
}
