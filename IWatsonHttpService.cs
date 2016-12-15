namespace LanguageTranslatorConsole
{
    public interface IWatsonHttpService
    {
        WatsonHttpResponse Post(string path, object data);
    }
}