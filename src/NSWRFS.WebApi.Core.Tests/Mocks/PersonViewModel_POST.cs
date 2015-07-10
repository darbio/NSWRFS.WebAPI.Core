// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonViewModel_POST.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   The person view model_ post.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.WebAPI.Core.Tests.Mocks
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The person view model_ post.
    /// </summary>
    public class PersonViewModel_POST
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
