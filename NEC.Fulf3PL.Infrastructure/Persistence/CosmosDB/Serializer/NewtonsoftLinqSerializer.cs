using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Serializer
{
    public class NewtonsoftLinqSerializer : CosmosLinqSerializer
    {
        private readonly JsonSerializer _jsonSerializer;

        public NewtonsoftLinqSerializer(JsonSerializerSettings settings)
        {
            _jsonSerializer = JsonSerializer.Create(settings);
        }

        public override T FromStream<T>(Stream stream)
        {
            if (stream == null || stream.Length == 0)
            {
                return default!;
            }

            using (stream)
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                return _jsonSerializer.Deserialize<T>(jsonTextReader)!;
            }
        }

        public override Stream ToStream<T>(T input)
        {
            MemoryStream memoryStream = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(memoryStream, leaveOpen: true))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                _jsonSerializer.Serialize(jsonTextWriter, input);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public override string SerializeMemberName(MemberInfo memberInfo)
        {
            var jsonProperty = memberInfo.GetCustomAttribute<JsonPropertyAttribute>();
            if (jsonProperty != null && !string.IsNullOrEmpty(jsonProperty.PropertyName))
            {
                return jsonProperty.PropertyName;
            }

            return memberInfo.Name;
        }
    }
}
