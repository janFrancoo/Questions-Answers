﻿using Core.Entities;

namespace Entities.Dtos
{
    public class UserForRegisterDto: IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
