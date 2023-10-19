using DataAccess.Interfaces;
using Entities;
using System.Text.Json;

namespace DataAccess.FileRepository.Parsers
{
    public class JsonParser : IJsonParser
    {
        public T? Parse<T>(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return default;
            }
            try
            {
                return JsonSerializer.Deserialize<T>(content);
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Unable to json parsing.", ex);
            }
        }
    }
}
