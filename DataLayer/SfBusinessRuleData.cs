using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Utils;
using DataLayer.Sfdc.Enterprise;
using ModelLayer;

namespace DataLayer
{
    public class SfBusinessRuleData:Salesforce
    {
        public async Task<ResponseService<BusinessRuleModel>> GetBusinessRules()
        {
            var response = new ResponseService<BusinessRuleModel>();

            try
            {
                const string soql = @"SELECT 
                            Id
                        ,   Nivel__c
                        ,   Linea_de_Negocio__c
                        ,   Tipo_de_Producto__c
                        ,   Cantidad__c
                        ,   Descuento__c 
                        FROM  Simulador_Reglas_de_Negocio__c
                        WHERE (Ciclo_escolar__r.Tipo_de_ciclo__c = 'Comercial'
                            AND Ciclo_escolar__r.Activo__c = true)";
                var rQuery = ExecuteQuery(soql);
                if (!rQuery.Any()) return null;

                response.ResulList = rQuery.Cast<Simulador_Reglas_de_Negocio__c>()
                    .Select(r => new BusinessRuleModel
                    {
                        Id = r.Id,
                        Level = r.Nivel__c,
                        BusinessLine = r.Linea_de_negocio__c,
                        ProductType = r.Tipo_de_Producto__c,
                        Quantity = r.Cantidad__c.GetValueOrDefault(),
                        Discount = r.Descuento__c.GetValueOrDefault()
                    })
                    .ToList();
                return response;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return response;
        }

    }
}
