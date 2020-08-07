using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CustomerManagement.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LowerCaseRouteAttribute : Attribute, IRouteTemplateProvider
    {
        private int? _order;

        public LowerCaseRouteAttribute([CallerFilePath] string callerFilePath = null)
        {
            var pattern = @"[A-za-z0-9]*.cs";

            Match match = Regex.Match(callerFilePath, pattern);
            if (match.Success)
            {
                Template = $"api/{match.Value.ToLower().Replace("controller.cs", "")}s";
            }
        }

       
        public string Template { get; }

       
        public int Order
        {
            get { return _order ?? 0; }
            set { _order = value; }
        }

        /// <inheritdoc />
        int? IRouteTemplateProvider.Order => _order;

        /// <inheritdoc />
        public string Name { get; set; }
        
        
       
    }
}