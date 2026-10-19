namespace Rece_Catalin_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        // Proprietate de navigare pentru relația cu Cărțile
        public ICollection<Book>? Books { get; set; }
    }
}