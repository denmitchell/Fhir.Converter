namespace Fhir.Converter.Api.Common
{

    /// <summary>
    /// Supported top-level .liquid templates in Templates\Hl7ToFhir directory
    /// </summary>
    public enum Hl7ToFhirTemplates
    {
        ADT_A01, //Syndromic
        ADT_A03, //Syndromic
        ADT_A04, //Syndromic
        ADT_A08, //Syndromic
        MDM_T01, //Master Data Management
        MDM_T02, //Master Data Management
        MDM_T05, //Master Data Management
        MDM_T06, //Master Data Management
        MDM_T09, //Master Data Management
        MDM_T10, //Master Data Management
        ORU_R01, //ELR
        ORM_O01, //ELR
        OML_O21, //Newborn Screening
        VXU_V04  //Immunizations
    }
}
