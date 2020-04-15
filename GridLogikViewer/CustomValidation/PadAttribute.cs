using GridLogikViewer.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.CustomValidation
{
    public class PadAttribute : ValidationAttribute, IClientValidatable
    {

        List<PrmGlobal> lstGlobal = new List<PrmGlobal>();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
         

            if (value == null)
                return new ValidationResult("Value is required");
            else
                return ValidationResult.Success;


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
                GetGlobalValues();
            }
            string property = metadata.PropertyName;
            string errorMessage = "";

            PrmGlobal objPrmGlobalResult = new PrmGlobal();

            objPrmGlobalResult = (from r in lstGlobal
                                  where r.prmunit.ToUpper() == property.ToUpper() && r.prmidentifier.ToUpper() == "MAXLENGTH"
                                  select r).FirstOrDefault();

            if (objPrmGlobalResult != null)
            {
                PrmGlobal objPrmGlobalPadResult = new PrmGlobal();
                objPrmGlobalPadResult = (from r in lstGlobal
                                         where r.prmunit.ToUpper() == property.ToUpper() && r.prmidentifier.ToUpper() == "PADDED"
                                      select r).FirstOrDefault();

                if (objPrmGlobalPadResult != null)
                {
                    errorMessage = objPrmGlobalResult.rfu1;
                    ModelClientValidationRule requiredRule = new ModelClientValidationRule();
                    requiredRule.ErrorMessage = errorMessage;
                    requiredRule.ValidationType = "pad";
                    requiredRule.ValidationParameters.Add("maxlengthvalue", objPrmGlobalResult.prmvalue);
                    requiredRule.ValidationParameters.Add("ispaddingrequired", objPrmGlobalPadResult.prmvalue.ToUpper());
                    yield return requiredRule;
                }
                else
                {
                    yield return
                         new ModelClientValidationRule { ValidationType = "paddingnotrequired", ErrorMessage = "" };
                }
            }
            else
            {
                yield return
                     new ModelClientValidationRule { ValidationType = "paddingnotrequired", ErrorMessage = "" };
            }


        }

        private void GetGlobalValues()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("prmglobal/GetTablesIdentifiers/MDAS").Result;

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

        }

        #endregion
    }
}