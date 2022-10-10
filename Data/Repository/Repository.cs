using ApiPerson.Data;
using ApiPerson.Interface;
using ApiPerson.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPerson.Data.Repository
{
    public class Repository : IPersonServices
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        public Repository(IConfiguration config, AppDbContext context)
        {
            _context = context;
            _config = config;
        }
       public async Task<List<Person>> FindAllPerson()
       {
            try {
                using (var connection = new SqlConnection(_config.GetConnectionString("SQLConnectionString")))
                {
                    var select = "select * from Persons";
                    var persons = (await connection.QueryAsync<Person>(select)).ToList();
                    return persons;
                }
            }
            catch(Exception ex)
            {
                 var x= "";
                return null;
            }
       }
        public async Task<int?>CreatePerson(Person personModel)
        {
            using(var connection = new SqlConnection(_config.GetConnectionString("SQLConnectionString")))
            {
                var parameters = new { Email = personModel.Email };
                var select = "select * from Persons where Email = @Email";
                var findPerson = await connection.QueryFirstOrDefaultAsync<Person>(select, parameters);
                if (findPerson == null) //Meu validador, não posso receber qualquer coisa
                {
                    Person person = new Person();
                    person.Name = personModel.Name;
                    person.Email = personModel.Email;
                    person.Password = personModel.Password;
                    await _context.AddAsync(person);
                    await _context.SaveChangesAsync();
                    return person.Id;
                }
                return null;
            }  
        }
        public async Task<int> UpdatePerson( Person personModel)
        {
            using(var connection = new SqlConnection(_config.GetConnectionString("SQLConnectionString")))
            {
                connection.Open();
                var parameters = new { Id = personModel.Id };
                var select = "select*from Persons where Id = @Id";
                var findPerson = await connection.QueryFirstAsync<Person>(select, parameters);
                if(findPerson.GetType()!= typeof(Person))//Meu validador, não posso editar algo que n existe
                {
                    return 0;
                }
                findPerson.Name = personModel.Name;
                findPerson.Id = personModel.Id;
                findPerson.Email = personModel.Email;
                _context.Persons.Update(findPerson);
                return await _context.SaveChangesAsync();
            }
        }
        public async Task<int> DeletePerson(int Id)
        {
            using(var connection = new SqlConnection(_config.GetConnectionString("SQLConnectionString")))
            {
                connection.Open();
                var parameters = new { Id };
                var select = "select * from Persons where Id = @Id";
                var findPerson = await connection.QueryFirstAsync<Person>(select, parameters);
                if(findPerson.GetType() != typeof(Person))
                {
                    return 0;
                }
                _context.Remove(findPerson);
                return await _context.SaveChangesAsync();
            }
        } 
    }
}
