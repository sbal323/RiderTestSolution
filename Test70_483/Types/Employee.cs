namespace Test70_483.Types
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public override string ToString() => $"Id={Id}\nFull Name={FullName}\n";
    }
}