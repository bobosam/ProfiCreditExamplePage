namespace ProfiCreditCars.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class contains all properties to verify submitted data.
    /// </summary>
    public class CarBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Model { get; set; }

        // In 1768 the first steam-powered automobile capable of human transportation was built by Nicolas-Joseph Cugnot.
        [Range(1768, Int32.MaxValue)]
        [Required]
        public int Year { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Color { get; set; }
    }
}