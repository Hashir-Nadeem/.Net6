using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Net6Model_Views.CustomConstraint;
using System.ComponentModel.DataAnnotations;

namespace Net6Model_Views.Models
{
    //model help us in structing and respresentation of our data 
    //it help us in applying validation and reusing same data and structre at different places 
    public class Person
    {
        //[FromQuery] if we apply this attribute to the model property so it will ignore precendence 
        [Required]
        public string  FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string  LastName { get; set; }
        [StringLength(100)]
        public List<string> Addresses { get; set; }

        [BindNever]
        public int PersonNo { get; set; }

        //suffix must be equal to first name first two characters
        [SuffixConstraint("FirstName",ErrorMessage ="Suffix must be equal to first two character of First name")]
        public string Suffix { get; set; }
    }
}
