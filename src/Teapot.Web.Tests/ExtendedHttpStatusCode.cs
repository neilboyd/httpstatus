namespace Teapot.Web.Tests
{
    public readonly struct ExtendedHttpStatusCode
    {
        public ExtendedHttpStatusCode(int code, string description)
        {
            Code = code;
            Description = description;
        }
        public int Code { get; }
        public string Description { get; }
        public override string ToString() => $"{Code} {Description}";
    }
}