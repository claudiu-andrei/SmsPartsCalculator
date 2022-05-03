namespace SMSParts.Domain.Common
{
    public static class Enums
    {
        public enum SmsTextType
        {
            Unknown = 0,
            StandardGsmSinglePartMessage = 1,
            StandardGsmMultiPartMessage = 2,
            NonStandardGsmSinglePartMessage = 3,
            NonStandardGsmMultiPartMessage = 4
        }
    }
}
