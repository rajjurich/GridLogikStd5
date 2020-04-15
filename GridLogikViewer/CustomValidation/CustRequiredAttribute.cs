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

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CustRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        string OtherProperty;

        public CustRequiredAttribute(string OtherProperty)
        {
            this.OtherProperty = OtherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            //var otherProperty = validationContext.ObjectInstance.GetType()
            //              .GetProperty(OtherProperty);

            //var othedrPropertyValue = otherProperty
            //    .GetVialue(validationContext.ObjectInstance, null);

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
            List<PrmGlobal> lstGlobal = GetGlobalValues();
            List<ModelClientValidationRule> lstRule = new List<ModelClientValidationRule>();
            if (lstGlobal != null || lstGlobal.Count != 0)
            {
                string property = metadata.PropertyName;
                lstRule.Add(RequiredAttr(property, lstGlobal));
                lstRule.Add(MinAttr(property, lstGlobal));
                lstRule.Add(MaxAttr(property, lstGlobal));
                lstRule.Add(RegexAttr(property, lstGlobal));
            }
            return lstRule;

        }
        #region Required Field
        private ModelClientValidationRule RequiredAttr(string property, List<PrmGlobal> lstGlobal)
        {
            PrmGlobal objPrmGlobalResult = new PrmGlobal();

            objPrmGlobalResult = (from r in lstGlobal
                                  where r.prmunit.ToUpper() == property.ToUpper() && r.prmvalue.ToUpper() == "Y" && r.prmidentifier.ToUpper() == "REQUIRED"
                                  select r).FirstOrDefault();

            if (objPrmGlobalResult != null)
            {
                ModelClientValidationRule requiredRule = new ModelClientValidationRule();
                requiredRule.ErrorMessage = objPrmGlobalResult.rfu1;
                requiredRule.ValidationType = "requiredcustom";
                requiredRule.ValidationParameters.Add("otherpropertyname", OtherProperty);
                return requiredRule;
            }
            else
            {
                return
                    new ModelClientValidationRule { ValidationType = "notrequired", ErrorMessage = "" };
            }
        }

        #endregion
        #region Min Attr
        private ModelClientValidationRule MinAttr(string property, List<PrmGlobal> lstGlobal)
        {

            PrmGlobal objPrmGlobalResult = new PrmGlobal();

            objPrmGlobalResult = (from r in lstGlobal
                                  where r.prmunit.ToUpper() == property.ToUpper() && r.prmidentifier.ToUpper() == "MINLENGTH"
                                  select r).FirstOrDefault();

            if (objPrmGlobalResult != null)
            {

                ModelClientValidationRule requiredRule = new ModelClientValidationRule();
                requiredRule.ErrorMessage = objPrmGlobalResult.rfu1;
                requiredRule.ValidationType = "custminlength";
                requiredRule.ValidationParameters.Add("minlengthvalue", objPrmGlobalResult.prmvalue);
                return requiredRule;
            }
            else
            {
                
                objPrmGlobalResult = (from r in lstGlobal
                                      where r.prmunit.ToUpper() == "TEXTFIELDS" && r.prmidentifier.ToUpper() == "MINLENGTH" && r.prmmodule.ToUpper() == "GLOBAL"
                                      select r).FirstOrDefault();
                if (objPrmGlobalResult != null)
                {
                    ModelClientValidationRule requiredRule = new ModelClientValidationRule();
                    requiredRule.ErrorMessage = "Min Length is " + objPrmGlobalResult.prmvalue;
                    requiredRule.ValidationType = "custminlength";
                    requiredRule.ValidationParameters.Add("minlengthvalue", objPrmGlobalResult.prmvalue);
                    return requiredRule;
                }
                else
                {
                     return
                         new ModelClientValidationRule { ValidationType = "minnotrequired", ErrorMessage = "" };
                }
            }
        }
        #endregion
        #region Max Attr
        private ModelClientValidationRule MaxAttr(string property, List<PrmGlobal> lstGlobal)
        {
            PrmGlobal objPrmGlobalResult = new PrmGlobal();

            objPrmGlobalResult = (from r in lstGlobal
                                  where r.prmunit.ToUpper() == property.ToUpper() && r.prmidentifier.ToUpper() == "MAXLENGTH"
                                  select r).FirstOrDefault();

            if (objPrmGlobalResult != null)
            {

                ModelClientValidationRule requiredRule = new ModelClientValidationRule();
                requiredRule.ErrorMessage = objPrmGlobalResult.rfu1;
                requiredRule.ValidationType = "custmaxlength";
                requiredRule.ValidationParameters.Add("maxlengthvalue", objPrmGlobalResult.prmvalue.ToUpper());
                return requiredRule;
            }
            else
            {
                objPrmGlobalResult = (from r in lstGlobal
                                      where r.prmunit.ToUpper() == "TEXTFIELDS" && r.prmidentifier.ToUpper() == "MAXLENGTH" && r.prmmodule.ToUpper() == "GLOBAL"
                                      select r).FirstOrDefault();
                if (objPrmGlobalResult != null)
                {
                    ModelClientValidationRule requiredRule = new ModelClientValidationRule();
                    requiredRule.ErrorMessage = "Max Length is " + objPrmGlobalResult.prmvalue;
                    requiredRule.ValidationType = "custmaxlength";
                    requiredRule.ValidationParameters.Add("maxlengthvalue", objPrmGlobalResult.prmvalue);
                    return requiredRule;
                }
                else
                {
                     return
                         new ModelClientValidationRule { ValidationType = "minnotrequired", ErrorMessage = "" };
                }
            }
        }
        #endregion
        #region Regex Attr
        private ModelClientValidationRule RegexAttr(string property, List<PrmGlobal> lstGlobal)
        {
            PrmGlobal objPrmGlobalResult = new PrmGlobal();
            objPrmGlobalResult = (from r in lstGlobal
                                  where r.prmunit.ToUpper() == property.ToUpper() && r.prmidentifier.ToUpper() == "DATATYPE"
                                  select r).FirstOrDefault();

            if (objPrmGlobalResult != null)
            {
                if (objPrmGlobalResult.prmvalue.Trim().Length > 1)
                {
                    ModelClientValidationRule requiredRule = new ModelClientValidationRule();
                    requiredRule.ErrorMessage = objPrmGlobalResult.rfu1;
                    requiredRule.ValidationType = "regexp";
                    requiredRule.ValidationParameters.Add("regextype", objPrmGlobalResult.prmvalue);
                    return requiredRule;
                }
                else
                {
                    return
                    new ModelClientValidationRule { ValidationType = "regexnotrequired", ErrorMessage = "" };
                }
            }
            else
            {
                return
                    new ModelClientValidationRule { ValidationType = "regexnotrequired", ErrorMessage = "" };
            }
        }
        #endregion
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