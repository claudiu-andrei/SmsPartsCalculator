namespace SMSParts.IntegrationTests.Models
{
    public class SmsPart
    {
        public string Part { get; set; }
        public int CharacterBytesCount { get; set; }
        public int CharacterCount => Part.Length;
    }
}
