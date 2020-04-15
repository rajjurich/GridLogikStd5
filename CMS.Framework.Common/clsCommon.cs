using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CMS.Framework.Common
{
    public static class clsCommon
    {
        #region GetPropertyByName
        public static DataTable ToDataTable(IEnumerable<dynamic> items)
        {
            var data = items.ToArray();
            if (data.Count() == 0) return null;

            var dt = new DataTable();
            foreach (var key in ((IDictionary<string, object>)data[0]).Keys)
            {
                dt.Columns.Add(key);
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }
        private static object getProperty<T>(T e, string propName)
        {
            var propInfo = typeof(T).GetProperty(propName,
        BindingFlags.SetProperty |
        BindingFlags.IgnoreCase |
        BindingFlags.Public |
        BindingFlags.Instance);
            //  return propInfo.GetValue(e);
            if (propInfo != null)
            {
                return propInfo.GetValue(e);
            }
            else
            {

                return null;
            }
        }
        public static dynamic getProperties<T>(string[] props, T e)
        {
            var ret = new ExpandoObject() as IDictionary<string, Object>; ;

            foreach (var p in props)
            {
                ret.Add(p, getProperty(e, p));
            }
            return ret;
        }

        #endregion
        public static string ReadConfigValue(string pi_strKey)
        {
            if (pi_strKey == null || pi_strKey.Equals(string.Empty))
                throw new System.InvalidOperationException("Key string cannot be empty");
            System.Configuration.AppSettingsReader appRdr = new System.Configuration.AppSettingsReader();
            object obj;
            obj = appRdr.GetValue(pi_strKey, Type.GetType("System.String"));
            if (obj == null)
                return string.Empty;
            return obj.ToString();
        }
        public static string ApplicationName()
        {
            return ReadConfigValue("ApplicationName");
        }
        public static string ApplicationApiKey()
        {
            return ReadConfigValue("ApplicationApiKey");
        }
        public static string BaseAddress()
        {
            return ReadConfigValue("BaseAddress");
        }
        public static string ServiceName()
        {
            return ReadConfigValue("ServiceName");
        }

        public const string strDelimeter = "||";
        public static string EncryptString(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            string key = "SMTCMSSP5";
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string DecryptString(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            string key = "SMTCMSSP5";
            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static string EncryptPass(this String str)
        {
            return EncryptString(str, true);
        }
        public static string DecryptPass(this String str)
        {
            return DecryptString(str, true);
        }

        public static string GetMonth(this int Month)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
        }

        public static string GetShortMonthName(this int Month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Month);
        }
        public static decimal GetAdditionDevRs_New(decimal frequency, decimal devmw, decimal sg, decimal devrate)
        {
            decimal percentage = 0;
         
            decimal addidevrs = 0;
            //kwgeneration = netexbus - sg;
            frequency = decimal.Round(frequency, 2);
            decimal devmw_original = 0;
            devmw_original = devmw;
            if (devmw < 0)
            {
                devmw = Math.Abs(devmw);

                percentage = (Math.Abs(devmw) / sg) * 100;

                //if ((sg * 0.2M) >= 250)
                //{
                if (frequency < Convert.ToDecimal(49.70))
                {
                    decimal addimw = 0;
                    //if (percentage <= 12)
                    //{
                    //addidevrs = (Math.Abs(devmw) * Convert.ToDecimal(8.24) * 250);
                    //}

                    if (devmw > 150)
                    {
                        addidevrs = 0;

                        if ((sg * 0.2M) >= 250)
                        {
                            if (devmw >= 150 && devmw >= 200)
                            {
                                addidevrs = addidevrs + Convert.ToDecimal((50 * 250 * 8.24));
                            }
                            else
                            {
                                addimw = Math.Abs(150 - devmw);
                                addidevrs = addidevrs + Convert.ToDecimal((addimw * Convert.ToDecimal(250) * Convert.ToDecimal(8.24)));
                            }

                            if (devmw > 200 && devmw >= 250)
                            {
                                addidevrs = addidevrs + Convert.ToDecimal((50 * 250 * 8.24));
                            }
                            else
                            {
                                addimw = Math.Abs(200 - devmw);
                                addidevrs = addidevrs + Convert.ToDecimal((addimw * Convert.ToDecimal(250) * Convert.ToDecimal(8.24)));
                            }

                            if (devmw > 250)
                            {
                                devmw = Math.Abs(devmw) - 250;
                                devmw = Math.Abs(devmw);
                                addidevrs = addidevrs + Convert.ToDecimal(((Math.Abs(devmw)) * Convert.ToDecimal(250) * Convert.ToDecimal(8.24)));
                            }
                            else
                            {
                                //addidevrs = addidevrs + Convert.ToDecimal(((Convert.ToDecimal(250) - devmw) * Convert.ToDecimal(250) * Convert.ToDecimal(8.24)));
                            }
                        }
                        else
                        {
                            decimal sg1 = 0;
                            decimal sg2 = 0;
                            decimal sg3 = 0;
                            decimal devsg1 = 0;
                            decimal devsg2 = 0;
                            decimal devsg3 = 0;
                            decimal devsg4 = 0;
                            decimal devsgrate1 = 0;
                            decimal devsgrate2 = 0;
                            decimal devsgrate3 = 0;
                            decimal devsgrate4 = 0;

                            sg1 = sg * 0.12M;
                            sg2 = sg * 0.15M;
                            sg3 = sg * 0.20M;

                            devsg1 = sg1;
                            devsg2 = sg2 - sg1;
                            devsg3 = sg3 - sg2;

                            devsg4 = Math.Abs(devsg1 + devsg2 + devsg3 + devmw_original);

                            devsgrate1 = 0;
                            devsgrate2 = 8.24M;
                            devsgrate3 = 8.24M;
                            devsgrate4 = 8.24M;

                            addidevrs = (devsg2 * devsgrate2) + (devsg3 * devsgrate3) + (devsg4 * devsgrate4);
                            addidevrs = addidevrs * 250;

                        }
                    }

                }
                else if (frequency >= Convert.ToDecimal(49.70) && frequency <= Convert.ToDecimal(50.05))
                {
                    decimal addimw = 0;

                    if (devmw > 150)
                    {
                        if ((sg * 0.2M) >= 250)
                        {
                            if (devmw >= 150 && devmw >= 200)
                            {
                                addidevrs = addidevrs + Convert.ToDecimal((50 * 250 * (devrate + (devrate * Convert.ToDecimal(0.2)))));
                            }
                            else
                            {
                                addimw = Math.Abs(150 - devmw);

                                addidevrs = addidevrs + Convert.ToDecimal((addimw * Convert.ToDecimal(250) * (devrate + (devrate * Convert.ToDecimal(0.2)))));
                            }

                            if (devmw > 200 && devmw >= 250)
                            {
                                addidevrs = addidevrs + Convert.ToDecimal((50 * 250 * (devrate + (devrate * Convert.ToDecimal(0.4)))));
                            }
                            else
                            {
                                addimw = Math.Abs(200 - devmw);
                                //addidevrs = addidevrs + Convert.ToDecimal((addimw * Convert.ToDecimal(250) * Convert.ToDecimal(8.24)));
                                addidevrs = addidevrs + Convert.ToDecimal((addimw * 250 * (devrate + (devrate * Convert.ToDecimal(0.4)))));

                                //addidevrs = addidevrs + Convert.ToDecimal(((Math.Abs(devmw) - Convert.ToDecimal(200)) * Convert.ToDecimal(250) * Convert.ToDecimal(8.24)));
                            }

                            if (devmw > 250)
                            {
                                devmw = devmw - 250;
                                devmw = Math.Abs(devmw);

                                addidevrs = addidevrs + Convert.ToDecimal((Math.Abs(devmw) * Convert.ToDecimal(250) * ((devrate * 2))));
                                // addidevrs = addidevrs + Convert.ToDecimal((50 * 250 * (devrate * (devrate * 2))));
                            }
                        }
                        else
                        {
                            decimal sg1 = 0;
                            decimal sg2 = 0;
                            decimal sg3 = 0;
                            decimal devsg1 = 0;
                            decimal devsg2 = 0;
                            decimal devsg3 = 0;
                            decimal devsg4 = 0;
                            decimal devsgrate1 = 0;
                            decimal devsgrate2 = 0;
                            decimal devsgrate3 = 0;
                            decimal devsgrate4 = 0;

                            sg1 = sg * 0.12M;
                            sg2 = sg * 0.15M;
                            sg3 = sg * 0.20M;

                            devsg1 = sg1;
                            devsg2 = sg2 - sg1;
                            devsg3 = sg3 - sg2;

                            devsg4 = Math.Abs(devsg1 + devsg2 + devsg3 + devmw_original);

                            //devsgrate1 = 0;
                            devsgrate2 = devrate + (devrate * 0.2M);
                            devsgrate3 = devrate + (devrate * 0.4M);
                            devsgrate4 = devrate * 2;

                            addidevrs = (devsg2 * devsgrate2) + (devsg3 * devsgrate3) + (devsg4 * devsgrate4);
                            addidevrs = addidevrs * 250;
                        }
                    }
                }

                else if (frequency > Convert.ToDecimal(50.05) && frequency <= Convert.ToDecimal(50.1))
                {
                    addidevrs = 0;
                }
                else if (frequency > Convert.ToDecimal(50.1))
                {
                    addidevrs = 0;
                }
                //}
                //else
                //{

                //}
            }
            else if (devmw > 0)
            {
                //percentage = (actualgenkw / sg) * 100;

                if (frequency <= Convert.ToDecimal(49.70))
                {
                    //if (percentage <= 12)
                    //{
                    //    addidevrs = (Math.Abs(devmw) * Convert.ToDecimal(8.24) * 250);
                    //}
                    //else
                    //{
                    //    addidevrs = 0;
                    //}
                    addidevrs = 0;

                }
                else if (frequency > Convert.ToDecimal(49.70) && frequency <= Convert.ToDecimal(50.05))
                {
                    addidevrs = 0;

                    //uirate objuirate = new uirate();
                    //var uirate = (from ui in db.uirates
                    //              where ui.frequency >= frequency && ui.frequency <= frequency
                    //              select ui).FirstOrDefault();

                    //objuirate = uirate;

                    //if (percentage <= 12)
                    //{
                    //    addidevrs = (Math.Abs(devmw) * (Convert.ToDecimal(objuirate.positive) / 100) * 250);
                    //}
                    //else
                    //{
                    //    addidevrs = 0;
                    //}
                }
                else if (frequency > Convert.ToDecimal(50.05) && frequency <= Convert.ToDecimal(50.1))
                {
                    addidevrs = 0;
                }
                else if (frequency > Convert.ToDecimal(50.1))
                {
                    //addidevrs = (Math.Abs(devmw) * Convert.ToDecimal(1.78) * 250);

                    decimal addimw = 0;

                    if (devmw > 150)
                    {
                        if ((sg * 0.2M) >= 250)
                        {
                            if (devmw >= 150 && devmw >= 200)
                            {
                                addidevrs = addidevrs + Convert.ToDecimal((50 * 250 * 1.78));
                            }
                            else
                            {
                                addimw = Math.Abs(150 - devmw);
                                addidevrs = addidevrs + Convert.ToDecimal((addimw * Convert.ToDecimal(250) * Convert.ToDecimal(1.78)));

                            }

                            if (devmw > 200 && devmw >= 250)
                            {
                                addidevrs = addidevrs + Convert.ToDecimal((50 * 250 * 1.78));
                            }
                            else
                            {
                                //addidevrs = addidevrs + Convert.ToDecimal(((Math.Abs(devmw) - Convert.ToDecimal(200)) * Convert.ToDecimal(250) * Convert.ToDecimal(1.78)));

                                addimw = Math.Abs(200 - devmw);
                                addidevrs = addidevrs + Convert.ToDecimal((addimw * Convert.ToDecimal(250) * Convert.ToDecimal(1.78)));
                            }

                            if (devmw > 250)
                            {
                                devmw = devmw - 250;
                                devmw = Math.Abs(devmw);

                                //addidevrs = addidevrs + Convert.ToDecimal(((Math.Abs(devmw) - Convert.ToDecimal(250)) * Convert.ToDecimal(250) * Convert.ToDecimal(1.78)));
                                addidevrs = addidevrs + Convert.ToDecimal(devmw * Convert.ToDecimal(250) * Convert.ToDecimal(1.78));
                            }
                        }
                        else
                        {
                            decimal sg1 = 0;
                            decimal sg2 = 0;
                            decimal sg3 = 0;
                            decimal devsg1 = 0;
                            decimal devsg2 = 0;
                            decimal devsg3 = 0;
                            decimal devsg4 = 0;
                            decimal devsgrate1 = 0;
                            decimal devsgrate2 = 0;
                            decimal devsgrate3 = 0;
                            decimal devsgrate4 = 0;

                            sg1 = sg * 0.12M;
                            sg2 = sg * 0.15M;
                            sg3 = sg * 0.20M;

                            devsg1 = sg1;
                            devsg2 = sg2 - sg1;
                            devsg3 = sg3 - sg2;

                            devsg4 = Math.Abs(devsg1 + devsg2 + devsg3 + devmw_original);

                            devsgrate1 = 0;
                            devsgrate2 = 1.78M;
                            devsgrate3 = 1.78M;
                            devsgrate4 = 1.78M;

                            addidevrs = (devsg2 * devsgrate2) + (devsg3 * devsgrate3) + (devsg4 * devsgrate4);
                            addidevrs = addidevrs * 250;
                        }
                    }
                }
            }

            return addidevrs;

        }

        public static decimal GetAdditionDevRs_New1(decimal frequency, decimal devmw, decimal sg, decimal devrate, decimal netexbusmw)
        {
            decimal puimw1 = 0, puimw2 = 0, puimw3 = 0, addui1 = 0;
            decimal NUIMW11 = 0, NUIMW12 = 0, NUIMW13 = 0, NUIMW14 = 0;

            decimal addidevrs = 0;
            decimal sgD12P = 0, sgD12N = 0;

            try
            {
                if (sg > 400)
                {
                    if (sg * 0.12M > 150)
                    {

                        sgD12P = sg + 150;
                        sgD12N = sg - 150;
                    }
                }
                else
                {
                    sgD12P = sg * 1.12M;
                    sgD12N = sg * 0.88M;
                }


                if (sg <= 400)
                {
                    sgD12P = sg + 48;
                    sgD12N = sg - 48;

                }








                if (devmw >= 0 && sg > 400)
                {
                    if (devmw > sg * 0.12M)
                    {
                        puimw1 = sg * 0.12M;
                    }
                    else
                    { puimw1 = netexbusmw - sg; }
                }

                if (devmw >= 0 && sg <= 400)
                {
                    if (devmw > 48)
                    { puimw1 = 48; }
                    else
                    { puimw1 = netexbusmw - sg; }
                }

                //if (sg > 400)
                //{
                if (devmw < 0 && sg > 400)
                {

                    if (Math.Abs(devmw) > sg * 0.12M && 150 >= sg * 0.12M)
                    {
                        NUIMW11 = sg * 0.12M; NUIMW12 = sg * 0.03M; NUIMW13 = sg * 0.05M; NUIMW14 = sg - netexbusmw - (sg * 0.2M);
                    }
                }


                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) > 250 && 150 < sg * 0.12M)//UIMW1
                    {
                        NUIMW11 = 150; NUIMW12 = 50; NUIMW13 = 50; NUIMW14 = sg - netexbusmw - 250;

                    }
                }

                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= sg * 0.2M && Math.Abs(devmw) > sg * 0.15M && 150 >= sg * 0.12M)
                    {

                        NUIMW11 = sg * 0.12M; NUIMW12 = sg * 0.03M; NUIMW13 = sg - netexbusmw - (sg * 0.15M); NUIMW14 = 0;

                    }

                }








                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= 250 && Math.Abs(devmw) > 200 && 150 < sg * 0.12M)
                    {
                        NUIMW11 = 150; NUIMW12 = 50; NUIMW13 = sg - netexbusmw - 200; NUIMW14 = 0;

                    }
                }








                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= sg * 0.15M && Math.Abs(devmw) > sg * 0.12M && 150 >= sg * 0.12M)
                    {
                        NUIMW11 = sg * 0.12M; NUIMW12 = sg - netexbusmw - (sg * 0.12M); NUIMW13 = 0; NUIMW14 = 0;
                    }
                }




                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= 200 && Math.Abs(devmw) > 150 && 150 < sg * 0.12M)
                    {
                        NUIMW11 = 150; NUIMW12 = sg - netexbusmw - 150; NUIMW13 = 0; NUIMW14 = 0;

                    }
                }

                if (devmw < 0 && sg > 400)
                {

                    if (Math.Abs(devmw) <= sg * 0.12M && 150 >= sg * 0.12M)
                    {
                        NUIMW11 = sg - netexbusmw; NUIMW12 = 0; NUIMW13 = 0; NUIMW14 = 0;
                    }
                }



                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= 150 && 150 < sg * 0.12M)
                    {
                        NUIMW11 = sg - netexbusmw; NUIMW12 = 0; NUIMW13 = 0; NUIMW14 = 0;

                    }
                }



                if (devmw < 0 && (sg <= 400))//|| netexbusmw <= 0
                {
                    if (Math.Abs(devmw) > 80)
                    {
                        NUIMW11 = 48; NUIMW12 = 12; NUIMW13 = 20; NUIMW14 = sg - netexbusmw - 80;

                    }
                }



                if (devmw < 0 && (sg <= 400))// || netexbusmw <= 0
                {
                    if (Math.Abs(devmw) <= 80 && Math.Abs(devmw) > 60)
                    {
                        NUIMW11 = 48; NUIMW12 = 12; NUIMW13 = sg - netexbusmw - 60; NUIMW14 = 0;
                    }


                }


                if (devmw < 0 && (sg <= 400))//|| netexbusmw <= 0
                {
                    if (Math.Abs(devmw) <= 60 && Math.Abs(devmw) > 48)
                    {
                        NUIMW11 = 48; NUIMW12 = sg - netexbusmw - 48; NUIMW13 = 0; NUIMW14 = 0;

                    }
                }


                if (devmw < 0 && (sg <= 400))//|| netexbusmw <= 0
                {
                    if (Math.Abs(devmw) <= 48)
                    {
                        NUIMW11 = sg - netexbusmw; NUIMW12 = 0; NUIMW13 = 0; NUIMW14 = 0;

                    }
                }



                if (devmw < 0 && frequency < 50.05M && frequency >= 49.7M)
                {
                    addui1 = ((NUIMW12 * 0.2M) + (NUIMW13 * 0.4M) + (NUIMW14 * 1)) * 900 * devrate / 3.6M;

                }





                if (devmw < 0 && frequency < 49.7M)
                {
                    addui1 = ((NUIMW11 * 1) + (NUIMW12 * 1) + (NUIMW13 * 1) + (NUIMW14 * 1)) * 900 * devrate / 3.6M;

                }
              

                #region new dsm for 50.05 penalty
                if ( frequency >= 50.05M)
                {
                    
                    addui1 = (((devmw / 4000) * 1000000) * (106.66M / 100));

                }
                #endregion
                // }


            }
            catch (Exception es)
            {

            }
            return addui1;


        }

        public static decimal GetAdditionDevRs_New_dsm(decimal frequency, decimal devmw, decimal sg, decimal devrate, decimal netexbusmw, decimal erc )
        {
            decimal puimw1 = 0, puimw2 = 0, puimw3 = 0, addui1 = 0;
            decimal NUIMW11 = 0, NUIMW12 = 0, NUIMW13 = 0, NUIMW14 = 0;

            decimal addidevrs = 0;
            decimal sgD12P = 0, sgD12N = 0;

            try
            {
                if (sg > 400)
                {
                    if (sg * 0.12M > 150)
                    {

                        sgD12P = sg + 150;
                        sgD12N = sg - 150;
                    }
                }
                else
                {
                    sgD12P = sg * 1.12M;
                    sgD12N = sg * 0.88M;
                }


                if (sg <= 400)
                {
                    sgD12P = sg + 48;
                    sgD12N = sg - 48;

                }               


                if (devmw >= 0 && sg > 400)
                {
                    if (devmw > sg * 0.12M)
                    {
                        puimw1 = sg * 0.12M;
                    }
                    else
                    { puimw1 = netexbusmw - sg; }
                }

                if (devmw >= 0 && sg <= 400)
                {
                    if (devmw > 48)
                    { puimw1 = 48; }
                    else
                    { puimw1 = netexbusmw - sg; }
                }

                //if (sg > 400)
                //{
                if (devmw < 0 && sg > 400)
                {

                    if (Math.Abs(devmw) > sg * 0.12M && 150 >= sg * 0.12M)
                    {
                        NUIMW11 = sg * 0.12M; NUIMW12 = sg * 0.03M; NUIMW13 = sg * 0.05M; NUIMW14 = sg - netexbusmw - (sg * 0.2M);
                    }
                }


                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) > 250 && 150 < sg * 0.12M)//UIMW1
                    {
                        NUIMW11 = 150; NUIMW12 = 50; NUIMW13 = 50; NUIMW14 = sg - netexbusmw - 250;

                    }
                }

                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= sg * 0.2M && Math.Abs(devmw) > sg * 0.15M && 150 >= sg * 0.12M)
                    {

                        NUIMW11 = sg * 0.12M; NUIMW12 = sg * 0.03M; NUIMW13 = sg - netexbusmw - (sg * 0.15M); NUIMW14 = 0;

                    }

                }


                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= 250 && Math.Abs(devmw) > 200 && 150 < sg * 0.12M)
                    {
                        NUIMW11 = 150; NUIMW12 = 50; NUIMW13 = sg - netexbusmw - 200; NUIMW14 = 0;

                    }
                }



                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= sg * 0.15M && Math.Abs(devmw) > sg * 0.12M && 150 >= sg * 0.12M)
                    {
                        NUIMW11 = sg * 0.12M; NUIMW12 = sg - netexbusmw - (sg * 0.12M); NUIMW13 = 0; NUIMW14 = 0;
                    }
                }




                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= 200 && Math.Abs(devmw) > 150 && 150 < sg * 0.12M)
                    {
                        NUIMW11 = 150; NUIMW12 = sg - netexbusmw - 150; NUIMW13 = 0; NUIMW14 = 0;

                    }
                }

                if (devmw < 0 && sg > 400)
                {

                    if (Math.Abs(devmw) <= sg * 0.12M && 150 >= sg * 0.12M)
                    {
                        NUIMW11 = sg - netexbusmw; NUIMW12 = 0; NUIMW13 = 0; NUIMW14 = 0;
                    }
                }



                if (devmw < 0 && sg > 400)
                {
                    if (Math.Abs(devmw) <= 150 && 150 < sg * 0.12M)
                    {
                        NUIMW11 = sg - netexbusmw; NUIMW12 = 0; NUIMW13 = 0; NUIMW14 = 0;

                    }
                }



                if (devmw < 0 && (sg <= 400))//|| netexbusmw <= 0
                {
                    if (Math.Abs(devmw) > 80)
                    {
                        NUIMW11 = 48; NUIMW12 = 12; NUIMW13 = 20; NUIMW14 = sg - netexbusmw - 80;

                    }
                }



                if (devmw < 0 && (sg <= 400))// || netexbusmw <= 0
                {
                    if (Math.Abs(devmw) <= 80 && Math.Abs(devmw) > 60)
                    {
                        NUIMW11 = 48; NUIMW12 = 12; NUIMW13 = sg - netexbusmw - 60; NUIMW14 = 0;
                    }


                }


                if (devmw < 0 && (sg <= 400))//|| netexbusmw <= 0
                {
                    if (Math.Abs(devmw) <= 60 && Math.Abs(devmw) > 48)
                    {
                        NUIMW11 = 48; NUIMW12 = sg - netexbusmw - 48; NUIMW13 = 0; NUIMW14 = 0;

                    }
                }


                if (devmw < 0 && (sg <= 400))//|| netexbusmw <= 0
                {
                    if (Math.Abs(devmw) <= 48)
                    {
                        NUIMW11 = sg - netexbusmw; NUIMW12 = 0; NUIMW13 = 0; NUIMW14 = 0;

                    }
                }
                

                if (devmw < 0 && frequency < 50.10M && frequency >= 49.85M)
                {
                    addui1 = ((NUIMW12 * 0.2M) + (NUIMW13 * 0.4M) + (NUIMW14 * 1)) * 900 * devrate / 3.6M * -1;

                }                


                if (devmw < 0 && frequency < 49.85M)
                {
                    addui1 = ((NUIMW11 * 1) + (NUIMW12 * 1) + (NUIMW13 * 1) + (NUIMW14 * 1)) * 900 * devrate / 3.6M * -1;

                }


                #region new dsm for 50.05 penalty
                if (frequency >= 50.10M)  /// DSM 1 Frequency was 50.05M and DSM 2 Frequency is 50.10M
                {
                    if (devmw >0)
                    // addui1 = (((devmw / 4000) * 1000000) * (106.66M / 100));
                        addui1 = -1 * (((Math.Abs(devmw) / 4000) * 1000000) * erc);
                    else
                        addui1 = (((Math.Abs(devmw) / 4000) * 1000000) * devrate);
                }
                #endregion
                // }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return addui1;


        }
    }
}