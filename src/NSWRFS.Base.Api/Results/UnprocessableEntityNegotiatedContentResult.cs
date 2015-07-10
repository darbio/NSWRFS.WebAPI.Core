// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnprocessableEntityNegotiatedContentResult.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the UnprocessableEntityNegotiatedContentResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.Results
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Results;

    using NSWRFS.Base.Api.Formatters;
    using NSWRFS.Base.Api.Models;
    using NSWRFS.Base.Api.ContractResolvers;

    using WebGrease.Css.Extensions;

    /// <summary>
    /// The 405 negotiated content result.
    /// </summary>
    public class UnprocessableEntityNegotiatedContentResult : OkNegotiatedContentResult<object>
    {
        /// <summary>
        /// The model state.
        /// </summary>
        private ModelStateDictionary modelState;

        /// <summary>
        /// The formatterCollection.
        /// </summary>
        private MediaTypeFormatterCollection formatterCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityNegotiatedContentResult"/> class.
        /// </summary>
        /// <param name="controller">
        /// The formatterCollection.
        /// </param>
        public UnprocessableEntityNegotiatedContentResult(ApiController controller)
            : base(null, controller)
        {
            this.modelState = controller.ModelState;
            this.formatterCollection = controller.Configuration.Formatters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityNegotiatedContentResult"/> class.
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
        public UnprocessableEntityNegotiatedContentResult(
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

            // Set the status code
            response.StatusCode = (HttpStatusCode)422;

            // Get the errors as our viewmodel
            var fieldErrors = (from key in this.modelState.Keys
                               let errors = this.modelState[key]
                               from error in errors.Errors
                               select
                                   new ValidationFieldViewModel_ALL() { FieldName = key.ToDelimitedString('_'), Message = error.ErrorMessage })
                .ToList();

            // Construct the return content
            var content = new ValidationViewModel_ALL()
            {
                Description = "Validation Failed",
                Message = "A validation error occurred.",
                Errors = fieldErrors
            };

            // Add our content to the response
            response.Content = new ObjectContent(typeof(ValidationViewModel_ALL), content, this.formatterCollection.JsonFormatter);

            // Return
            return response;
        }
    }
}