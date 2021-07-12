using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Helmes.Helpers.CustomValidation
{
    public class CountryCodeValidator : ValidationAttribute
    {
            public override string FormatErrorMessage(string name)
            {
                return "Incorrect country code";
            }

            protected override ValidationResult IsValid(object objValue,
                                                           ValidationContext validationContext)
            {

            var isCountryCodeValid = CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                    .Select(culture => new RegionInfo(culture.LCID))
                        .Any(region => region.TwoLetterISORegionName == objValue.ToString());

            if (isCountryCodeValid == false)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
                return ValidationResult.Success;
            }

    }
}
