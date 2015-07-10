// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonContentNegotiator.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the JsonContentNegotiator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.ContentNegotiators
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    /// <summary>
    /// The JSON content negotiator.
    /// </summary>
    public class JsonContentNegotiator : IContentNegotiator
    {
        /// <summary>
        /// The JSON formatter.
        /// </summary>
        private readonly JsonMediaTypeFormatter jsonFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonContentNegotiator"/> class.
        /// </summary>
        /// <param name="formatter">
        /// The formatter.
        /// </param>
        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            this.jsonFormatter = formatter;
        }

        /// <summary>
        /// The negotiate.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="formatters">
        /// The formatters.
        /// </param>
        /// <returns>
        /// The <see cref="ContentNegotiationResult"/>.
        /// </returns>
        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            var result = new ContentNegotiationResult(this.jsonFormatter, new MediaTypeHeaderValue("application/json"));
            return result;
        }
    }
}