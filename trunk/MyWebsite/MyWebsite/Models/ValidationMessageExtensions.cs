using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyWebsite.Models
{
    public static class ValidationMessageExtensions
    {
        public static IHtmlString MyValidationMessageFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> ex
        )
        {
            var htmlAttributes = new RouteValueDictionary();
            string validationMessage = null;
            var expression = ExpressionHelper.GetExpressionText(ex);
            var modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
            var formContext = htmlHelper.ViewContext.ClientValidationEnabled ? htmlHelper.ViewContext.FormContext : null;
            if (!htmlHelper.ViewData.ModelState.ContainsKey(modelName) && formContext == null)
            {
                return null;
            }

            var modelState = htmlHelper.ViewData.ModelState[modelName];
            var modelErrors = (modelState == null) ? null : modelState.Errors;
            var modelError = (((modelErrors == null) || (modelErrors.Count == 0))
                ? null
                : modelErrors.FirstOrDefault(m => !String.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrors[0]);

            if (modelError == null && formContext == null)
            {
                return null;
            }

            var builder = new TagBuilder("span");
            builder.MergeAttributes(htmlAttributes);
            builder.AddCssClass((modelError != null) ? HtmlHelper.ValidationMessageCssClassName : HtmlHelper.ValidationMessageValidCssClassName);

            if (!String.IsNullOrEmpty(validationMessage))
            {
                builder.InnerHtml = validationMessage;
            }
            else if (modelError != null)
            {
                builder.InnerHtml = GetUserErrorMessageOrDefault(htmlHelper.ViewContext.HttpContext, modelError, modelState);
            }

            if (formContext != null)
            {
                bool replaceValidationMessageContents = String.IsNullOrEmpty(validationMessage);
                builder.MergeAttribute("data-valmsg-for", modelName);
                builder.MergeAttribute("data-valmsg-replace", replaceValidationMessageContents.ToString().ToLowerInvariant());
            }

            return new HtmlString(builder.ToString(TagRenderMode.Normal));
        }

        private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
        {
            if (!String.IsNullOrEmpty(error.ErrorMessage))
            {
                return error.ErrorMessage;
            }
            if (modelState == null)
            {
                return null;
            }

            var attemptedValue = (modelState.Value != null) ? modelState.Value.AttemptedValue : null;
            return string.Format(CultureInfo.CurrentCulture, "Value '{0}' not valid for property", attemptedValue);
        }
    }
}