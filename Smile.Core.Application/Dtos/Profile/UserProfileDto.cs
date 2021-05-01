using System;

namespace Smile.Core.Application.Dtos.Profile
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime DateRegistered { get; set; }
        public string PhotoUrl { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}