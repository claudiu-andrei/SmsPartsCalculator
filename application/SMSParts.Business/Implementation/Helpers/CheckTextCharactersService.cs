using System.Linq;
using System.Text.RegularExpressions;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Common;

namespace SMSParts.Business.Implementation.Helpers
{
    public class CheckTextCharactersService : ICheckTextCharactersService
    {
        public bool TextContainsEmoji(string text)
        {
            var regex = new Regex(GlobalVariables.Patterns.EmojiPattern);
            var hasEmoji = regex.Match(text).Success;
            return hasEmoji;
        }

        public bool TextContainsNonStandardOrExtendedGsmCharacters(string text)
        {
            var foundNonGsmCharacters = text
                .Any(chr => !GlobalVariables.GsmCharacters.GsmCharacterBytesDict
                    .ContainsKey(chr));
            return foundNonGsmCharacters;
        }
    }
}
