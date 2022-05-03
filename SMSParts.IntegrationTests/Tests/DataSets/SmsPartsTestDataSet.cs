using System.Collections;
using System.Collections.Generic;
using SMSParts.IntegrationTests.Models;

namespace SMSParts.IntegrationTests.Tests.DataSets
{
    public class SmsPartsTestDataSet : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            #region Standard characters data 
            yield return new object[]
            {
                new string('a', 159) + 'Q',
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = false,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a',159) + 'Q',
                            CharacterBytesCount = 160
                        }
                    }
                },
                "simple sms with 160 characters"
            };

            yield return new object[]
            {
                new string('^', 80),
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = false,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('^', 80),
                            CharacterBytesCount = 160
                        }
                    }
                },
                "simple sms with 80 2byte characters"
            };

            yield return new object[]
            {
                new string('a', 161),
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = false,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 153),
                            CharacterBytesCount = 153
                        },
                        new()
                        {
                            Part = new string('a', 8),
                            CharacterBytesCount = 8
                        }
                    }
                },
                "multi part sms with 161 characters (153 + 8)"
            };

            yield return new object[]
            {
                new string('a', 153*2),
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = false,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 153),
                            CharacterBytesCount = 153
                        },
                        new()
                        {
                            Part = new string('a', 153),
                            CharacterBytesCount = 153
                        }
                    }
                },
                "multi part sms with 2*153 characters (153 + 153)"
            };

            yield return new object[]
            {
                new string('a', 153*2+1),
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = false,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 153),
                            CharacterBytesCount = 153
                        },
                        new()
                        {
                            Part = new string('a', 153),
                            CharacterBytesCount = 153
                        },
                        new()
                        {
                            Part = new string('a', 1),
                            CharacterBytesCount = 1
                        }
                    }
                },
                "multi part sms with 2*153 characters (153 + 8)"
            };

            yield return new object[]
            {
                new string('a', 158) + '^',
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = false,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 158) + '^',
                            CharacterBytesCount = 160
                        }
                    }
                },
                "simple sms with 158 characters + 1 2byte character (160 total)"
            };

            yield return new object[]
            {
                new string('a', 159) + '^',
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = false,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 153),
                            CharacterBytesCount = 153
                        },
                        new()
                        {
                            Part = new string('a', 6) + '^',
                            CharacterBytesCount = 8
                        }
                    }
                },
                "multi part sms with 159 characters + 1 2byte character (153 + 6 + '^')"
            };

            yield return new object[]
            {
                new string('a', 152) + '^' +  new string('a', 152),
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = false,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 152),
                            CharacterBytesCount = 152
                        },
                        new()
                        {
                            Part = '^' + new string('a', 151) ,
                            CharacterBytesCount = 153
                        },
                        new()
                        {
                            Part = new string('a', 1) ,
                            CharacterBytesCount = 1
                        }
                    }
                },
                "multi part sms with 3 messages: (152 due to 2 byte not fitting + 153 + 1)"
            };
            #endregion

            #region NonStandard characters data
            yield return new object[]
            {
                new string('a', 1) + 'ń',
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = true,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 1) + 'ń',
                            CharacterBytesCount = 2
                        }
                    }
                },
                "non standard sms when non standard character ń' in text"
            };

            yield return new object[]
            {
                new string('a', 66) + "😛" + new string('a', 2),
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = true,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 66) + "😛" + new string('a', 2),
                            CharacterBytesCount = 70
                        }
                    }
                },
                "non standard sms 70 characters"
            };

            yield return new object[]
            {
                new string('a', 66) + "😛" + new string('a', 3),
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = true,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 66) + '�',// '\ud83d',
                            CharacterBytesCount = 67
                        },
                        new()
                        {
                            Part = /*'\ude1b' */ '�' + new string('a', 3),
                            CharacterBytesCount = 4
                        }
                    }
                },
                "non standard multi part sms with split emoji (67 + 4)"
            };


            yield return new object[]
            {
                new string('a', 66) + "😛" + new string('a', 64) + "🍕",
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = true,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 66) + '�',// '\ud83d',
                            CharacterBytesCount = 67
                        },
                        new()
                        {
                            Part = /*'\ude1b' */ '�' + new string('a', 64) + "🍕",
                            CharacterBytesCount = 67
                        }
                    }
                },
                "non standard multi part sms split and non split emoji (67 + 67)"
            };

            yield return new object[]
            {
                new string('a', 132) + "😛"  + 'ń',
                new SmsPartsInformationDto
                {
                    HasNonStandardGsmCharacters = true,
                    SmsParts = new List<SmsPart>
                    {
                        new()
                        {
                            Part = new string('a', 67),
                            CharacterBytesCount = 67
                        },
                        new()
                        {
                            Part = new string('a', 65) +  "😛",
                            CharacterBytesCount = 67
                        },
                        new()
                        {
                            Part = new string('ń', 1),
                            CharacterBytesCount = 1
                        }
                    }
                },
                "non standard multi part sms split with end emoji and non standard character(67+67+1)"
            };

            #endregion
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
