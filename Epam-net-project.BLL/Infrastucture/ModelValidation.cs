using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EpamNetProject.BLL.Infrastucture
{
    public static class ModelValidation
    {
        public static string IsValidModel<T>(T entity)
        {
            var context = new ValidationContext(entity, null, null);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(
                entity, context, results,
                true
            );
            var result = results.FirstOrDefault();
            return result?.ToString();
        }
    }
}
