using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class JsonExtensions
    {
        static readonly ILogger Logger = LoggerFactory.GetLogger(LoggingComponent.Common);

        private static readonly JsonSerializerSettings JsonSerializerSettingsDefault = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter>() { new StringEnumConverter() }
        };

        private static readonly JsonSerializerSettings JsonSerializerSettingCircularReferencesDefault = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter>() { new StringEnumConverter() },
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        /// <summary>
        /// Converts given object to JSON formated string.
        /// </summary>
        /// <param name="o">Object to serialize.</param>
        /// <param name="indented">Formating. If <c>true</c> string is Indented;otherwise not.</param>
        /// <param name="nullvalue">Value to return if null.</param>
        /// <returns>Returns JSON formated string from given object.</returns>
        public static string ToJson(this object o, bool indented = false, string nullvalue = "")
        {
            if (o == null) return nullvalue;

            var formatting = indented ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(o, formatting, JsonSerializerSettingsDefault);
        }

        public static string ToJsonWithRef(this object o, bool indented = false, string nullvalue = "")
        {
            if (o == null) return nullvalue;

            var formatting = indented ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(o, formatting, JsonSerializerSettingCircularReferencesDefault);
        }

        public static T FromJson<T>(this string json)
        {
            var item = default(T);
            if (json.IsWhiteSpace()) return item;
            try { item = JsonConvert.DeserializeObject<T>(json); }
            catch (Exception ex) { Logger.TraceError(ex.Message, ex); } // item = new T();
            return item;
        }

        /// <summary>
        /// Converts JSON to anonymous object. 
        /// </summary>
        /// <param name="json">JSON to deserialize.</param>
        /// <returns>Returns object.</returns>
        public static object FromJsonToObject(this string json)
        {
            var item = default(object);
            if (json.IsWhiteSpace()) return item;
            try { item = JsonConvert.DeserializeObject(json); }
            catch (Exception ex) { Logger.TraceError(ex.Message, ex); } // item = new T();
            return item;
        }

        /// <summary>
        /// Converts JSON to given object. 
        /// </summary>
        /// <param name="json">JSON to deserialize.</param>
        /// <returns>Returns IDictionary_string,object_.</returns>
        public static IDictionary<string, object> FromJsonToDictionary(this string json)
        {
            var d = default(IDictionary<string, object>);
            if (json.IsWhiteSpace()) return d;
            d = JsonConvert.DeserializeObject<IDictionary<string, object>>(json);
            return d;
        }

    }
}
