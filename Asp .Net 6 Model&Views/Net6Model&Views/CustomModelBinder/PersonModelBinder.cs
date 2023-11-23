using Microsoft.AspNetCore.Mvc.ModelBinding;
using Net6Model_Views.Models;

namespace Net6Model_Views.CustomModelBinder
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var person = new Person();
            person.PersonNo = 001;
            person.FirstName=bindingContext.ValueProvider.GetValue(nameof(Person.FirstName)).FirstValue;
            person.LastName = bindingContext.ValueProvider.GetValue(nameof(Person.LastName)).FirstValue;
            person.Suffix=bindingContext.ValueProvider.GetValue(nameof(Person.Suffix)).FirstValue;
            bindingContext.Result=ModelBindingResult.Success(person);
            return Task.CompletedTask;

        }
    }
}
