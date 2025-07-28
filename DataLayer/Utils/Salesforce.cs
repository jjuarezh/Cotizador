using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;
using DataLayer.Sfdc.Enterprise;
using ModelLayer;


namespace BusinessLayer.Utils
{
    public abstract class Salesforce
    {

        #region Constructor
        public Salesforce()
        {
            var userName = ConfigurationManager.AppSettings["SFDC_username"];
            var password = ConfigurationManager.AppSettings["SFDC_password"];
            var token = ConfigurationManager.AppSettings["SFDC_token"];
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                Binding = new SforceService();

                if (IsConnected())
                {
                    Binding.Timeout = 600000; // Five minute timeout
                    Binding.Url = Global.EndPoint;
                    Binding.SessionHeaderValue = new SessionHeader { sessionId = Global.SessionId };
                    try
                    {
                        Binding.getUserInfo();
                    }
                    catch
                    {
                        Global.SessionId = null;
                        Global.EndPoint = null;
                    }
                }
                if (IsConnected()) return;

                var result = Binding.login(userName, password + token);


                Binding.Url = result.serverUrl;
                Binding.SessionHeaderValue = new SessionHeader { sessionId = result.sessionId };
                Global.SessionId = result.sessionId;
                Global.EndPoint = result.serverUrl;
                Global.NextLoginTime = DateTime.Now.AddMinutes(10);
            }
            catch (SoapException ex)
            {
                throw new Exception(ex.Detail.InnerText + "\n" + ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        public SforceService Binding { get; set; }

        public List<sObject> ExecuteQuery(string query)
        {
            try
            {
                var output = new List<sObject>();
                var qResult = Binding.query(query);
                if (qResult.size == 0) return output;
                var done = false;
                if (qResult.size <= 0) return null;
                while (!done)
                {
                    output.AddRange(qResult.records.ToList());
                    if (qResult.done) done = true;
                    else qResult = Binding.queryMore(qResult.queryLocator);
                }
                return output;
            }
            catch (SoapException ex)
            {
                throw new Exception(ex.Detail.InnerText);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsConnected()
        {
            bool blnResult;
            if (!string.IsNullOrEmpty(Global.SessionId) & (Global.SessionId != null))
                blnResult = DateTime.Now <= Global.NextLoginTime;
            else
                blnResult = false;

            return blnResult;
        }
    }

}
