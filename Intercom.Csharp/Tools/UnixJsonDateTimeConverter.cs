﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Intercom.Csharp.Tools
{
    /// <summary>
    /// Since Intercom needs dates back as unix timestamps, we need a custom converter for serialization.
    /// </summary>
    public class UnixJsonDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer jsonSerializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(
                    String.Format("Unexpected token parsing date. Expected Integer, got {0}.",
                    reader.TokenType));
            }

            var ticks = (long)reader.Value;

            var date = new DateTime(1970, 1, 1);
            date = date.AddSeconds(ticks);

            return date;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            long ticks;
            if (value is DateTime)
            {
                var epoc = new DateTime(1970, 1, 1);
                var delta = ((DateTime)value) - epoc;
                if (delta.TotalSeconds < 0)
                {
                    delta = new TimeSpan(0);
                }
                ticks = (long)delta.TotalSeconds;
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
            writer.WriteValue(ticks);
        }
    }
}
