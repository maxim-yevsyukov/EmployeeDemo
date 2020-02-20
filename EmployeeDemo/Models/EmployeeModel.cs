using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Buffers.Text;
using System.Buffers;

namespace EmployeeDemo.Models
{
    public class EmployeeModel
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(IntToStringConverter))]
        public int Id { get; set; }

        [JsonPropertyName("employee_name")]
        public string Employee_Name { get; set; }

        [JsonPropertyName("employee_salary")]
        public string Employee_Salary { get; set; }

        [JsonPropertyName("employee_age")]
        public string Employee_Age { get; set; }

        [JsonIgnore]
        public BitmapImage Profile_Image { get; set; }
    }

    /// <summary>
    /// I need "Id" to be an integer for proper sorting, hence the custom converter
    /// </summary>
    public class IntToStringConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                if (Utf8Parser.TryParse(span, out int number, out int bytesConsumed) && span.Length == bytesConsumed)
                {
                    return number;
                }

                if (int.TryParse(reader.GetString(), out number))
                {
                    return number;
                }
            }

            return reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
