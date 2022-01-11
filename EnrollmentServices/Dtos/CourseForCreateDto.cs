using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentServices.Dtos
{
    public class CourseForCreateDto : IValidatableObject
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int Credits { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Title.StartsWith("Training") || Title.Length >= 50)
            {
                yield return new ValidationResult("Harus dimulai dengan kata Training dan lebih kecil dari 50 karakter",
                    new[] { "Title" });
            }

            if (Credits >= 10)
            {
                yield return new ValidationResult("harus lebih kecil dari 10 karakter",
                    new[] { "Credits" });
            }
        }
    }
}
