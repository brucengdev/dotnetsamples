using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace IValidateOptionsExample;

public class MyConfigOptionsValidator: IValidateOptions<MyConfigOptions>
{
    private MyConfigOptions _myConfig { get; set; }
    public MyConfigOptionsValidator(IConfiguration config)
    {
        _myConfig = config.GetSection(MyConfigOptions.MyConfig).Get<MyConfigOptions>();
    }
    public ValidateOptionsResult Validate(string? name, MyConfigOptions options)
    {
        var errors = new List<string>();

        var regex = new Regex("^[a-zA-Z]+$");
        if (!regex.Match(options.Key1).Success)
        {
            errors.Add("Key1 is invalid");
        }

        if (options.Key2 < 10 || options.Key2 > 90)
        {
            errors.Add("Key2 must be between 10 and 90 inclusive.");
        }

        if (options.Key2 <= options.Key3)
        {
            errors.Add("Key 2 must be greater than key 3");
        }

        if (errors.Count > 0)
        {
            return ValidateOptionsResult.Fail(errors);
        }
        return ValidateOptionsResult.Success;
    }
}