namespace Teapot.Web.Tests
{
    public readonly struct ExtendedHttpStatusCode
    {
        public ExtendedHttpStatusCode(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; }
        public string Message { get; }
        public override string ToString() => $"{Code} {Message}";
    }
}