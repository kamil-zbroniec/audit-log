using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuditLog.Api.AuditLogs.Entities
{
    public class EntityDetailsConverter : JsonConverter<IEntityDetails>
    {
        public override EntityDetails Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IEntityDetails value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case ContractHeaderEntityDetails contractHeaderEntityDetails:
                    JsonSerializer.Serialize(writer, contractHeaderEntityDetails, options);
                    break;
                case EntityDetails entityDetails:
                    JsonSerializer.Serialize(writer, entityDetails, options);
                    break;
                default:
                    JsonSerializer.Serialize(writer, null!, options);
                    break;
            }
        }
    }
}