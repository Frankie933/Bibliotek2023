using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek2023
{

    //Person- Klassen representera en grundläggande person med ett namn och ett id.
     class Person
     {
        public string Name { get; private set; }
        public string id { get; private set; }

        //konstruktorn tar emot namn och iod och ersätter egenskaper
        public Person (string name, string id)
        {
            Name = name;
            this.id = id;
        }

     }
    // Borrower klassen ärver från person och representerar en person som kan låna böcker.
     class Borrower : Person
     {

        //en privat lista som lagrar de böcker som låntagaren har lånat.
        private List <Book> borrowedBooks = new List <Book> ();

        //konstruktorn för brrower tar emot namn och ID och andropar sedan bas- klassen konstruktor. 
        public Borrower(string name, string id) :base (name, id)

        {
        }



       


     }

    
}
