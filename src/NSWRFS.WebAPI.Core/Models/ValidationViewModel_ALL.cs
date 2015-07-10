// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationViewModel_ALL.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the ValidationViewModel_ALL type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.WebAPI.Core.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The validation view model_ all.
    /// </summary>
    public class ValidationViewModel_ALL
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public List<ValidationFieldViewModel_ALL> Errors { get; set; }
    }
}