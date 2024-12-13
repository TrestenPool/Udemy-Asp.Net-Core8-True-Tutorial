using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidationsExample.Validations;

// import our custom validations attribute
namespace ModelValidationsExample.Models;

public class Person{
    [Required(ErrorMessage = "{0} cannot be empty or null mate!")]
    [Display(Name = "Person Name")]
    [StringLength(20,MinimumLength =1)]
    [RegularExpression(@"^[A-Za-z .]+$", ErrorMessage ="{0} should only contain alpha, space or dot")]
    public string? Name { get; set; } 

    [EmailAddress]
    [Required]
    public string? Email { get; set; } 

    [Phone]
    [ValidateNever] // disable validation
    public string? Phone { get; set; } 

    [Required]
    public string? Password { get; set; } 

    [Compare(otherProperty: "Password")]
    [Required]
    public string? ConfirmationPassword { get; set; } 

    [Range(0.00, 100.0)]
    public double? Price { get; set; } 

    [MinimumYearValidator(1999, ErrorMessage = "The error must be greater than or equal to {0}")]
    public DateOnly? Dob { get; set; } 

    public DateTime FromDate {get; set;}  

    [DateRangeValidator("FromDate", ErrorMessage = "'FromDate' should be older than or equal to the 'ToDate'")]
    public DateTime ToDate {get; set;}  
}