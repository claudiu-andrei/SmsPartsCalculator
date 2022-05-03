namespace SMSParts.Business.Interfaces.Helpers
{
    public interface ICheckTextCharactersService
    {
        bool TextContainsEmoji(string text);
        bool TextContainsNonStandardOrExtendedGsmCharacters(string text);
    }
}
