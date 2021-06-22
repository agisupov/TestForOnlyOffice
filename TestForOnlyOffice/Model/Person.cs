using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestForOnlyOffice.Model
{
    public class Person
    {
        [JsonPropertyName("personId")]
        public string PersonId { get; set; } //string, чтобы мог использовать Guid, с int не получается

        [JsonPropertyName("first_name")]
        [Required(ErrorMessage = "Не указано имя")]
        [BindRequired]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [Required(ErrorMessage = "Не указана фамилия")]
        [BindRequired]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Неправильный адрес")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required]
        [MinLength(6, ErrorMessage = "Пароль не должен быть меньше 6 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
