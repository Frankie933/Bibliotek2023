using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek2023
{
     class Book   // skapa en klass med namnet "Book"
     {
        private static int NextBookId = 1; // deklarera en privat statisk variabel för att hålla reda på nästa tillgängliga BOK-ID

        public int BookId { get; } // deklarera en offentlig egenskap (property) för att hämta bokens ID:

        public string Title { get; private set;} // Deklarera en offentlig egenskap för boken titel med privat skrivåtkomst.
        public string Author { get; private set;} // deklarera en offentlig egenskap för bokens författare med privat skrivåtkomst,
        public bool IsBorrowed { get; internal set;} //Deklarera en offentlig egenska för att ange om boken är utlånad eller inte med intern skrivåtkomst.
        public string BorrowerName { get; internal set;} //Deklarera en offentlig egenskapo för låntagarens ID med intern skriv åtkomst.
        public string BorrowerId { get; internal set;} //Deklarera en offentlig egenskap för lånetagarens ID med intern Skrivåtkomst.

       
         // skapa en konstruktor för klassen book som tar emot bokens ID, titel och författare som parameterar. 
        public Book (int bookId, string title, string author)

        {
            
            BookId= NextBookId++; // Tilldela det nästa tillgängliga bokID till den aktuella boken och öka sedan värdet för nästa tillgänglga ID. 
            Title = title;  // Tilldela den angivna titeln till boken. 
            Author = author; // Tilldela den angivna författaren till boken. 
            IsBorrowed = false; // Markera boken som inte utlånad när den skapas. 
            BorrowerName = null; // Ange låntagaren namn till null eftersom boken inte är utlånad än.
            BorrowerId = null; // Ange låntagarens ID till null eftersom boken inte är utlånad än.


        }

        // skapa en metod för att låna boken. 
        public void BorrowBook(string borrowerName, string borrowerId) 
        {

            if (int.TryParse(borrowerId, out _)) // Kontrollera om låntagarens ID kan konverteras till en heltalsvärde. 
            {

                if (!IsBorrowed) // Om boken inte utlånad.
                {
                    IsBorrowed = true; // Markera boken som utlånad.
                    BorrowerName = borrowerName; // sätt låntagarens namn.
                    BorrowerId = borrowerId; // Sätt låntagarens ID.
                }

            }
           
        }

        // skapa en metod för att returnera boken.
        public void ReturnBook()
        {
            if (IsBorrowed) //om boken är utlånad
            {
                IsBorrowed= false; // Markera boken som inte utlånad längre.
                BorrowerName = null; // Återställ låntagarens namn till null.
                BorrowerId = null; 
            }

        }


     }
}
