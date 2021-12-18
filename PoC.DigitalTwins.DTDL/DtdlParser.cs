namespace PoC.DigitalTwins.DTDL;
using Microsoft.Azure.DigitalTwins.Parser;

public class DtdlParser
{
    ModelParser ModelParser { get; }
    public DtdlParser()
    {
        ModelParser = new ModelParser();
    }

    public async Task<bool> IsDtdlValid(string jsonText)
    {
        return await AreDtdlsValid(new string[] { jsonText });
    }
    public async Task<bool> AreDtdlsValid(IEnumerable<string> jsonTexts)
    {
        try
        {
            await ModelParser.ParseAsync(jsonTexts);
            return true;
        }
        catch (ParsingException)
        {
            return false;
        }
        catch(ResolutionException)
        {
            return false;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    public async Task<IReadOnlyDictionary<Dtmi, DTEntityInfo>> ParseModels(IEnumerable<string> jsonModels)
    {
        return await ModelParser.ParseAsync(jsonModels);
    }

    public async Task<IReadOnlyDictionary<Dtmi, DTEntityInfo>> ParseModels2(IEnumerable<string> jsonModels)
    {
        return await ModelParser.ParseAsync(jsonModels);
    }
}
