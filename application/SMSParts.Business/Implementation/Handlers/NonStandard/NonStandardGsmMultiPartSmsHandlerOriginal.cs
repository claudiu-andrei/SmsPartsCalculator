using System.Collections.Generic;
using System.Text;
using SMSParts.Business.Interfaces.Handlers.NonStandard;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Common;
using SMSParts.Domain.Models;

namespace SMSParts.Business.Implementation.Handlers.NonStandard
{
    /// <summary>
    /// Deprecated.
    /// </summary>
    public class NonStandardGsmMultiPartSmsHandlerOriginal : INonStandardGsmMultiPartSmsHandler
    {
        private ICheckTextCharactersService CheckTextCharactersService { get; }
        private readonly int _maxAllowedChars = GlobalVariables.SmsParts.MultipleNonStandardGsm;

        public NonStandardGsmMultiPartSmsHandlerOriginal(
            ICheckTextCharactersService checkTextCharactersService)
        {
            CheckTextCharactersService = checkTextCharactersService;
        }

        public SmsPartsInformationDto GetSmsParts(string text)
        {
            var currentPart = new SmsPart();
            var stringBuilder = new StringBuilder();
            var smsParts = new List<SmsPart>();

            for(var i = 0; i < text.Length; i++)
            {
                var charStr = text[i].ToString();

                if (i < text.Length - 1)
                {
                    var candidateEmoji = charStr + text[i + 1];
                    var isEmoji = CheckTextCharactersService.TextContainsEmoji(candidateEmoji);
                    if (isEmoji)
                    {
                        i++;
                        charStr = candidateEmoji;
                    }
                }

                if (!CharacterFitsInCurrentSmsPart(currentPart, charStr.Length))
                {
                    currentPart.Part = stringBuilder.ToString();
                    smsParts.Add(currentPart);
                    stringBuilder.Clear();
                    currentPart = new SmsPart();
                }

                currentPart.CharacterBytesCount += charStr.Length;
                stringBuilder.Append(charStr);
            }

            if (currentPart.CharacterBytesCount > 0)
            {
                currentPart.Part = stringBuilder.ToString();
                smsParts.Add(currentPart);
            }

            var result = new SmsPartsInformationDto
            {
                HasNonStandardGsmCharacters = true,
                SmsParts = smsParts
            };

            return result;
        }

        private bool CharacterFitsInCurrentSmsPart(SmsPart currentPart, int len)
        {
            return currentPart.CharacterBytesCount + len <= _maxAllowedChars;
        }

        //private bool CharacterFitsInCurrentSmsPart(SmsPart currentPart, string charStr)
        //{
        //    return currentPart.CharacterBytesCount + charStr.Length <= _maxAllowedChars;
        //}
    }
}
