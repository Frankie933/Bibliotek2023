using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek2023
{
    internal class Menu
    {
        //referens till Library objektet som använd för att utföra operationer på bibliotek.
        private Library library;

        public Menu(Library library)  // konstruktorn tar emot en instan av ibrary-klassen.
        {
            this.library = library;  // tilldelar den inkommande library instansen till den privata medlemsvariabeln.
        }   

        
        public void Show() // visar huvudmeny och hanterar användarinput
        {
            bool showMenu = true; // variabel för att bestäma om meny ska visas eller ej 

            while (showMenu)  // while meny som håller meny aktiv så länge showMenu är true

            {
                Console.Clear(); //rensar fönstret för att visa en ren meny

                //skriver ut välkomstdedelande och menyval
                Console.WriteLine("Välkommen till bibliotek!");
                Console.WriteLine("1. Lägga till ny bok");
                Console.WriteLine("2. Visa Tillgängliga böcker");
                Console.WriteLine("3. Låna en bok");
                Console.WriteLine("4. Återlämna en bok");
                Console.WriteLine("5. Visa alla tillgängliga böcker och alla utlånade Böcker");
                Console.WriteLine("6. Visa lånade böcker");
                Console.WriteLine("7. Avsluta");
                Console.WriteLine("vänligen välj en åtgärd: ");

                

                string val = Console.ReadLine();  //Läser användarens val från konsolen.


                switch (val) //En switch-sats för att hantera olika användarval. 
                {


                    case "1": // lägger till en ny bok
                        Console.WriteLine("Ange boktitel: ");
                        string title = Console.ReadLine();  // läser inmatning och sparar den som bokitel
                        Console.WriteLine("Ange författare: ");
                        string author = Console.ReadLine(); //läser inmatning och sparar den som författare
                        Book newBook = new Book(library.GetallBooks().Count + 1,title,author);  //skapar den ny bok med ett id baserat på antalet befintliga böcker, samt användaren inmatade titel och författre. 
                        library.AddBook(newBook); //Lägger till den nya boken i biblioteket.
                        Console.WriteLine($"{newBook.Title} har lagts till i bibliotek."); //skriver ut meddelande om att boken har lagts till. 
                        break;

                    case "2":
                        // Visa tillgängliga böcker.
                        var avilableBooks = library.GetAvilablebooks(); // hämtar en lista med tillgängliga böcker från bibliotekt. 
                        Console.WriteLine("tillgängliga böcker: "); 
                        foreach (var book in avilableBooks) //Loppar genom varje tillgänglig book.
                        {
                            Console.WriteLine($"ID:{book.BookId},Title: {book.Title},Författare:{book.Author},"); //Skriver ut information om varje tillgänglig bok. 

                        }
                        Console.WriteLine("Tryck på valfri Tanget för att forsätta..."); //uppmanar användaren att trycka på tangent för att försätta 
                        Console.ReadKey(); // väntar på användaren ska trycka på en tangent. 
                        break;

                        case "3":  //Låna en bok från biblioteket
                        var availableBooks = library.GetAvilablebooks(); //hämtar en lista med tillgängliga böcker från biblioteket. 
                        Console.WriteLine("Tillgängliga böcker:"); //Skriver ut en rubrik för tillgängliga böcker. 
                        foreach (var book in availableBooks)  //Loopar genom varje tillgängliga bok.
                        {
                            Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, Författare: {book.Author}"); //skriver ut information om varje tillgängliga bok. 
                        }

                        Console.WriteLine("Ange bok-ID för att låna: "); //uppmanar användaren att ange Bok-ID för att låna
                        int borrowBookId;
                        if (int.TryParse(Console.ReadLine(), out borrowBookId)) //Läser inmatningen som bok ID och försöker konvertera den till ett heltal.
                        {
                            Console.WriteLine("Ange Låntagarens Namn: ");  //be om användaren namn
                            string borrowerName = Console.ReadLine(); //läser och sparar namn.... 
                            Console.WriteLine("Ange Låntagarens Personnummer: "); // personumer anges
                            string borrowerId = Console.ReadLine(); //läser inmatningen och sparar den som låntagaren personummer. 


                            library.BorrowBook(borrowBookId, borrowerName, borrowerId); //lånar boken genom att anropa metoden i biblioteket. 
                        }             
                        Console.WriteLine("Tryck på valfri tangent för att fortsätta..."); 
                        Console.ReadKey();
                        break;

                        case "4": //Återlämna en bok till biblioteket.
                        Console.WriteLine("Ange Bok-ID för att returnera: "); //uppmanar användaren att ange id för boken för att returnera
                        int returnBookId;
                        if(int.TryParse(Console.ReadLine(), out returnBookId)) //Läser inmantningen som BokID  och försöker konverera den till heltal. 
                        {
                            library.Returbook(returnBookId); //Returnerar boken genom att andropa metoden i biblioteket.
                        }
                        else
                        {
                            Console.WriteLine("Ogiltligt ID, Försök igen");  //meddelande om ogiltigt id om inmatningen inte är helttal. 

                        }
                        Console.WriteLine("Tryck på valfri Tanget för att forsätta...");
                        Console.ReadKey();
                        break;

                        case "5":
                        var allbooks = library.GetallBooks(); //visa alla böcker i biblioteket
                        Console.WriteLine("Alla böcker: "); //skriver ut en rubrik för alla böcker. 
                        foreach (var book in allbooks) // loopar genom varje bok i biblioteket. 
                        {
                            string borrowerInfo = book.IsBorrowed ? $" (lånad av: {book.BorrowerName},  Personnummer: {book.BorrowerId})":""; //skapar en sträng med information om låntagaren om boken är utlånad. 
                            Console.WriteLine($" ID: {book.BookId}, Title: {book.Title} Författare: {book.Author}{borrowerInfo}");   //Skriver ut information om varje bok. 

                        }
                        Console.WriteLine("Tryck på valfri Tanget för att forsätta...");
                        Console.ReadKey();
                        break;

                        case "6": //visa alla lånade böcker
                         var borrowBooks = library.GetBorrowedBooks(); //Hämtar en lista med alla lånade bölcker 

                        if(borrowBooks.Count == 0 ) //kontrollerar om det inte finns några lånade böcker
                        {
                            Console.WriteLine("Det finns inga böcker som är lånade.");

                        }
                        else
                        {
                            Console.WriteLine("Lånade böcker: ");
                            foreach (var book in borrowBooks)  // Loopar genom varje lånad bok.
                            {
                               Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, Författare: {book.Author}, Lånad av: {book.BorrowerName}, (Personnummer:{book.BorrowerId}))");
                                //skriver ut information om varjelånad bok och deras låntagare
                            }
                        }
                        Console.WriteLine("Tryck på valfri tangent för att fortsätta");
                        Console.ReadKey();
                        break;

                    case "7":
                        //Avsluta programmet.
                        Environment.Exit(0); //Avslutar programmet.
                        break;

                    default:
                        //fellmeddelande för ogiltigt val.
                        Console.WriteLine("ogiltligt val. försök igen. "); // Meddelande om ogiltigt användarval.
                        break;





                }

            }
        }



    }
}
