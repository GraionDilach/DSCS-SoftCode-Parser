using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text.Json;

namespace DSCS_SoftCode_Parser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var digimonDict = new Dictionary<string, string>();
            var digimonTextDict = new Dictionary<string, string>();

            using var digimonReader = new StreamReader("Digimon.json");
            {
                using var digimonJson = JsonDocument.Parse(digimonReader.ReadToEnd());
                {
                    foreach (var digimon in digimonJson.RootElement.EnumerateObject())
                    {
                        if (digimon.Value.ValueKind == JsonValueKind.Array)
                        {
                            digimonDict.Add(digimon.Name, digimon.Value[0].GetRawText());
                        }
                    }
                }
            }

            var digimonCSV = "";
            foreach (var item in digimonDict)
            {
                digimonCSV += item.Key + "," + item.Value + Environment.NewLine;
            }

            File.WriteAllText("digimon.csv", digimonCSV);

            using var digimonTextReader = new StreamReader("DigimonText.json");
            {
                using var digimonTextJson = JsonDocument.Parse(digimonTextReader.ReadToEnd());
                {
                    foreach (var digimon in digimonTextJson.RootElement.EnumerateObject())
                    {
                        if (digimon.Value.ValueKind == JsonValueKind.Array)
                        {
                            digimonTextDict.Add(digimon.Name, digimon.Value[0].GetRawText());
                        }
                    }
                }
            }

            var digimonTextCSV = "";
            foreach (var item in digimonTextDict)
            {
                digimonTextCSV += item.Key + "," + item.Value + Environment.NewLine;
            }

            File.WriteAllText("digimonText.csv", digimonTextCSV);


            var bothDigimonDict = new Dictionary<string, string>();
            var onlyDigimonDict = new Dictionary<string, string>();
            var onlyDigimonTextDict = new Dictionary<string, string>();

            foreach (var item in digimonTextDict)
            {
                if (digimonDict.TryGetValue(item.Key, out var digimon))
                {
                    bothDigimonDict.Add(item.Key, item.Value + "," + digimon);
                }
                else
                {
                    onlyDigimonTextDict.Add(item.Key, item.Value);
                }
            }

            foreach (var item in digimonDict)
            {
                if (!digimonTextDict.TryGetValue(item.Key, out var digimon))
                {
                    onlyDigimonDict.Add(item.Key, item.Value);
                }
            }

            var bothDigimonCSV = "";
            foreach (var item in bothDigimonDict)
            {
                bothDigimonCSV += item.Key + "," + item.Value + Environment.NewLine;
            }

            File.WriteAllText("bothdigimon.csv", bothDigimonCSV);

            var onlyDigimonCSV = "";
            foreach (var item in onlyDigimonDict)
            {
                onlyDigimonCSV += item.Key + "," + item.Value + Environment.NewLine;
            }

            File.WriteAllText("onlyDigimon.csv", onlyDigimonCSV);

            var onlyDigimonTextCSV = "";
            foreach (var item in onlyDigimonTextDict)
            {
                onlyDigimonTextCSV += item.Key + "," + item.Value + Environment.NewLine;
            }

            File.WriteAllText("onlyDigimonText.csv", onlyDigimonTextCSV);
        }
    }
}
