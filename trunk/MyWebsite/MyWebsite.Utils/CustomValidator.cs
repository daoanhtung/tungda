using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace MyWebsite.CustomControl
{
    public class RequiredIfAttribute : RequiredAttribute, IClientValidatable
    {
        private String PropertyName { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(String propertyName, Object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var instance = context.ObjectInstance;
            var type = instance.GetType();
            var propertyValue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (propertyValue != null && propertyValue.ToString() == DesiredValue.ToString())
            {
                var result = base.IsValid(value, context);
                return result;
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
                                                                               ControllerContext context)
        {
            var errorMessage = ErrorMessageString;
            var requireIfRule = new ModelClientValidationRule
                                    {ErrorMessage = errorMessage, ValidationType = "requiredif"};
            requireIfRule.ValidationParameters.Add("propertyname", PropertyName);
            requireIfRule.ValidationParameters.Add("desiredvalue", DesiredValue);
            yield return requireIfRule;
        }
    }
}