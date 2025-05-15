using DotLiquid;
using Microsoft.Health.Fhir.Liquid.Converter;
using Microsoft.Health.Fhir.Liquid.Converter.Models;
using Microsoft.Health.Fhir.Liquid.Converter.Processors;
using Microsoft.Health.Fhir.Liquid.Converter.Utilities;

namespace Fhir.Converter.Api.Controllers
{
    /// <summary>
    /// This class provides an outer cache wrapper around the Microsoft TemplateProvider class, which
    /// has its own internal cache behavior.  (It only Parses a liquid template when
    /// the template is not already parsed and available from the cache.)  This outer cache
    /// wrapper simply allows a dictionary of TemplateProvider objects to be served up as a singleton. 
    /// </summary>
    /// <see cref="TemplateProvider"/>
    public class CachedTemplateProviderDictionary
    {
        public Dictionary<string, ITemplateProvider> TemplateProviders { get; set; }

        public const string HL7V2_TEMPLATE_PROVIDER_KEY = "Hl7v2";
        public const string CCDA_TEMPLATE_PROVIDER_KEY = "Ccda";
        public const string JSON_TEMPLATE_PROVIDER_KEY = "Json";
        public const string FHIRSTU3_TEMPLATE_PROVIDER_KEY = "Stu3ToR4";


        private static readonly string TemplateDirectory = Path.Join("Templates");
        private static readonly string Hl7v2TemplateDirectory = Path.Join(TemplateDirectory, HL7V2_TEMPLATE_PROVIDER_KEY);
        private static readonly string CcdaTemplateDirectory = Path.Join(TemplateDirectory, CCDA_TEMPLATE_PROVIDER_KEY);
        private static readonly string JsonTemplateDirectory = Path.Join(TemplateDirectory, JSON_TEMPLATE_PROVIDER_KEY);
        private static readonly string FhirStu3TemplateDirectory = Path.Join(TemplateDirectory, FHIRSTU3_TEMPLATE_PROVIDER_KEY);


        public CachedTemplateProviderDictionary()
        {
            TemplateProviders = new()
            {
                { "Hl7v2", new TemplateProvider(Hl7v2TemplateDirectory, DataType.Hl7v2) },
                { "Ccda", new TemplateProvider(CcdaTemplateDirectory, DataType.Ccda) },
                { "Json", new TemplateProvider(JsonTemplateDirectory, DataType.Json) },
                { "Fhir", new TemplateProvider(FhirStu3TemplateDirectory, DataType.Fhir) }
            };
        }


    }
}
