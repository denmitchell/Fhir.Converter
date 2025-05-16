using DotLiquid;
using Microsoft.Health.Fhir.Liquid.Converter;
using Microsoft.Health.Fhir.Liquid.Converter.Models;
using Microsoft.Health.Fhir.Liquid.Converter.Processors;
using Microsoft.Health.Fhir.Liquid.Converter.Utilities;

namespace Fhir.Converter.Api.Common
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

        public const string HL7V2_TEMPLATE_PROVIDER_KEY = "Hl7v2ToFhir";
        public const string CCDA_TEMPLATE_PROVIDER_KEY = "CcdaToFhir";
        public const string FHIR_TO_HL7_TEMPLATE_PROVIDER_KEY = "FhirToHl7v2";
        public const string FHIRSTU3_TEMPLATE_PROVIDER_KEY = "Stu3ToR4Fhir";


        private static readonly string TemplateDirectory = Path.Join("Templates");
        private static readonly string Hl7v2TemplateDirectory = Path.Join(TemplateDirectory, "Hl7v2");
        private static readonly string CcdaTemplateDirectory = Path.Join(TemplateDirectory, "Ccda");
        private static readonly string FhirToHl7v2TemplateDirectory = Path.Join(TemplateDirectory, "FhirToHl7v2");
        private static readonly string Stu3ToR4TemplateDirectory = Path.Join(TemplateDirectory, "Stu3ToR4");


        public CachedTemplateProviderDictionary()
        {
            TemplateProviders = new()
            {
                { HL7V2_TEMPLATE_PROVIDER_KEY, new TemplateProvider(Hl7v2TemplateDirectory, DataType.Hl7v2) },
                { CCDA_TEMPLATE_PROVIDER_KEY, new TemplateProvider(CcdaTemplateDirectory, DataType.Ccda) },
                { FHIRSTU3_TEMPLATE_PROVIDER_KEY, new TemplateProvider(Stu3ToR4TemplateDirectory, DataType.Fhir) },
                { FHIR_TO_HL7_TEMPLATE_PROVIDER_KEY, new TemplateProvider(FhirToHl7v2TemplateDirectory, DataType.Fhir) }
            };
        }


    }
}
