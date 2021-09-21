using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository
{
    public class PersonRepositoryOld : IPersonRepository
    {
        private readonly MyContext myContext;
        public PersonRepositoryOld(MyContext myContext) { 
            this.myContext = myContext;
        }
        public int Delete(string NIK)
        {
            var delete = myContext.Persons.Find(NIK);
            if (delete == null) {
                throw new ArgumentNullException();
            }
            myContext.Persons.Remove(delete);
            var deleted = myContext.SaveChanges();
            return deleted;
        }

        public IEnumerable<Person> Get()
        {
            //throw new NotImplementedException();
            return myContext.Persons.ToList();
        }

        public Person Get(string NIK)
        {
            //throw new NotImplementedException();
            return myContext.Persons.Find(NIK);
        }

        public int Insert(Person person)
        {
            var insert = 0;
            myContext.Persons.Add(person);
            if (person.NIK != "")
            {
                insert = myContext.SaveChanges();
                
            }
                return insert;

        }

        public int Update(Person person)
        {
            
            myContext.Entry(person).State = EntityState.Modified;
            var update = myContext.SaveChanges();
            return update;

            //throw new NotImplementedException();
        }
    }
}
