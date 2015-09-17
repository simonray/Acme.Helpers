using Microsoft.AspNet.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Helpers.Website.Models
{
    public class DetailedPersonView : BasicPersonView
    {
        public DetailedPersonView(Person p)
            : base(p)
        {
            Gender = p.Gender;
            Salary = p.Salary;
            Address = p.Address;
        }

        //Enumeration
        public Gender Gender { get; set; }

        //Specify a custom data format
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Salary { get; set; }

        //Ignore this property
        [HiddenInput(DisplayValue = false)]
        public PersonAddress Address { get; set; }

        //Specify specific text if the property value is null
        [DisplayFormat(NullDisplayText = "Not specified")]
        public string AddressLines { get { return Address?.AddressLine1; } }
    }
}
