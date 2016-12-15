using Newtonsoft.Json;

namespace LanguageTranslatorConsole
{
    public class Translation
    {
        [JsonProperty(PropertyName = "translation")]
        public string TranslationText { get; set; }
    }
}