using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EpamNetProject.BLL.Infrastructure
{
    public static class ModelValidation
    {
        public static void IsValidModel<T>(T entity)
        {
            var context = new ValidationContext(entity, null, null);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(
                entity, context, results,
                true
            );
            if (results.Any())
            {
                throw new ArgumentException(results.FirstOrDefault()?.ToString());
            }
        }
    }
}
