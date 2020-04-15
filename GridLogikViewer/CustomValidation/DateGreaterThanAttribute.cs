using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using GridLogikViewer.Models;
using Newtonsoft.Json.Linq;
using System.Web.Configuration;

namespace GridLogikViewer.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DateGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        string otherPropertyName;
        string otherValue;
        List<PrmGlobal> lstGlobal = null;
        public DateGreaterThanAttribute(string otherPropertyName)
        {
            this.otherPropertyName = otherPropertyName;
        }

        /// <summary>
        /// Format the error message filling in the name of the property to validate and the reference one.
        /// </summary>
        /// <param name="name">The name of the property to validate</param>
        /// <returns>The formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            // In our case this will return: "Estimated end date must be greater than Start date"
            return string.Format(ErrorMessageString, name, otherPropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                // Using reflection we can get a reference to the other date property, in this example the project start date
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);
                // Let's check that otherProperty is of type DateTime as we expect it to be
                //if (otherPropertyInfo.PropertyType.Equals(new DateTime().GetType()))
                //{
                //    DateTime toValidate = (DateTime)value;
                otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null).ToString();
                DateTime referenceProperty = (DateTime)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                // if the end date is lower than the start date, than the validationResult will be set to false and return
                // a properly formatted error message
                //if (toValidate.CompareTo(referenceProperty) < 1)
                //{
                //    //string message = FormatErrorMessage(validationContext.DisplayName);
                //    //validationResult = new ValidationResult(message);
                //    validationResult = new ValidationResult(ErrorMessageString);
                //}
                validationResult = new ValidationResult("");
                //}
                //else
                //{
                // validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
                // }
            }
            catch (Exception ex)
            {
                // Do stuff, i.e. log the exception
                // Let it go through the upper levels, something bad happened
                throw ex;
            }

            return validationResult;
        }


        #region IClientValidatable Members

        /// <summary>
        ///  
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {

            if (lstGlobal == null || lstGlobal.Count == 0)
            {
                lstGlobal = GetGlobalValues();
            }
            string property = metadata.PropertyName;
            PrmGlobal objPrmGlobalResult = new PrmGlobal();
            objPrmGlobalResult = (from r in lstGlobal
                                  where r.prmunit.ToUpper() == property.ToUpper() && r.prmidentifier.ToUpper() == "MESSAGE"
                                  select r).FirstOrDefault();

            if (objPrmGlobalResult != null)
            {

                PrmGlobal objPrmDateGlobalResult = new PrmGlobal();
                objPrmDateGlobalResult = (from r in lstGlobal
                                          where r.prmmodule.ToUpper() == "GLOBAL" && r.prmunit.ToUpper() == "DATEFIELD" && r.prmidentifier.ToUpper() == "FORMAT"
                                          select r).FirstOrDefault();

                ModelClientValidationRule requiredRule = new ModelClientValidationRule();
                requiredRule.ErrorMessage = objPrmGlobalResult.rfu1;
                requiredRule.ValidationType = "dategreaterthan";
                requiredRule.ValidationParameters.Add("otherpropertyname", otherPropertyName);
                requiredRule.ValidationParameters.Add("dateformat", objPrmDateGlobalResult.prmvalue);
                yield return requiredRule;

            }
            else
            {
                yield return
                new ModelClientValidationRule { ValidationType = "dategreaterthannotrequired", ErrorMessage = "" };
            }
        }
        private List<PrmGlobal> GetGlobalValues()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("prmglobal/GetTablesIdentifiers/MDAS").Result;
            List<PrmGlobal> lstGlobal = new List<PrmGlobal>();
            if (response.IsSuccessStatusCode)
            {
                var objResponse = response.Content.ReadAsStringAsync().Result;

                dynamic objPrmGlobal = JValue.Parse(objResponse);
                foreach (dynamic prm in objPrmGlobal.Data.result)
                {
                    PrmGlobal obj = new PrmGlobal();
                    obj.prmidentifier = prm.prmidentifier.ToString();
                    obj.prmmodule = prm.prmmodule.ToString();
                    obj.prmrecid = Convert.ToInt16(prm.prmrecid.ToString());
                    obj.prmunit = (prm.prmunit.ToString().IndexOf('.') > 0 ? prm.prmunit.ToString().Substring(prm.prmunit.ToString().IndexOf('.'), (prm.prmunit.ToString().Length) - (prm.prmunit.ToString().IndexOf('.'))).ToString().Replace(".", "") : prm.prmunit.ToString());
                    obj.prmvalue = prm.prmvalue.ToString();
                    obj.rfu1 = prm.rfu1.ToString();
                    obj.rfu2 = prm.rfu2.ToString();
                    lstGlobal.Add(obj);
                }
            }
            return lstGlobal;
        }

        #endregion

    }
}