using System.Collections.Generic;
using Newtonsoft.Json;

namespace LanguageTranslatorConsole
{
    public class TranslateResponse
    {
        [JsonProperty(PropertyName = "word_count")]
        public int WordCount { get; set; }

        [JsonProperty(PropertyName = "character_count")]
        public int CharacterCount { get; set; }

        [JsonProperty(PropertyName = "translations")]
        public List<Translation> Translations { get; set; }
    }
}