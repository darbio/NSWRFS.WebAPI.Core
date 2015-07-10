// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessException.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the BusinessException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.WebAPI.Core.Exceptions
{
    using System;

    /// <summary>
    /// The business exception.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BusinessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }
    }
}