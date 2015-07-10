// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the BaseApiController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.WebAPI.Core.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Results;

    using NLog;

    using NSWRFS.WebAPI.Core.Results;

    /// <summary>
    /// The base API controller. 
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// The NLog logger.
        /// </summary>
        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The unprocessable entity.
        /// </summary>
        /// <returns>
        /// The <see cref="UnprocessableEntityNegotiatedContentResult"/>.
        /// </returns>
        protected internal virtual UnprocessableEntityNegotiatedContentResult UnprocessableEntity()
        {
            return new UnprocessableEntityNegotiatedContentResult(this);
        }

        /// <summary>
        /// The unsupported media type.
        /// </summary>
        /// <returns>
        /// The <see cref="UnsupportedMediaTypeNegotiatedContentResult"/>.
        /// </returns>
        protected internal virtual UnsupportedMediaTypeNegotiatedContentResult UnsupportedMediaType()
        {
            return new UnsupportedMediaTypeNegotiatedContentResult(this);
        }

        /// <summary>
        /// The gone.
        /// </summary>
        /// <returns>
        /// The <see cref="GoneNegotiatedContentResult"/>.
        /// </returns>
        protected internal virtual GoneNegotiatedContentResult Gone()
        {
            return new GoneNegotiatedContentResult(this);
        }

        /// <summary>
        /// The method not allowed.
        /// </summary>
        /// <returns>
        /// The <see cref="MethodNotAllowedNegotiatedContentResult"/>.
        /// </returns>
        protected internal virtual MethodNotAllowedNegotiatedContentResult MethodNotAllowed()
        {
            return new MethodNotAllowedNegotiatedContentResult(this);
        }

        /// <summary>
        /// The not modified.
        /// </summary>
        /// <returns>
        /// The <see cref="NotModifiedNegotiatedContentResult"/>.
        /// </returns>
        protected internal virtual NotModifiedNegotiatedContentResult NotModified()
        {
            return new NotModifiedNegotiatedContentResult(this);
        }

        /// <summary>
        /// The no content.
        /// </summary>
        /// <returns>
        /// The <see cref="NoContentNegotiatedContentResult"/>.
        /// </returns>
        protected internal virtual NoContentNegotiatedContentResult NoContent()
        {
            return new NoContentNegotiatedContentResult(this);
        }

        /// <summary>
        /// The ok list.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="actionBaseUri">
        /// The action base uri.
        /// </param>
        /// <param name="currentPageIndex">
        /// The current page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <typeparam name="T">
        /// The content type.
        /// </typeparam>
        /// <returns>
        /// The OkNegotiatedIListContentResult.
        /// </returns>
        protected internal virtual OkNegotiatedIListContentResult<IList<T>, T> OkList<T>(IList<T> list, Uri actionBaseUri, int currentPageIndex, int pageSize) where T : class
        {
            // Work out the index of our last page
            var pagesCount = list.Count / pageSize;

            // Set the Uri
            var actionBaseUriString = actionBaseUri.ToString();
            if (!actionBaseUriString.EndsWith("/"))
            {
                actionBaseUriString += "/";
            }

            // Calculate the Uris
            var firstPageUri = new Uri(string.Format("{0}?page=1&per_page={1}", actionBaseUriString, pageSize));
            var previousPageUri = new Uri(string.Format("{0}?page={1}&per_page={2}", actionBaseUriString, currentPageIndex > 1 ? (currentPageIndex - 1) : 1, pageSize));
            var nextPageUri = new Uri(string.Format("{0}?page={1}&per_page={2}", actionBaseUriString, currentPageIndex + 1, pageSize));
            var lastPageUri = new Uri(actionBaseUri, string.Format("{0}?page={1}&per_page={2}", actionBaseUriString, pagesCount > 0 ? pagesCount : 1, pageSize));

            // Calculate the skip
            var skip = (pageSize * currentPageIndex) - pageSize;

            // Create the return list
            var returnList = list.Skip(skip).Take(pageSize).ToList();

            // Create the response
            var response = new OkNegotiatedIListContentResult<IList<T>, T>(returnList, this, firstPageUri, previousPageUri, nextPageUri, lastPageUri, currentPageIndex, pagesCount);

            return response;
        }

        /// <summary>
        /// The ok list.
        /// </summary>
        /// <param name="list">
        /// The list.
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
        /// <typeparam name="T">
        /// The content type
        /// </typeparam>
        /// <returns>
        /// The OkNegotiatedIListContentResult
        /// </returns>
        protected internal virtual OkNegotiatedIListContentResult<IList<T>, T> OkList<T>(IList<T> list, Uri firstPageUri, Uri previousPageUri, Uri nextPageUri, Uri lastPageUri, int currentPageIndex, int pageCount)
        {
            // Create the response
            var response = new OkNegotiatedIListContentResult<IList<T>, T>(list, this, firstPageUri, previousPageUri, nextPageUri, lastPageUri, currentPageIndex, pageCount);
            return response;
        }
    }
}