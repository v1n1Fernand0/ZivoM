namespace ZivoM.Helpers
{
    public class Credential
    {
        public string Type { get; set; } = "password";
        public string Value { get; set; }
        public bool Temporary { get; set; } = false;
    }
}
