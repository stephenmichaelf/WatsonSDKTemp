using System;

namespace LanguageTranslatorConsole
{
    public interface ILanguageTranslatorService
    {
        //TranslateResponse Translate(String modelId, String text);

        TranslateResponse Translate(LanguageKey source, LanguageKey target, String text);
    }
}