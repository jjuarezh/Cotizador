using System.Collections.Generic;
using System.Linq;
using BusinessLayer;
using BusinessLayer.Utils;
using DataLayer.Sfdc.Enterprise;
using ModelLayer;

namespace DataLayer.Functions
{
    internal class Accounts
    {
        public static List<SfAccountModel> GetAccount(string infoUserSf)
        {
            #region Parameters

            var accountId = string.Empty;
            var schoolName = string.Empty;
            var latitud = string.Empty;
            var longitude = string.Empty;
            var contactId = string.Empty;
            var contactName = string.Empty;
            var activityDate = string.Empty;

            #endregion

            Salesforce sfdc = new SfPopulation();

            var ListAccount = new List<SfAccountModel>();
            var ListPopulation = new List<PopulationModel>();

            var querySchool = string.Format(@"SELECT AccountId
                                                        , 	Account.Name                    Account
                                                        ,	max(Account.BillingLatitude)    Latitud
                                                        ,	max(Account.BillingLongitude)   Longitude
                                                        ,	WhoId                           ContactId
                                                        , 	Who.Name                        Contact
                                                        ,   min(ActivityDate__c)            ActivityDate
                                                    FROM    Event 
                                                    WHERE	EventSubType = 'Event' 
	                                                        AND ActivityDate = THIS_WEEK   
	                                                        AND owner.UserName = '{0}' 
	                                                        AND AccountId != null
                                                    GROUP BY    AccountId
                                                        , 	Account.Name
                                                        ,	WhoId
                                                        , 	Who.Name ", infoUserSf);

            var rSchool = sfdc.ExecuteQuery(querySchool).Cast<AggregateResult>();

            foreach (var rs in rSchool)
            {
                foreach (var xmlElem in rs.Any)
                    switch (xmlElem.LocalName)
                    {
                        case "AccountId":
                            accountId = xmlElem.InnerText;
                            ListPopulation = Populations.GetPopulation(accountId);
                            break;
                        case "Account":
                            schoolName = xmlElem.InnerText;
                            break;
                        case "Latitud":
                            latitud = xmlElem.InnerText;
                            break;
                        case "Longitude":
                            longitude = xmlElem.InnerText;
                            break;
                        case "ContactId":
                            contactId = xmlElem.InnerText;
                            break;
                        case "Contact":
                            contactName = xmlElem.InnerText;
                            break;
                        case "ActivityDate":
                            activityDate = xmlElem.InnerText;
                            break;
                    }


                ListAccount.Add(new SfAccountModel
                {
                    AccountId = accountId, SchoolName = activityDate + ", " + schoolName, IdContact = contactId,
                    ContactName = contactName,
                    Location = new SfAccountModel.ubication {Latitude = latitud, Longitude = longitude},
                    Population = ListPopulation
                });
            }

            return ListAccount.OrderBy(x => x.SchoolName).ToList();
        }
    }
}