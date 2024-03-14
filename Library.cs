using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek2023
{
    internal class Library
    {
        private List<Book> books = new List<Book>();  // Skapar en privat lista som innehåller objekt av typen book.
        private string filePath = "Library_data_txt"; // anger en filväg för att spara biblioteksdata. 



        public Library() 
        {

            LoadData(); //kallar på load-metoden vid skapande av ett library-objekt för att ladda befintliga biblioteksdata från filen.
        }

        private void SaveData() //sparar biblioteksdata till filen. 

        { 
            using (StreamWriter writer = new StreamWriter(filePath)) //öppnar en strewamwriter för att skriva till filen. 
            {
                foreach (Book book in books) //itererar genom varje bok i listan av böcker. 
                {
                    writer.WriteLine($"{book.BookId},{book.Title},{book.Author},{book.IsBorrowed},{book.BorrowerName}, {book.BorrowerId}"); //skriver bokens information till filen. 
                }
            }
        }

        public void LoadData() //läser biblioteksdata från filen och fyller listan med böcker. 
        {
            if(File.Exists(filePath)) // kontrollerar om filen existerar
            {
                using (StreamReader reader = new StreamReader(filePath)) // öppnar en streamreader för läsa från filen
                {
                    string line;

                    while ((line = reader.ReadLine()) != null) // läser varje rad i filen tills inget mer data finns att läsa. 

                    {

                        
                        string[] data = line.Split(','); //delar upp raden i delar baserat på kommatecken.

                        if (data.Length >= 6) //kontrollerar om det finns tillräckligt med delar för att skapa en bok. 
                        {


                            int bookId = int.Parse(data[0]); //konverterar bokens ID från sträng till heltal. 
                            string title = data[1]; //hämtar bokens titel.
                            string author = data[2]; //hämtar bokens författare.    
                            bool isBorrowed = bool.Parse(data[3]); //konverterar information om boken är utlånad från sträng till bool.
                            string borrowerName = data[4]; // hämtar namnet på lånetagaren. 
                            string borrowerId = data[5];  // hämtar låntagrens ID

                            Book book = new Book(bookId, title, author)  // skapar en ny bok med de angivna detaljerna. 
                            {
                                IsBorrowed = isBorrowed,
                                BorrowerName = borrowerName,
                                BorrowerId = borrowerId
                            };
                            books.Add(book); //lägger till den skapade boken i listan av böcker. 

                        }
                        else
                        {

                            Console.WriteLine("ogiltig data i filen: " + line);  //skriver ut ett felmeddelande om data i filen inte är giltig. 
                        }

                        
                    }
                }
            }
        }
        public void AddBook (Book book)   //metod för att lägga till en bok i biblioteket och spara data till filen
        {
            books.Add(book); // lägger till den angivna boken i listan av böcker. 
            SaveData(); // sparar den uppdaterade listan av böcker till filen. 

        }
        public List <Book> GetAvilablebooks()   // Metod för att hämta tillgängliga böcker från bibliotek.
        {
            List<Book> avilableBooks = new List<Book>();

            foreach (Book book in books)
            {
                if (!book.IsBorrowed)
                {
                    avilableBooks.Add(book); // lägg till listan av tillgänliga böcker.

                }
            }
            return avilableBooks; //retunerar listan av tillgängliga böcker. 

        }

        public void BorrowBook (int bookId, string borrowerName, string BorrowerId) //Metod för att låna en bok från bibliotek baserat på bookens ID och låntagrens information.
        {

            bool isValidId = int.TryParse(BorrowerId, out _); // konttrolerar om brrowerId är ett numriskt värde.

            if (!isValidId)
            {
                Console.WriteLine("ogiltigt personnummer. Endast numrisk värde."); // meddelande om ogiltig personnummer.
                return; // avslutar metoden om personnumret inte är giltigt för att undvika fortsatt exekvering av kod. 
            }

            Book bookToBorrow = books.Find(book => book.BookId == bookId);  //hittar boken med angivna id:T i listan av böcker.

            if(bookToBorrow != null && !bookToBorrow.IsBorrowed)  //om bokens hittas och inte ä redan utlånad
            {
                bookToBorrow.BorrowBook(borrowerName, BorrowerId); //utför lån av boken. 
                SaveData();  //sparar den uppdaterade bokinformation till filen. 

                Console.WriteLine($"{bookToBorrow.Title} har lånats av {borrowerName} (Personnummer:{BorrowerId}");  //skriver ut meddelande om att boken har lånats. 
            }
            else if (bookToBorrow != null && bookToBorrow.IsBorrowed) // om boken hittad men är redan utlånad. 
            {
                Console.WriteLine($"{bookToBorrow.Title} är redan utlånad av {bookToBorrow.BorrowerName} (Personnummer: {bookToBorrow.BorrowerId})."); //skriver ut meddelande om att boken är utlånad
            }
            else // om boken inte hittas. 
            {
                Console.WriteLine("Boken finns inte eller är redan Lånad"); //skriver ut meddelande om att boken inte finns eller redan utlånad. 
            }

          
        }
        
        public void Returbook(int bookId) //metod för att retunera en bok baserat på bokens id
        {
            Book bookToReturn = books.Find(book => book.BookId== bookId);  //hittar boken med det angivna ID.t i listan av böcker. 

            if(bookToReturn !=null && bookToReturn.IsBorrowed)  //om boken hittas och är utlånad.
            {
                bookToReturn.ReturnBook(); //utfär återlämning av boken. 
                SaveData(); //sparar den uppdaterade bokinformation till filen. 
                Console.WriteLine($"{bookToReturn.Title} har återlämnats.");  //skriver ut meddelanden om att boken har återlämnats. 

            }
            else // om boken inte hittas eller inte är utlånad. 
            {
                Console.WriteLine("boken har inte hittats eller utlånad."); // skrivver ut meedelandde om att boken inte hittas eller inte ät utlånad.
            }
        }

        public List <Book> GetBorrowedBooks() //metod för att hämta en lista av utlånde böcker från biblioteket. 
        {
            List <Book> borrowbooks= new List<Book>();
            foreach (Book book in books)
            {
                if(book.IsBorrowed)
                {
                    borrowbooks.Add(book);  //lägger till utlånde böcker i listan utlånade böcker. 

                }
            }
            return borrowbooks; //returnerar listan av utlånade böcker. 
        }
        public List <Book> GetallBooks() //metod för att hämta en lista av alla böcker i biblioteket. 
        {
            return books; //returnerar hela listan av böcker i biblioteket. 
        }
    }
}
