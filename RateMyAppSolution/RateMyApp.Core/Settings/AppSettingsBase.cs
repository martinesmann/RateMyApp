using RateMyApp.Core.Abstractions.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RateMyApp.Core.Settings
{
    public class AppSettingsBase
    {
        private readonly string settingsFileName = "appreview.txt";
        private IFile file;
        public AppSettingsBase(IFile fileInstance)
        {
            file = fileInstance;
        }

        async public Task<string> SaveJson(string json)
        {
            await file.WriteTextFile(settingsFileName, json);
            return json;
        }

        async public Task<string> GetJson()
        {
            var json = await file.ReadTextFile(settingsFileName);
            if (!string.IsNullOrWhiteSpace(json))
            {
                return json.TrimEnd('\0');
            }
            return json;
        }

        public async Task<object> GetValue(string key)
        {
            object result = null;
            var settings = await GetSettingsContainer();
            if (settings != null)
            {
                var setting = settings.Settings.Where(item => item.Key == key).FirstOrDefault();
                if (setting != null)
                {
                    result = setting.Value;
                }
            }
            return result;
        }

        public async Task SetValue(string key, object value)
        {
            var settings = await GetSettingsContainer();
            if (settings != null)
            {
                var setting = settings.Settings.Where(item => item.Key == key).FirstOrDefault();
                if (setting != null)
                {
                    setting.Value = value;
                }
                else
                {
                    settings.Settings.Add(new KeyValuePair { Key = key, Value = value });
                }
                await SaveSettingsContainer(settings);
            }
        }

        async Task<JsonSettingsContainer> GetSettingsContainer()
        {
            JsonSettingsContainer container = null;
            var jsonText = await GetJson();
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                container = new JsonSettingsContainer() { Settings = new List<KeyValuePair>() };
            }
            else
            {
                try
                {
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText)))
                    {
                        var jsonSerializer = new DataContractJsonSerializer(typeof(JsonSettingsContainer));
                        var obj = jsonSerializer.ReadObject(stream);
                        if (obj != null && obj is JsonSettingsContainer)
                        {
                            container = obj as JsonSettingsContainer;
                        }
                    }
                }
                catch (SerializationException )
                {
                    container = new JsonSettingsContainer() { Settings = new List<KeyValuePair>() };
                }
            }
            return container;
        }

        async Task SaveSettingsContainer(JsonSettingsContainer settings)
        {
            string json = null;
            using (var stream = new MemoryStream())
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(JsonSettingsContainer));
                jsonSerializer.WriteObject(stream, settings);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            await SaveJson(json);
        }
    }

    public class JsonSettingsContainer
    {
        public List<KeyValuePair> Settings { get; set; }
    }

    public class KeyValuePair
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
