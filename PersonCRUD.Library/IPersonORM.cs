using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonCRUD.Library
{
    public interface IPersonORM
    {
        void PersonCreate(string personFirstName, string personLastName, string personEmail, string personPhone);
        void PersonUpdate(int personID, string personFirstName, string personLastName, string personEmail, string personPhone);
        void PersonDelete(int personID);
        List<Person> ReadAllPeople();
        List<Person> SearchPersonFirstName(string personFirstName);
    }
}
