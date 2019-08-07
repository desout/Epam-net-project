using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EpamNetProject.BLL.Infrastucture
{
    public static class ModelValidation
    {
        public static string IsValidModel<T>(T entity)
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(
                entity, context, results, 
                validateAllProperties: true
            );
            var result = results.FirstOrDefault();
            return result != null ? result.ToString(): null;
        }
    }
}
