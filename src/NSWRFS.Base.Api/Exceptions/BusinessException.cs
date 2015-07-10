// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessException.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the BusinessException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.Exceptions
{
    using NSWRFS.WebAPI.Core.Exceptions;
    using System;

    /// <summary>
    /// The business exception.
    /// </summary>
    public class ExampleException : BusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ExampleException(string message)
            : base(message)
        {
        }
    }
}