using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NET_Task.Models
{
    public enum typeOfBook
    {
        Fiction,
        Nonfiction
    }
    public class Book
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        [Range(1000, 2019, ErrorMessage = "Year of publication must be between 1000 and 2019.")]
        [Display(Name = "Year of publication")]
        public int YearOfPublication { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        [Display(Name = "Type of book")]
        public typeOfBook TypeOfBook { get; set; }

        [DataType(DataType.MultilineText)]
        public string Sages { get; set; }
    }
}