using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestForOnlyOffice.Model
{
    public class Person
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("first_name")]
        [Required(ErrorMessage = "FirstNameRequired")]
        [BindRequired]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [Required(ErrorMessage = "LastNameRequired")]
        [BindRequired]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "EmailRequired")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "EmailAddress")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "PasswordRequired")]
        [MinLength(6, ErrorMessage = "PasswordLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("avatar")]
        public byte[] Avatar { get; set; }

        [NotMapped]
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        public IFormFile AvatarFile { get; set; }
    }
}
