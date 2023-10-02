using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [InverseProperty("Sender")]
        public List<Message> SentMessages { get; set; }
        [InverseProperty("Receiver")]
        public List<Message> ReceivedMessages { get; set; }
    }
}

// * We use this one when we have two relationships to the same class.