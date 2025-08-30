using System.ComponentModel.DataAnnotations;

namespace MeterReadingApi.Domain
{
    public class Account
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        private Account() //for EF core
        {            
        }
        public Account(string firstName, string lastName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
