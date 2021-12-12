using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Net;
using System.Text.Json.Serialization;

namespace AoC.Library.Helpers
{
    public class InputSourceBase
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private const string _inputDir = "c:/aoc/{0}/input";
        private const string _secrets = "c:/aoc/{0}/secrets.json";
        private const string _inputNameFormat = "day{0}.txt";
        private const string _inputSourceFormat = "https://adventofcode.com/{0}/day/{1}/input";

        private string _secret = null;
        private readonly int _ofYear;

        private InputSourceBase(string sessionId, int ofYear)
        {
            _secret = sessionId;
            _ofYear = ofYear;
        }

        public async Task<TInput> GetInputForDay<TInput>(int dayNum, Func<string, TInput> transformFunc)
        {
            //i can reuse this next year
            var inputAddress = string.Format(_inputSourceFormat, 2021, dayNum);
            var fileInputName = Path.Combine(string.Format(_inputDir, _ofYear), string.Format(_inputNameFormat, dayNum));

            if (File.Exists(fileInputName))
                return transformFunc(File.ReadAllText(fileInputName));

            var request = new HttpRequestMessage(HttpMethod.Get, inputAddress);
            request.Headers.Add("Cookie", "session=" + _secret);

            var result = await _httpClient.SendAsync(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception("Could not get input from server: " + result.StatusCode);


            var input = await result.Content.ReadAsStringAsync();

            File.WriteAllText(fileInputName, input);
            return transformFunc(input);
        }

        public static InputSourceBase GetInstance(int ofYear)
        {
            var dir = string.Format(_inputDir, ofYear);
            var secrets = string.Format(_secrets, ofYear);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(secrets))
                throw new FileNotFoundException("Secrets file not found - could not get input");

            var text = File.ReadAllText(secrets);
            var secret = JsonSerializer.Deserialize<SecretDto>(text);

            return new InputSourceBase(secret.SessionId, ofYear);
        }
    }

    public class SecretDto
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }
    }
}
