using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNet.Mvc;

namespace Acme.Helpers.Website.Models
{
    [DebuggerDisplayAttribute("{Id}-{FirstName}")]
    public class Person
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }
        public string Location { get; }
        public decimal Salary { get; }
        public PersonAddress Address { get; }
        public string FavoriteColor { get; }

        public Person() { }

        public Person(int id, string firstName, string lastName, string email, DateTime birthDate, string location, PersonAddress address, decimal salary, string favoriteColor)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            Location = location;
            Address = address;
            Salary = salary;
            FavoriteColor = favoriteColor;
        }
    }
}
