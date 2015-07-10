// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OkNegotiatedIListContentResult.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the OkNegotiatedIListContentResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.WebAPI.Core.Results
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    /// <summary>
    /// The ok negotiated i enumerable content result.
    /// </summary>
    /// <typeparam name="T1">
    /// IList object
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type in the IList
    /// </typeparam>
    public class OkNegotiatedIListContentResult<T1, T2> : OkNegotiatedContentResult<T1> where T1 : IList<T2>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkNegotiatedIListContentResult{T1,T2}"/> class. 
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="firstPageUri">
        /// The first page uri.
        /// </param>
        /// <param name="previousPageUri">
        /// The previous page uri.
        /// </param>
        /// <param name="nextPageUri">
        /// The next page uri.
        /// </param>
        /// <param name="lastPageUri">
        /// The last page uri.
        /// </param>
        /// <param name="currentPageIndex">
        /// The current page index.
        /// </param>
        /// <param name="pageCount">
        /// The page count.
        /// </param>
        public OkNegotiatedIListContentResult(
            T1 content,
            ApiController controller,
            Uri firstPageUri,
            Uri previousPageUri,
            Uri nextPageUri,
            Uri lastPageUri,
            int currentPageIndex,
            int pageCount)
            : base(content, controller)
        {
            this.FirstPageUri = firstPageUri;
            this.PreviousPageUri = previousPageUri;
            this.NextPageUri = nextPageUri;
            this.LastPageUri = lastPageUri;
            this.CurrentPageIndex = currentPageIndex;
            this.TotalPageCount = pageCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OkNegotiatedIListContentResult{T1,T2}"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="contentNegotiator">
        /// The content negotiator.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="formatters">
        /// The formatters.
        /// </param>
        /// <param name="firstPageUri">
        /// The first page uri.
        /// </param>
        /// <param name="previousPageUri">
        /// The previous page uri.
        /// </param>
        /// <param name="nextPageUri">
        /// The next page uri.
        /// </param>
        /// <param name="lastPageUri">
        /// The last page uri.
        /// </param>
        /// <param name="currentPageIndex">
        /// The current page index.
        /// </param>
        /// <param name="pageCount">
        /// The page count.
        /// </param>
        public OkNegotiatedIListContentResult(
            T1 content,
            IContentNegotiator contentNegotiator,
            HttpRequestMessage request,
            IEnumerable<MediaTypeFormatter> formatters,
            Uri firstPageUri,
            Uri previousPageUri,
            Uri nextPageUri,
            Uri lastPageUri,
            int currentPageIndex,
            int pageCount)
            : base(content, contentNegotiator, request, formatters)
        {
            this.FirstPageUri = firstPageUri;
            this.PreviousPageUri = previousPageUri;
            this.NextPageUri = nextPageUri;
            this.LastPageUri = lastPageUri;
            this.CurrentPageIndex = currentPageIndex;
            this.TotalPageCount = pageCount;
        }

        /// <summary>
        /// Gets or sets the first page uri.
        /// </summary>
        public Uri FirstPageUri { get; set; }

        /// <summary>
        /// Gets or sets the previous page uri.
        /// </summary>
        public Uri PreviousPageUri { get; set; }

        /// <summary>
        /// Gets or sets the next page uri.
        /// </summary>
        public Uri NextPageUri { get; set; }

        /// <summary>
        /// Gets or sets the last page uri.
        /// </summary>
        public Uri LastPageUri { get; set; }

        /// <summary>
        /// Gets or sets the total page count.
        /// </summary>
        public int TotalPageCount { get; set; }

        /// <summary>
        /// Gets or sets the current page index.
        /// </summary>
        public int CurrentPageIndex { get; set; }

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

            // Add our custom headers
            // The total page count
            response.Headers.Add("X-Total-Page-Count", string.Format("{0}", this.TotalPageCount));

            // The current page index
            response.Headers.Add("X-Current-Page", string.Format("{0}", this.CurrentPageIndex));

            // The total number of items
            response.Headers.Add("X-Total-Count", string.Format("{0}", this.Content.Count));

            // The link header
            var link = string.Format(
                    "<{0}>; rel=\"first\", <{1}>; rel=\"previous\", <{2}>; rel=\"next\", <{3}>; rel=\"last\"",
                    this.FirstPageUri,
                    this.PreviousPageUri,
                    this.NextPageUri,
                    this.LastPageUri);
            response.Headers.Add("Link", link);

            return response;
        }
    }
}