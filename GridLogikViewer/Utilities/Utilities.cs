using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Utilities
{
    public class Utilities
    {
        public string ErrorMessages(string errorcode, string filePath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(filePath);
                DataTable dt = ds.Tables[0];
                DataRow[] dr = dt.Select("Message_Id = " + errorcode);
                if (dr.Length != 0)
                {
                    return dr[0]["Message_Text"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }

        }

        public bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}