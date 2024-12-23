using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_App.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_Commerce_App.Models;

public class Order : IValidatableObject{
  [BindNever]
  public int? OrderNo { get; set; }

  [Required]
  [MinimumYear("2005-01-01")]
  public DateTime? OrderDate { get; set; }

  [Required]
  public double? InvoicePrice { get; set; }

  [Required]
  public List<Product> Products { get; set; } = new List<Product>();

  // our validate method
  public IEnumerable<ValidationResult> Validate(ValidationContext validationContext){

    // makes sure there is at least one product    
    if(Products.Count == 0) {
      yield return new ValidationResult("There must be at least one Product");
    }

    double priceOfAllProducts = 0;
    
    // go through all of the products
    foreach(var product in Products) {
      priceOfAllProducts += (product.Price * product.Quantity);
    }

    // the invoice price and the price of all of the products does not match
    if(priceOfAllProducts != InvoicePrice){
      yield return new ValidationResult($"The price {priceOfAllProducts} and {InvoicePrice} do not match up");
    }

    yield return ValidationResult.Success;
  }

}