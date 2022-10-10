using ApiPerson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPerson.Interface
{
    public interface IPersonServices
    {
       // Person FindById(int Id);
        Task<List<Person>> FindAllPerson();
        Task<int?> CreatePerson(Person personModel);
        Task<int> UpdatePerson(Person personModel);
        Task<int> DeletePerson(int Id);

    }
}