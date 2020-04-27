using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

namespace TranslatorAPI
{
    public class Translator
    {
        private string APIKey = "trnsl.1.1.20200421T094203Z.8f9595c509cb9fc9.0b95698bf5cc031b04cc33a1099595b862c1af1a";
        public Translator(string APIKey)
        {
            this.APIKey = APIKey;
        }
        public Translator()
        {
        }
        public Languages SupportedLanguages()
        {
            // Получить список поддерживаемых языков
            // Создание текста запроса
            string language = $"ru";
            string dataRequest = $"https://translate.yandex.net/api/v1.5/tr.json/getLangs?ui={language}&key={APIKey}";

            // Отправка запроса к Web API
            WebRequest request = WebRequest.Create(dataRequest);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();

            // Статус запроса: удачен, неудачен
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            if (((HttpWebResponse)response).StatusDescription != "OK")
            {
                return null;
            }

            // Чтение запроса: распаковка JSON
            string dataJson;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                dataJson = reader.ReadToEnd();
            }
            Languages languages = JsonSerializer.Deserialize<Languages>(dataJson);

            // Закрытие соединений
            response.Close();
            return languages;
        }
        public Translation Translate(string text, string lang, string toLang)
        {
            // Создание текста запроса
            string dataRequest = $"https://translate.yandex.net/api/v1.5/tr.json/translate?lang={lang}-{toLang}&key={APIKey}&text={text}";

            // Отправка запроса к Web API
            WebRequest request = WebRequest.Create(dataRequest);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();

            // Статус запроса: удачен, неудачен
            if (((HttpWebResponse)response).StatusDescription != "OK")
            {
                return null;
            }

            // Чтение запроса: распаковка JSON
            string dataJson;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                dataJson = reader.ReadToEnd();
            }
            Translation translation = JsonSerializer.Deserialize<Translation>(dataJson);

            // Закрытие соединений
            response.Close();
            return translation;
        }
    }

    public class Languages
    {
        public string[] dirs { get; set; }
        public Dictionary<string, string> langs { get; set; }
    }

    public class Translation
    {
        public int code { get; set; }
        public string lang { get; set; }
        public string[] text { get; set; }
    }
}
