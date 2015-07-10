// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultJsonFormatter.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the DefaultJsonFormatter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.WebAPI.Core.Formatters
{
    using System;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;

    using NSWRFS.WebAPI.Core.ContractResolvers;

    /// <summary>
    /// The default JSON formatter.
    /// </summary>
    public class DefaultJsonFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultJsonFormatter"/> class.
        /// </summary>
        public DefaultJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SerializerSettings.Formatting = Formatting.Indented;
            this.SerializerSettings.ContractResolver = new LowerCaseDelimitedPropertyNamesContractResolver('_');
        }

        /// <summary>
        /// The set default content headers.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}