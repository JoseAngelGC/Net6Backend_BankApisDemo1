﻿namespace BancoApis.DomainServices.Dtos.Requests
{
    public class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
