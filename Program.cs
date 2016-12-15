using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LanguageTranslatorConsole
{
    public class LanguageTranslatorService : ILanguageTranslatorService
    {
        private readonly Dictionary<LanguageKey, String> _mappings = new Dictionary<LanguageKey, string>
        {
            { LanguageKey.Afrikaans, "af" },
            { LanguageKey.Arabic, "ar" },
            { LanguageKey.Azerbaijani, "az" },
            { LanguageKey.Bashkir, "ba" },
            { LanguageKey.Belarusian, "be" },
            { LanguageKey.Bulgarian, "bg" },
            { LanguageKey.Bengali, "bn" },
            { LanguageKey.Bosnian, "bs" },
            { LanguageKey.Czech, "cs" },
            { LanguageKey.Chuvash, "cv" },
            { LanguageKey.Danish, "da" },
            { LanguageKey.German, "de" },
            { LanguageKey.Greek, "el" },
            { LanguageKey.English, "en" },
            { LanguageKey.Esperanto, "eo" },
            { LanguageKey.Spanish, "es" },
            { LanguageKey.Estonian, "et" },
            { LanguageKey.Basque, "eu" },
            { LanguageKey.Persian, "fa" },
            { LanguageKey.Finnish, "fi" },
            { LanguageKey.French, "fr" },
            { LanguageKey.Gujarati, "gu" },
            { LanguageKey.Hebrew, "he" },
            { LanguageKey.Hindi, "hi" },
            { LanguageKey.Haitian, "ht" },


        };

        private readonly List<String> _allowableTranslations = new List<string>
        {
            // TODO: How do we handle conversational and patent
            "ar-en",
            "ar-en-conversational", 
            "arz-en",
            "de-en",
            "en-ar",
            "en-ar-conversational",
            "en-arz",
            "en-de",
            "en-es",
            "en-es-conversational",
            "en-fr",
            "en-fr-conversational",
            "en-it",
            "en-pt",
            "en-pt-conversational",
            "es-en",
            "es-en-conversational",
            "es-en-patent",
            "es-fr",
            "fr-en",
            "fr-en-conversational",
            "fr-es",
            "it-en",
            "ko-en-patent",
            "pt-en",
            "pt-en-conversational",
            "pt-en-patent",
            "zh-en-patent"
        }; 

        private const string RELATIVE_ROOT = "language-translator/api/v2/";

        private readonly IWatsonHttpService _watsonHttpService;

        public LanguageTranslatorService()
        {
            _watsonHttpService = new WatsonHttpService();
        }

        public TranslateResponse Translate(LanguageKey source, LanguageKey target, String text)
        {
            return Translate(new TranslateRequest { Source = source, Target = target });
        }

        private TranslateResponse Translate(TranslateRequest translateRequest)
        {
            // make sure it's a valid mapping
            string sourceString = _mappings[translateRequest.Source];
            string targetString = _mappings[translateRequest.Target];

            if (!_allowableTranslations.Contains(sourceString + "-" + targetString))
            {
                throw new ArgumentException("Unable to convert from " + sourceString + " to " + " " + targetString + ". Valid mappings are: ");
            }

            // we know the mapping is valid, let's try to translate
            var apiModel = new TranslateRequestApiModel
            {
                ModelId = String.Empty,
                Source = sourceString,
                Target = targetString,
                Text = translateRequest.Text
            };

            WatsonHttpResponse response = _watsonHttpService.Post(RELATIVE_ROOT + "translate", apiModel);

            if (response.Status == WatsonHttpStatusCode.Ok)
            {
                return JsonConvert.DeserializeObject<TranslateResponse>(response.Result);
            }
            else
            {
                // TODO: There was an error. Throw?
                return new TranslateResponse();
            }
        }
    }

    /// <summary>
    /// Things to do:
    /// 
    /// 1. Overload translate to have either source and target or just model id.
    /// 2. Create enum values that map to strings for source and target languages.
    /// 
    /// Questions:
    /// 1. Do we need to overload translate and allow them using the model id?
    /// 2. Do we need to expose the ability to get a list of model ids?
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var languageTranslatorService = new LanguageTranslatorService();

            TranslateResponse translateResponse = 
                languageTranslatorService.Translate(source: LanguageKey.English, target: LanguageKey.German, text: "Please translate this text");

            Console.WriteLine("Word Count: {0}", translateResponse.WordCount);
            Console.WriteLine("Character Count: {0}", translateResponse.CharacterCount);

            Console.ReadKey();
        }
    }
}