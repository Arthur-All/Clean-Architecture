using ApiPerson.Data;
using ApiPerson.Data.Repository;
using ApiPerson.Interface;
using ApiPerson.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPerson.Services
{
    public class PersonService : IPersonServices
    {
        private readonly Repository _repo;
        public PersonService(Repository repository)
        {
            _repo = repository;
        }
        
        public async Task<List<Person>> FindAllPerson()
        {
            var personList = await _repo.FindAllPerson();
            return personList;
        }
       /* public async Task<Person> FindById(int Id)
        {
            var person = await _repo.FindById(Id);
            return person;
        }*/

       public async Task<int?> CreatePerson(Person personModel)
       {
            var create = await _repo.CreatePerson(personModel);
            return create;
       }
       public async Task<int> UpdatePerson(Person personModel)
       {
            var edit = await _repo.UpdatePerson(personModel); //resultado do meu repositorio
            return edit;
       }
       public async Task<int> DeletePerson(int Id)
       {
            var delete = await _repo.DeletePerson(Id);
            return delete;
       }



    }


}
