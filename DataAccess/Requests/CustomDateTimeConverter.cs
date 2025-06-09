using System.Text.Json.Serialization;
using System.Text.Json;

namespace DataAccess.Requests
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format;

        public CustomDateTimeConverter(string format)
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (DateTime.TryParseExact(value, _format, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }
            throw new FormatException($"Не удалось преобразовать строку '{value}' в формат даты.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}