﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Resources
{
    public interface ICurrentUserService
    {
        string GetCurrentUsername();
    }
}
