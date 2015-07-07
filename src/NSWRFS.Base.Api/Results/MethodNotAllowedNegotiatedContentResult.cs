// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodNotAllowedNegotiatedContentResult.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the NotModifiedNegotiatedContentResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.Results
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    /// <summary>
    /// The 405 negotiated content result.
    /// </summary>
    public class MethodNotAllowedNegotiatedContentResult : OkNegotiatedContentResult<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodNotAllowedNegotiatedContentResult"/> class.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        public MethodNotAllowedNegotiatedContentResult(ApiController controller)
            : base(null, controller)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodNotAllowedNegotiatedContentResult"/> class.
        /// </summary>
        /// <param name="contentNegotiator">
        /// The content negotiator.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="formatters">
        /// The formatters.
        /// </param>
        public MethodNotAllowedNegotiatedContentResult(
            IContentNegotiator contentNegotiator,
            HttpRequestMessage request,
            IEnumerable<MediaTypeFormatter> formatters)
            : base(null, contentNegotiator, request, formatters)
        {
        }

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
            response.StatusCode = HttpStatusCode.MethodNotAllowed;
            return response;
        }
    }
}