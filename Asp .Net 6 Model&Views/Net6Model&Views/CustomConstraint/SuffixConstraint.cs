using System.ComponentModel.DataAnnotations;

namespace Net6Model_Views.CustomConstraint
{
    public class SuffixConstraint:ValidationAttribute
    {
        private readonly string _firstname;

        public SuffixConstraint(string firstname)
        {
            _firstname = firstname;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //we cannot access first name filed vlaue directly we need to get reference of feild then get value by reflection
            var firstNameField = validationContext.ObjectType.GetProperty(_firstname);
            //this value will get through reflection 
            //reflection allow us to read meta data field and properties of the object at runtime
            var firstNameValue = firstNameField.GetValue(validationContext.ObjectInstance).ToString();
            if (value != firstNameValue[0].ToString())
            {
                //error message will get value from model
                return new ValidationResult(ErrorMessage,new string[] {validationContext.MemberName});
            }

            return null;
        }
    }
}
