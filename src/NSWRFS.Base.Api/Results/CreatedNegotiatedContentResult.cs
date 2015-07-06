// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OkCreatedNegotiatedContentResult.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   The ok created negotiated content result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.Results
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    /// <summary>
    /// The ok created negotiated content result.
    /// </summary>
    /// <typeparam name="T">
    /// The type to include in Content
    /// </typeparam>
    public class OkCreatedNegotiatedContentResult<T> : OkNegotiatedContentResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkCreatedNegotiatedContentResult{T}"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="createdResourceUri">
        /// The Uri of the created resource.
        /// </param>
        public OkCreatedNegotiatedContentResult(T content, ApiController controller, Uri createdResourceUri) : base(content, controller)
        {
            this.CreatedResourceUri = createdResourceUri;
        }

        /// <summary>
        /// Gets or sets the Uri of the created resource.
        /// </summary>
        public Uri CreatedResourceUri { get; set; }

        /// <summary>
        /// The execute async.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await base.ExecuteAsync(cancellationToken);

            // Set the status code
            response.StatusCode = HttpStatusCode.Created;
            response.Headers.Location = this.CreatedResourceUri;

            return response;
        }
    }
}