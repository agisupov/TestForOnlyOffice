using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestForOnlyOffice.Model
{
    public class Person
    {
        public int PersonId { get; set; }

        //[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Некорректное имя, имя пишется с заглавной буквы и только буквами")]
        [Required(ErrorMessage = "Не указано имя")]
        [BindRequired]
        public string FirstName { get; set; }

        //[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Некорректная фамилия, фамилия пишется с заглавной буквы и только буквами")]
        [Required(ErrorMessage = "Не указана фамилия")]
        [BindRequired]
        public string LastName { get; set; }
    }
}
