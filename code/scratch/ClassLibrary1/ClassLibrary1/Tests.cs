using Xunit;

namespace ClassLibrary1
{
    public class Tests
    {
        [Fact]
        public void SimpleTest()
        {
            var jsonCreate = "{ \"Name\": \"Max\", \"Address\": \"Berlin\" }";
            var jsonAddressChanged = "{ \"Address\": \"New York\" }";

            // I want to call convert method which converts to the correct event

            // DomainEvent e = MyConverter.Convert(...)
        }
    }

    // Just an idea
    public class MyConverter
    {
        public T Convert<T>(dynamic source) where T : class // or DomainEvent
        {
            // TODO
            return null;
        }
    }
}