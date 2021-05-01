using System;
using System.Collections.Generic;

namespace Smile.Core.Application.Dtos.Auth
{
    public class UserAuthDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime DateRegistered { get; set; }
        public string PhotoUrl { get; set; }
        public bool EmailConfirmed { get; set; }

        public ICollection<UserRoleDto> UserRoles { get; set; }
    }
}