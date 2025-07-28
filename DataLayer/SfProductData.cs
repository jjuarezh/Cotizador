using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Utils;
using DataLayer.Sfdc.Enterprise;
using ModelLayer;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SfProductData : Salesforce
    {
        public async Task<ResponseService<ProductModel>> GetProducts(DateTime? lastDate = null)
        {
            ResponseService<ProductModel> response = new ResponseService<ProductModel>();

            try
            {
                if (lastDate == null) lastDate = Convert.ToDateTime("1900/01/01");

                var soql = string.Format(@"SELECT  
	                                                Product2.Id
                                                ,	Product2.ProductCode
                                                ,	Product2.Name
                                                ,	Product2.Materia__r.Name
                                                ,	Product2.Coleccion__r.Name
                                                ,	Product2.Linea_de_Negocio__r.Name
                                                ,	Product2.Nivel__r.Name
                                                ,	Product2.Family
                                                ,	Product2.Componente_curricular__c
                                                ,	Product2.Campo_rea_mbito__c
                                                ,	Product2.Enfoque__c
                                                ,	Product2.Formato__c
                                                ,	Product2.Mercado__c
                                                ,	Product2.Tipo__c
                                                ,	Product2.NivelGrado__r.Name
                                                ,	Product2.NivelGrado__r.Grado__r.Name
                                                ,   Product2.Cantidad__c
                                                ,	UnitPrice
                                            FROM    PricebookEntry
                                            WHERE   Product2.Clase__c INCLUDES('Cotizador')
                                                    AND Product2.Componente_curricular__c != null
                                                    AND UnitPrice > 0                    
                                                    AND IsActive = true
                                                    AND Product2.IsActive = true
                                            ", lastDate);
                //                 AND Product2.LastModifiedDate  > {0:yyyy-MM-ddTHH:mm:ssZ}

                var products = ExecuteQuery(soql).Cast<PricebookEntry>();

                response.ResulList = PricebookEntry2ProductModels(products.ToList());
                //return PricebookEntry2ProductModels(products.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return response;
        }

        private List<ProductModel> PricebookEntry2ProductModels(List<PricebookEntry> products)
        {
            var models = new List<ProductModel>();

            try
            {
                foreach (var p in products)
                {
                    if (p.Product2 == null) continue;

                    var product = new ProductModel
                    {
                        Id = p.Product2.Id,
                        Label = string.Format("{0} - {1}", p.Product2.ProductCode, p.Product2.Name).Trim(),
                        Price = p.UnitPrice.Value,
                        ProductCode = p.Product2.ProductCode,
                        Quantity = Convert.ToInt32(p.Product2.Cantidad__c ?? 0),
                        Title = p.Product2.Name,
                    };


                    var metadata = new List<string>();

                    var type = "";
                    if (p.Product2.Coleccion__r.Name.Contains("Loran"))
                        type = "Loran";
                    else if (p.Product2.Mercado__c == "Literatura")
                        type = "Papel";
                    else if (p.Product2.Componente_curricular__c.Contains("Autonomía curricular"))
                        type = "Complemento";
                    else
                        type = "Base";
                    metadata.Add(type);

                    product.Type = type;

                    if (p.Product2.Mercado__c != null)
                    {
                        metadata.Add(p.Product2.Mercado__c);
                        product.BusinessLine = p.Product2.Mercado__c;
                    }

                    if (p.Product2.Family != null)
                        metadata.Add(p.Product2.Family);
                    if (p.Product2.Materia__r != null)
                    {
                        product.Subject = p.Product2.Materia__r.Name;
                        metadata.Add(p.Product2.Materia__r.Name);
                    }
                    if (p.Product2.Coleccion__r != null)
                        metadata.Add(p.Product2.Coleccion__r.Name);
                    if (p.Product2.Linea_de_negocio__r != null)
                        metadata.Add(p.Product2.Linea_de_negocio__r.Name);
                    if (p.Product2.Nivel__r != null)
                    {
                        product.Level = p.Product2.Nivel__r.Name;
                        metadata.Add(p.Product2.Nivel__r.Name);
                    }
                    if (p.Product2.Componente_curricular__c != null)
                        metadata.AddRange(p.Product2.Componente_curricular__c.Split(';'));
                    if (p.Product2.Campo_rea_mbito__c != null)
                        metadata.AddRange(p.Product2.Campo_rea_mbito__c.Split(';'));
                    if (p.Product2.Formato__c != null)
                        metadata.Add(p.Product2.Formato__c);
                    if (p.Product2.Mercado__c != null)
                        metadata.Add(p.Product2.Mercado__c);
                    if (p.Product2.Tipo__c != null)
                        metadata.Add(p.Product2.Tipo__c);
                    if (p.Product2.NivelGrado__r != null)
                    {
                        metadata.Add(p.Product2.NivelGrado__r.Name);
                        product.Grade = p.Product2.NivelGrado__c ?? p.Product2.NivelGrado__r.Name;

                        if (p.Product2.NivelGrado__r.Grado__r != null)
                            metadata.Add(p.Product2.NivelGrado__r.Grado__r.Name);
                    }

                    product.Metadata = metadata.Where(x => x != "").Distinct(StringComparer.CurrentCultureIgnoreCase)
                        .OrderBy(s => s).ToList();

                    models.Add(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return models;
        }
    }
}