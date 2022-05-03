namespace SMSParts.Business.Interfaces.Helpers
{
    public interface IGsmCharactersBytesHelper
    {
        int GetTextBytesNeeded(string text);

        int GetCharacterBytesNeeded(char chr);
    }
}
