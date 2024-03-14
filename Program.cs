namespace Bibliotek2023
{
    class Program
    {
        static void Main(string[] args)
        {
            // skapar en instan av Librarey klassen.
           Library library = new Library();

            Menu menu = new Menu(library); // Skapar en instan av menu klassen och skickar med bibliotek som parameter
            menu.Show();  // visar menyn för användaren



        }
    }
}