using ECommerceModelBinding.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ECommerceModelBinding.Models
{
    public class Order
    {
        [BindNever]
        public int? OrderNo { get; set; }

        [Required(ErrorMessage ="{0} can't be blank!")]
        [OrderDateValidator]
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        [DataType(DataType.Currency, ErrorMessage ="{0} must be a valid number!")]
        [InvoiceValidator]
        public double? InvoicePrice { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        public List<Product>? Products { get; set; }
    }
}
