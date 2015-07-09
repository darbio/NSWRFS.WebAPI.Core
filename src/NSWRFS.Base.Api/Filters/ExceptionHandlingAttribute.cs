// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingAttribute.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   The exception handling attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Filters;

    using NSWRFS.Base.Api.Exceptions;
    using NSWRFS.Base.Api.Models;

    /// <summary>
    /// The exception handling attribute.
    /// </summary>
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="actionExecutedContext">
        /// The action executed context.
        /// </param>
        /// <exception cref="HttpResponseException">
        /// HttpResponseException
        /// </exception>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // If this is a business level exception, show it to the client.
            if (actionExecutedContext.Exception is BusinessException)
            {
                var exception = actionExecutedContext.Exception as BusinessException;
                var viewModel = new ErrorViewModel_ALL(exception);

                throw new HttpResponseException(
                    new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new ObjectContent(typeof(ErrorViewModel_ALL), viewModel, new JsonMediaTypeFormatter()),
                        ReasonPhrase = viewModel.Description
                    });
            }
            else
            {
                // Show a generic exception to the end user
                var viewModel = new ErrorViewModel_ALL(actionExecutedContext.Exception);
                throw new HttpResponseException(
                    new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new ObjectContent(typeof(ErrorViewModel_ALL), viewModel, new JsonMediaTypeFormatter()),
                        ReasonPhrase = viewModel.Description
                    });
            }
        }

        /// <summary>
        /// The on exception async.
        /// </summary>
        /// <param name="actionExecutedContext">
        /// The action executed context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Run(() => this.OnException(actionExecutedContext), cancellationToken);
        }
    }
}