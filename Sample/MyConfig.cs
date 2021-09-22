using Aksio.Microservices.Configuration;

#nullable disable

namespace Sample
{
    [Configuration("my-config")]
    public class MyConfig
    {
        public string SomeString { get; init; }

        public int SomeInteger { get; init; }
    }
}