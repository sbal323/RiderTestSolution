namespace ConsoleAppCS
{
    public class Employee: Item
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return "Employee to string";
        }
    }
}