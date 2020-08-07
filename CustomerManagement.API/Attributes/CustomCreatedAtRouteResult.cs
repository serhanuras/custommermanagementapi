using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;

namespace CustomerManagement.API.Attributes
{
    [DefaultStatusCode(DefaultStatusCode)]
    public class CustomCreatedAtRouteResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status201Created;
        
        
        public CustomCreatedAtRouteResult(
            long id,
            [ActionResultObjectValue] object value)
            : base(value)
        {
            Id = id;
        }
        

       
        /// <summary>
        /// Gets or sets the route data to use for generating the URL.
        /// </summary>
        public long Id { get; set; }

        /// <inheritdoc />
        public override void OnFormatting(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            base.OnFormatting(context);


            var url = context.HttpContext.Request.GetDisplayUrl() + "/" + Id.ToString();

            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidOperationException();
            }

            context.HttpContext.Response.Headers[HeaderNames.Location] = url;
        }
    }
}