// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OkNegotiatedIListContentResult.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the OkNegotiatedIListContentResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Collections;
    using System.Web.Http;
    using System.Web.Http.Results;

    /// <summary>
    /// The ok negotiated i enumerable content result.
    /// </summary>
    /// <typeparam name="T">
    /// IList object
    /// </typeparam>
    public class NoContentNegotiatedContentResult : OkNegotiatedContentResult<object>
    {
        public NoContentNegotiatedContentResult(ApiController controller)
            : base(null, controller)
        {
        }

        public NoContentNegotiatedContentResult(
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
            response.StatusCode = HttpStatusCode.NoContent;
            return response;
        }
    }
}