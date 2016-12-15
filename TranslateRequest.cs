using System;
using Newtonsoft.Json;

namespace LanguageTranslatorConsole
{
    public class TranslateRequest // do we even need this model? we are passing the source target and text directly then we just need the model below
    {
        public String ModelId { get; set; }

        public LanguageKey Source { get; set; }

        public LanguageKey Target { get; set; }

        public String Text { get; set; }
    }

    public class TranslateRequestApiModel
    {
        [JsonProperty(PropertyName = "model_id")]
        public String ModelId { get; set; }

        [JsonProperty(PropertyName = "source")]
        public String Source { get; set; }

        [JsonProperty(PropertyName = "target")]
        public String Target { get; set; }

        [JsonProperty(PropertyName = "text")]
        public String Text { get; set; }
    }
}