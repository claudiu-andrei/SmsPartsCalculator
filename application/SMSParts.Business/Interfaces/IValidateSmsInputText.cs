namespace SMSParts.Business.Interfaces
{
    public interface IValidateSmsInputText
    {
        string Validate(bool hasNonStandardCharacters, int totalCharactersNeeded);
    }
}
