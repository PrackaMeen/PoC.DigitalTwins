namespace PoC.DigitalTwins.DTDL.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Azure.DigitalTwins.Parser;
using System.IO;
using System.Linq;

public class DtdlParserTests
{
    protected static async Task<string> LoadDtdlByName(string dtdlName)
    {
        var relativePath = Path.Combine("Examples", dtdlName);
        var fullPath = Path.GetFullPath($"./{relativePath}.json");
        string serializedDtdl = await DtdlReader.LoadDtdlBy(fullPath);
        return serializedDtdl;
    }

    [Theory]
    [InlineData("InvalidDTDL")]
    [InlineData("ValidJSON")]
    [InlineData("InvalidJSON")]
    public async Task ParseModel_InvalidFileContent(string fileName)
    {
        string serializedDtdl = await LoadDtdlByName(fileName);
        List<string> serializedDtdls = new() { serializedDtdl };

        DtdlParser parser = new();
        var isValid = await parser.AreDtdlsValid(serializedDtdls);

        Assert.False(isValid);
    }

    [Theory]
    [InlineData("Interface")]
    [InlineData("InterfaceWithEnum")]
    [InlineData("InterfaceWithComponent")]
    [InlineData("InterfaceWithProperties")]
    [InlineData("InterfaceWithPropertyAndRelationship")]
    [InlineData("TelemetryEnum")]
    [InlineData("PropertyEnum")]
    [InlineData("Enum")]
    public async Task ParseModel_ParseSingle_NotEmpty(string fileName)
    {
        string serializedDtdl = await LoadDtdlByName(fileName);
        List<string> serializedDtdls = new() { serializedDtdl };

        DtdlParser parser = new();
        var actual = await parser.ParseModels(serializedDtdls);

        Assert.NotEmpty(actual);
    }

    [Theory]
    [InlineData("InterfaceExtendsWithProperty", new string[] { "InterfaceWithProperties" })]
    [InlineData("InterfaceDependentEnum", new string[] { "Enum" })]
    public async Task ParseModel_ParseWithDependencies_NotEmpty(string fileName, string[] dependencyNames)
    {
        List<string> dtdlNamesToLoad = new(dependencyNames)
        {
            fileName
        };

        List<string> serializedDtdls = new();
        foreach (var dtdlName in dtdlNamesToLoad)
        {
            string serializedDtdl = await LoadDtdlByName(dtdlName);
            serializedDtdls.Add(serializedDtdl);
        }

        DtdlParser parser = new();
        var actual = await parser.ParseModels(serializedDtdls);

        Assert.NotEmpty(actual);
    }

    [Theory]
    [InlineData("InterfaceExtendsWithProperty")]
    [InlineData("InterfaceDependentEnum")]
    public async Task ParseModel_Parse_DependencyMissing_ResolutionException(string fileName)
    {
        string serializedDtdl = await LoadDtdlByName(fileName);
        List<string> serializedDtdls = new() { serializedDtdl };

        DtdlParser parser = new();
        await Assert.ThrowsAsync<ResolutionException>(async () => await parser.ParseModels(serializedDtdls));
    }

    [Theory]
    [InlineData("Interface")]
    [InlineData("InterfaceWithEnum")]
    [InlineData("InterfaceWithComponent")]
    [InlineData("InterfaceWithProperties")]
    [InlineData("InterfaceWithPropertyAndRelationship")]
    [InlineData("TelemetryEnum")]
    [InlineData("PropertyEnum")]
    [InlineData("Enum")]
    public async Task GetValidationErrors_ValidModels(string fileName)
    {
        string serializedDtdl = await LoadDtdlByName(fileName);

        DtdlParser parser = new();
        var isValid = await parser.IsDtdlValid(serializedDtdl);
        Assert.True(isValid);
    }

    [Theory]
    [InlineData("InterfaceExtendsWithProperty")]
    [InlineData("InterfaceDependentEnum")]
    public async Task GetValidationErrors_InvalidModels(string fileName)
    {
        string serializedDtdl = await LoadDtdlByName(fileName);
        List<string> serializedDtdls = new() { serializedDtdl };

        DtdlParser parser = new();
        var isValid = await parser.AreDtdlsValid(serializedDtdls);
        Assert.False(isValid);
    }

    [Fact]
    public async Task LoadAllDtdlsFrom()
    {
        var allSerializedDtdls = await DtdlReader.LoadAllDtdlsFrom(Path.Combine("./", "Tests"));

        Assert.NotEmpty(allSerializedDtdls);
        Assert.Equal(25, allSerializedDtdls.Count());
    }
}
