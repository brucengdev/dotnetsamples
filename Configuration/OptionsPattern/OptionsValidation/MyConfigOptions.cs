using System.ComponentModel.DataAnnotations;

namespace OptionsValidation;

public class MyConfigOptions
{
    public const string MyConfig = "MyConfig";
    
    [RegularExpression(@"^[a-zA-Z]+$")]
    public string Key1 { get; set; }
    
    [Range(1, 20, ErrorMessage = "Value for {0} must be from {1} to {2}")]
    public int Key2 { get; set; }
    public int Key3 { get; set; }
}