using Microsoft.AspNetCore.Mvc;
using Microsoft.Health.Fhir.Liquid.Converter;
using Microsoft.Health.Fhir.Liquid.Converter.Exceptions;
using Microsoft.Health.Fhir.Liquid.Converter.Models;
using Microsoft.Health.Fhir.Liquid.Converter.Processors;

namespace Fhir.Converter.Api.Controllers
{
    public class ConverterController(CachedTemplateProviderDictionary cache) : Controller
    {
        private readonly CachedTemplateProviderDictionary cache = cache;

        private static readonly ProcessorSettings processorSettings = new() { };


        [HttpPost("hl7v2-to-fhir/{template}")]
        [Consumes("text/plain")]
        public IActionResult Hl7v2ToFhir(string template, [FromBody] string payload,
            [FromServices] ILogger<Hl7v2Processor> logger)
        {
            ITemplateProvider templateProvider = cache.TemplateProviders[
                CachedTemplateProviderDictionary.HL7V2_TEMPLATE_PROVIDER_KEY];

            Hl7v2Processor processor = new(processorSettings, logger);

            try
            {
                string result = processor.Convert(payload, template, templateProvider);
                return new ContentResult { Content = result };
            }
            catch (TemplateLoadException ex)
            {
                return new BadRequestObjectResult($"Could not process hl7v2-to-fhir/{{template}}: {ex.Message}");
            }

        }


        [HttpPost("ccda-to-fhir/{template}")]
        [Consumes("text/plain")]
        public IActionResult CcdaToFhir(string template, [FromBody] string payload,
            [FromServices] ILogger<CcdaProcessor> logger)
        {
            ITemplateProvider templateProvider = cache.TemplateProviders[
                CachedTemplateProviderDictionary.HL7V2_TEMPLATE_PROVIDER_KEY];

            CcdaProcessor processor = new(processorSettings, logger);

            try
            {
                string result = processor.Convert(payload, template, templateProvider);
                return new ContentResult { Content = result };
            }
            catch (TemplateLoadException ex)
            {
                return new BadRequestObjectResult($"Could not process ccda-to-fhir/{{template}}: {ex.Message}");
            }

        }


        [HttpPost("json-to-fhir/{template}")]
        [Consumes("text/plain")]
        public IActionResult JsonToFhir(string template, [FromBody] string payload,
            [FromServices] ILogger<CcdaProcessor> logger)
        {
            ITemplateProvider templateProvider = cache.TemplateProviders[
                CachedTemplateProviderDictionary.HL7V2_TEMPLATE_PROVIDER_KEY];

            CcdaProcessor processor = new(processorSettings, logger);

            try
            {
                string result = processor.Convert(payload, template, templateProvider);
                return new ContentResult { Content = result };
            }
            catch (TemplateLoadException ex)
            {
                return new BadRequestObjectResult($"Could not process json-to-fhir/{{template}}: {ex.Message}");
            }

        }


    }
}
