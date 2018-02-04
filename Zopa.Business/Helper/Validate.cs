using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zopa.Business.Helper
{
    public static class Validate
    {
        public static ICollection<ValidationResult> ValidateObject(object value)
        {
            ValidationContext context = new ValidationContext(value, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(value, context, results, true);
            return results;
        }
    }
}