using System.Collections.Generic;
using System.Linq;
using SMSParts.Business.Interfaces.Helpers;
using static SMSParts.Domain.Common.GlobalVariables;

namespace SMSParts.Business.Implementation.Helpers
{
    public class GsmCharactersBytesHelper : IGsmCharactersBytesHelper
    {

        public int GetTextBytesNeeded(string text)
        {
            var textChars = text.ToCharArray();

            return GetTextBytesNeeded(textChars);
        }

        private int GetTextBytesNeeded(IEnumerable<char> textChars)
        {
            return textChars.Sum(GetCharacterBytesNeeded);
        }

        public int GetCharacterBytesNeeded(char chr)
        {
            if (GsmCharacters.GsmCharacterBytesDict.ContainsKey(chr))
            {
                return GsmCharacters.GsmCharacterBytesDict[chr];
            }

            // unknown characters will be treated as such
            return 1;
        }

    }
}
