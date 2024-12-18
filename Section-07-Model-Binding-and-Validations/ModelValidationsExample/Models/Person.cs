using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidationsExample.Validations;

// import our custom validations attribute
namespace ModelValidationsExample.Models;

public class Person : IValidatableObject{
    [Required(ErrorMessage = "{0} cannot be empty or null mate!")]
    [Display(Name = "Person Name")]
    [StringLength(20,MinimumLength =1)]
    [ValidateNever]
    [RegularExpression(
      pattern: @"^[A-Za-z .]+$", 
      ErrorMessage ="{0} should only contain alpha, space or dot")]
    public string? Name { get; set; } 

    [EmailAddress]
    [Required]
    [ValidateNever]
    public string? Email { get; set; } 

    [Phone]
    [ValidateNever] // disable validation
    public string? Phone { get; set; } 

    [Required]
    [ValidateNever]
    public string? Password { get; set; } 

    [Compare(otherProperty: "Password")]
    [Required]
    [ValidateNever]
    public string? ConfirmationPassword { get; set; } 

    [Range(0.00, 100.0)]
    [ValidateNever]
    public double? Price { get; set; } 

    [MinimumYearValidator(1999, ErrorMessage = "The error must be greater than or equal to {0}")]
    [ValidateNever]
    public DateOnly? Dob { get; set; } 

    [ValidateNever]
    public DateTime FromDate {get; set;}  

    [DateRangeValidator("FromDate", ErrorMessage = "'FromDate' should be older than or equal to the 'ToDate'")]
    [ValidateNever]
    public DateTime ToDate {get; set;}  

    [ValidateNever]
    public int? Age{get; set;}

    public List<string?> Tags {get; set;} = new List<string?>();

    // validation
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext){
      if(Age.HasValue) {

        if(Age > 54) {
          yield return new ValidationResult("Dang, your old mate! You can't enter this club", new []{nameof(Age)});
        }
        else {
          yield return ValidationResult.Success;
        }

      }

      yield return ValidationResult.Success;
    }

}