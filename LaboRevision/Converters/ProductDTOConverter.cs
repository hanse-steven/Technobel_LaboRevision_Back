using System.Text.Json;
using System.Text.Json.Serialization;
using LaboRevision.DTO;

namespace LaboRevision.Converters;

public class ProductShortDTOConverter : JsonConverter<Dictionary<ProductShortDTO, int>>
{
    public override Dictionary<ProductShortDTO, int>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        var dictionary = new Dictionary<ProductShortDTO, int>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return dictionary;
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var product = new ProductShortDTO();
                int quantity = 0;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        break;
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        var propertyName = reader.GetString();
                        reader.Read();

                        switch (propertyName)
                        {
                            case "id":
                                product.Id = reader.GetGuid();
                                break;
                            case "name":
                                product.Name = reader.GetString();
                                break;
                            case "price":
                                product.Price = reader.GetDecimal();
                                break;
                            case "quantity":
                                quantity = reader.GetInt32();
                                break;
                        }
                    }
                }

                dictionary[product] = quantity;
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<ProductShortDTO, int> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var kvp in value)
        {
            writer.WriteStartObject();
            writer.WriteString("id", kvp.Key.Id.ToString());
            writer.WriteString("name", kvp.Key.Name);
            writer.WriteNumber("price", kvp.Key.Price);
            writer.WriteNumber("quantity", kvp.Value);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
}