﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public class ClientConfiguration : IClientConfiguration
    {
        public string UserId { get; set; }
    }
}
