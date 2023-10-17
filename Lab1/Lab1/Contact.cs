public class Contact
{
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }
    public string Address { get; }

    public Contact(string firstName, string lastName, string phoneNumber, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Address = address;
    }
}