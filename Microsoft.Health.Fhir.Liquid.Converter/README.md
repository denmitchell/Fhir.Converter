1. Created C# .NET 8.0 Library Project: Microsoft.Health.Fhir.Liquid.Converter
2. Exited Visual Studio
3. Downloaded zip at https://github.com/microsoft/FHIR-Converter/tree/main
4. Replaced Microsoft.Health.Fhir.Liquid.Converter folder in solution with corresponding folder from Zip
5. Copied data\Templates folder from Zip into Microsoft.Health.Fhir.Liquid.Converter project
6. Updated Microsoft.Health.Fhir.Liquid.Converter.csproj so that meta-schema.json file path is correct
   Template\Json\Schema\meta-schema.json
7. Updated all files to ""copy if newer"" in Microsoft.Health.Fhir.Liquid.Converter.Templates

  <ItemGroup>
    <None Include="Templates\**\*.*">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
 
8. Removed Microsoft.Health.MeasurementUtility from the .csproj file
9. Removed Microsoft.Health.MeasurementUtility related code from JsonProcessor.cs, Hl7v2Processor.cs, 
      FhirToHl7v2Processor.cs, CcdaProcessor.cs, BaseProcessor.cs 
   a. Removed import statement: using Microsoft.Health.MeasurementUtility;
   b. Removed performance tracking wrapper code ... using (ITimed inputDeserializationTime = ... { 	
10. Updated TargetFramework in .csproj to net8.0
11. Solution should compile
12. Add Converter Web API (additional work to be done here later)
13. In StringFilters.cs, updated the following line to remove warning

            //using var sha1 = new SHA1Managed(); // lgtm[cs/weak-crypto]
            using var sha1 = SHA1.Create(); // lgtm[cs/weak-crypto]
14. In ConvertHl7MessageToString.cs, update the following line to use \n, instead of \r\n

                //sb.AppendLine();
                sb.Append('\n');

 1. Create xUnit Test Project Microsoft.Health.Fhir.Liquid.Converter.UnitTests
 2. Exited Visual Studio
 3. Replaced Microsoft.Health.Fhir.Liquid.Converter.UnitTests folder in solution with corresponding folder from Zip
 4. Copied data\SampleData folder from Zip into Microsoft.Health.Fhir.Liquid.Converter.UnitTests
 5. Updated TestConstants.cs code that points to Templates and SampleData
    a.  public static readonly string SampleDataDirectory = Path.Join("SampleData");
    b.  public static readonly string TemplateDirectory = Path.Join("Templates");
 6. Updated TargetFramework in .csproj to net8.0
 7. Update all files to ""copy if newer"" in Microsoft.Health.Fhir.Liquid.Converter.UnitTests.SampleData
 
  <ItemGroup>
    <None Include="SampleData\**\*.*">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>


 8. Solution should compile
 9. All UnitTests should pass

