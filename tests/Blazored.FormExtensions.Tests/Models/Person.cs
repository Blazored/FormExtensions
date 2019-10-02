using System.ComponentModel.DataAnnotations;

namespace Blazored.FormExtensions.Tests.Models
{
    public class Person
    {
        [Display(Name = "Person_First")]
        public string First { get; set; }

        [Display(Name = "Person_Last")]
        public string Last { get; set; }

        public string EmailAddress { get; set; }
    }
}