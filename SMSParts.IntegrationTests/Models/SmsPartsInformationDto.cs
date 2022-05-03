using System.Collections.Generic;

namespace SMSParts.IntegrationTests.Models
{
    public class SmsPartsInformationDto
    {
        public int SmsPartsCount => SmsParts.Count;

        public bool HasNonStandardGsmCharacters { get; set; }

        public List<SmsPart> SmsParts { get; set; }
    }
}
