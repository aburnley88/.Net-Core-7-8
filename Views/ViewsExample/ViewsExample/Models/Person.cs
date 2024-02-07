namespace ViewsExample.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
    public enum Gender { Male, Female, TransMan, TransWoman, NonBinary }
}
