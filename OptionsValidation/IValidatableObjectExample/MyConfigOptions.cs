using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace IValidatableObjectExample;

internal class MyConfigOptions: IValidatableObject
{
    public const string MyConfig = "MyConfig";
    
    public string Key1 { get; set; }
    public int Key2 { get; set; }
    public int Key3 { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        var regex = new Regex("^[a-zA-Z]+$");
        if (!regex.Match(Key1).Success)
        {
            errors.Add(new ValidationResult("Key1 is invalid"));
        }

        if (Key2 < 10 || Key2 > 90)
        {
            errors.Add(new ValidationResult("Key2 must be between 10 and 90 inclusive."));
        }

        if (Key2 <= Key3)
        {
            errors.Add(new ValidationResult("Key 2 must be greater than key 3"));
        }

        return errors;
    }
}