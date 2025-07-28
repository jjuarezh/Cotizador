using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Utils;
using DataLayer.Sfdc.Enterprise;
using ModelLayer;

namespace DataLayer.Functions
{
    internal class Populations : Salesforce
    {
        public static List<PopulationModel> GetPopulation(string idAccount)
        {
            Salesforce sfdc = new SfPopulation();

            var populationsList = new List<PopulationModel>();

            var queryPopulation = string.Format(@"SELECT Ciclo_escolar__r.name
                                            , Nivel__c
                                            , Maternal__C
                                            , Prefirst__c
                                            , Grado_1__c
                                            , Grado_2__c
                                            , Grado_3__c
                                            , Grado_4__c
                                            , Grado_5__c
                                            , Grado_6__c 
                                    FROM    Population__C 
                                    WHERE   Ciclo_escolar__r.Activo__c = true
                                                AND Ciclo_escolar__r.Tipo_de_ciclo__c = 'Comercial'
                                                AND Colegio__c = '{0}'", idAccount);

            var rPopulation = sfdc.ExecuteQuery(queryPopulation).Cast<Population__c>();

            foreach (var p in rPopulation)
                try
                {
                    populationsList.Add(new PopulationModel
                        {Grade = "1", Level = p.Nivel__c, Quantity = p.Grado_1__c.Value});
                    populationsList.Add(new PopulationModel
                        {Grade = "2", Level = p.Nivel__c, Quantity = p.Grado_2__c.Value});
                    populationsList.Add(new PopulationModel
                        {Grade = "3", Level = p.Nivel__c, Quantity = p.Grado_3__c.Value});

                    switch (p.Nivel__c)
                    {
                        case "Preescolar":
                            populationsList.Add(new PopulationModel
                                {Grade = "Maternal", Level = p.Nivel__c, Quantity = p.Maternal__c.Value});
                            populationsList.Add(new PopulationModel
                                {Grade = "Prefirst", Level = p.Nivel__c, Quantity = p.Prefirst__c.Value});
                            break;
                        case "Primaria":
                            populationsList.Add(new PopulationModel
                                {Grade = "4", Level = p.Nivel__c, Quantity = p.Grado_4__c.Value});
                            populationsList.Add(new PopulationModel
                                {Grade = "5", Level = p.Nivel__c, Quantity = p.Grado_5__c.Value});
                            populationsList.Add(new PopulationModel
                                {Grade = "6", Level = p.Nivel__c, Quantity = p.Grado_6__c.Value});

                            break;
                        case "Bachillerato":
                            populationsList.Add(new PopulationModel
                                {Grade = "4", Level = p.Nivel__c, Quantity = p.Grado_4__c.Value});
                            populationsList.Add(new PopulationModel
                                {Grade = "5", Level = p.Nivel__c, Quantity = p.Grado_5__c.Value});
                            populationsList.Add(new PopulationModel
                                {Grade = "6", Level = p.Nivel__c, Quantity = p.Grado_6__c.Value});
                            break;
                    }
                }
                catch (Exception)
                {
                }

            return populationsList;
        }
    }
}