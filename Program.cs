using System;

namespace LanguageTranslatorConsole
{
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