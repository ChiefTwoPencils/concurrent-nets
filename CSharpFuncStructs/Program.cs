namespace CSharpFuncStructs
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = new Address("123 First St.", "Sacmo", "95814");
            var person = new Person("Tom", "Jones", 42, address);
            // person.FirstName = ... => ERROR!
            person = person.WithFirstName("Harry");
            person = person
                .WithFirstName("Joey")
                .WithLastName("Johnson")
                .WithAge(69);
        }
    }
}
