﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Helpers;

namespace UserManagement.Domain.Entities
{
    [Table("[User]")]
    public class User
    {
        [Dapper.Contrib.Extensions.Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [MinLength(2)]
        [Required]
        [Name]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [MinLength(2)]
        [Required]
        [Name]
        public string LastName { get; set; }

        [Required]
        [MinimumAge(16)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MaxLength(50)]
        public string Resident { get; set; }

        [MaxLength(50)]
        [Required]
        public string PrivateID { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public DateTime? RegistrationDate { get; set; }

        [MaxLength(50)]
        [Required]
        public string RegistrationIP { get; set; }

        [MaxLength(50)]
        [Required]
        public string Language { get; set; }

        [MaxLength(50)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        [Required]
        public string Mobile { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [ComplexPassword]
        public string Password { get; set; }

        [Computed]
        [Write(false)]
        public IEnumerable<Address> Addresses { get; set; }
    }
}
