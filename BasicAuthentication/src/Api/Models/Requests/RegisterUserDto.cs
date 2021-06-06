﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models.Requests
{
    public class RegisterUserDto
    {
        [JsonPropertyName("first_name")]
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [JsonPropertyName("date_of_birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("phone")]
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }

        [JsonPropertyName("username")]
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
    }
}
