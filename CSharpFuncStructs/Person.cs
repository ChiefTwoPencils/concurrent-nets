namespace CSharpFuncStructs
{
    class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
        public Address Address { get; }

        public Person(string firstName, string lastName, int age, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Address = address;
        }

        public Person WithFirstName(string firstName) => 
            new Person(firstName, LastName, Age, Address);

        public Person WithLastName(string lastName) =>
            new Person(FirstName, lastName, Age, Address);

        public Person WithAge(int age) =>
            new Person(FirstName, LastName, age, Address);

        public Person WithAddress(Address address) =>
            new Person(FirstName, LastName, Age, address);
    }

    class Address
    {
        public string Street { get; }
        public string City { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string zipCode)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        public Address WithStreet(string street) =>
            new Address(street, City, ZipCode);

        public Address WithCity(string city) =>
            new Address(Street, city, ZipCode);

        public Address WithZipCode(string zipCode) =>
            new Address(Street, City, zipCode);
    }
}
