using Fhir.Converter.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Health.Fhir.Liquid.Converter;
using Microsoft.Health.Fhir.Liquid.Converter.Exceptions;
using Microsoft.Health.Fhir.Liquid.Converter.Models;
using Microsoft.Health.Fhir.Liquid.Converter.Processors;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Fhir.Converter.Api.Controllers
{
    [ApiController]
    public class ConverterController(CachedTemplateProviderDictionary cache) : Controller
    {
        private readonly CachedTemplateProviderDictionary cache = cache;

        private static readonly ProcessorSettings processorSettings = new() { };


        /// <summary>
        /// Converts HL7v2 to FHIR using the specified template (message structure)
        /// </summary>
        /// <param name="template">The HL7 Message Structure (e.g., ORU_R01).</param>
        /// <param name="payload">The raw HL7 v2 message.</param>
        /// <returns>FHIR JSON output</returns>
        [HttpPost("hl7v2-to-fhir/{template}")]
        [Consumes("text/plain")]
        [Produces("application/xml")]
        public IActionResult Hl7v2ToFhir(Hl7ToFhirTemplates template, [FromBody] string payload,
            [FromServices] ILogger<Hl7v2Processor> logger)
        {
            ITemplateProvider templateProvider = cache.TemplateProviders[
                CachedTemplateProviderDictionary.HL7V2_TEMPLATE_PROVIDER_KEY];

            Hl7v2Processor processor = new(processorSettings, logger);

            try
            {
                string result = processor.Convert(payload, template.ToString(), templateProvider);
                return new ContentResult { Content = result };
            }
            catch (TemplateLoadException ex)
            {
                return new BadRequestObjectResult($"Could not process hl7v2-to-fhir/{{template}}: {ex.Message}");
            }

        }


        /// <summary>
        /// Converts FHIR to HL7v2 using the specified template. 
        /// ** Special Note: Currently only ObservationBundle (ORU_R01) is supported
        /// </summary>
        /// <param name="template">ObservationBundle</param>
        /// <param name="payload">The FHIR JSON message</param>
        /// <returns>Pipe-delimited HL7 output</returns>
       [HttpPost("fhir-to-hl7v2/{template}")]
       [Consumes("application/json")]
       [Produces("text/plain")]
        public IActionResult FhirToHl7v2(FhirToHl7v2Templates template, [FromBody] JsonDocument payload,
            [FromServices] ILogger<FhirToHl7v2Processor> logger)
        {
            ITemplateProvider templateProvider = cache.TemplateProviders[
                CachedTemplateProviderDictionary.FHIR_TO_HL7_TEMPLATE_PROVIDER_KEY];

            string jsonString = payload.RootElement.GetRawText();
            JObject jObject = JObject.Parse(jsonString);

            FhirToHl7v2Processor processor = new(processorSettings, logger);

            try
            {
                string result = processor.Convert(jObject, template.ToString(), templateProvider);
                return new ContentResult { Content = result };
            }
            catch (TemplateLoadException ex)
            {
                return new BadRequestObjectResult($"Could not process fhir-to-hl7v2/{{template}}: {ex.Message}");
            }

        }


        /// <summary>
        /// Converts CCDA to FHIR using the specified template
        /// ** Special Note: only CCD has been tested, but other templates may work
        /// To provide support, add additional enumeration to CcdaToFhirTemplates
        /// </summary>
        /// <param name="template">The HL7 Message Structure (e.g., ORU_R01).</param>
        /// <param name="payload">The raw HL7 v2 message.</param>
        /// <returns>FHIR JSON output</returns>
        [HttpPost("ccda-to-fhir/{template}")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        public IActionResult CcdaToFhir(CcdaToFhirTemplates template, [FromBody] string payload,
            [FromServices] ILogger<CcdaProcessor> logger)
        {
            ITemplateProvider templateProvider = cache.TemplateProviders[
                CachedTemplateProviderDictionary.CCDA_TEMPLATE_PROVIDER_KEY];

            CcdaProcessor processor = new(processorSettings, logger);

            try
            {
                string result = processor.Convert(payload, template.ToString(), templateProvider);
                return new ContentResult { Content = result };
            }
            catch (TemplateLoadException ex)
            {
                return new BadRequestObjectResult($"Could not process ccda-to-fhir/{{template}}: {ex.Message}");
            }

        }


        /// <summary>
        /// Converts STU3 to R4 FHIR using the specified template
        /// </summary>
        /// <param name="template">One of the top-level liquid templates</param>
        /// <param name="payload">STU3 FHIR JSON</param>
        /// <returns>R4 FHIR JSON</returns>
        [HttpPost("stu3-to-r4fhir/{template}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Stu3ToR4Fhir(Stu3ToR4FhirTemplates template, [FromBody] string payload,
            [FromServices] ILogger<FhirProcessor> logger)
        {
            ITemplateProvider templateProvider = cache.TemplateProviders[
                CachedTemplateProviderDictionary.FHIRSTU3_TEMPLATE_PROVIDER_KEY];

            FhirProcessor processor = new(processorSettings, logger);

            try
            {
                string result = processor.Convert(payload, template.ToString(), templateProvider);
                return new ContentResult { Content = result };
            }
            catch (TemplateLoadException ex)
            {
                return new BadRequestObjectResult($"Could not process stu3-to-r4fhir/{{template}}: {ex.Message}");
            }

        }


    }
}
