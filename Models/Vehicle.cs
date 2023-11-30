using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TriniCars4SaleWithLogin.Models
{
    public class Vehicle
    {
        [Display(Name = "ID No.")]
        public int VehicleId { get; set; }
        public string? OwnerID { get; set; }

        [Display(Name = "License")]
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string? LicensePlate { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Colour { get; set; }
        [Display(Name = "Engine")]
        public string? EngineSize { get; set; }
        public string? Mileage { get; set; }
        public string? Transmission { get; set; }
        public string? Features { get; set; }
        [Display(Name = "Additionals")]
        public string? AdditionalInfo { get; set; }
        [Display(Name = "Price")]
        [Range(1, 100000, ErrorMessage = "Payment amount is required between .01 and $100,000.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AskingPrice { get; set; }

        [Display(Name = "Thumbnail")]
        public string? ThumbUrl { get; set; }

        [NotMapped]
        public IFormFile? ThumbFile { get; set; }

        public string? AdditionalUrl1 { get; set; }
        [NotMapped]
        public IFormFile? AdditionalFile1 { get; set; }

        public string? AdditionalUrl2 { get; set; }
        [NotMapped]
        public IFormFile? AdditionalFile2 { get; set; }
        [Display(Name = "Owner")]
        public string? ContactName { get; set; }
        [Display(Name = "Contact")]
        public string? ContactNum { get; set; }

        public VehicleStatus Status { get; set; }
    }

    public enum VehicleStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
    

