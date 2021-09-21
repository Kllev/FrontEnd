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
    public class OldPersonsRepository : IPersonRepository
    {
        private readonly MyContext myContext;
        public OldPersonsRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(string NIK)
        {
            var wantdelete = myContext.Persons.Find(NIK);
            if(wantdelete == null)
            {
                throw new Exception();
            }
            myContext.Persons.Remove(wantdelete);
            var deleted = myContext.SaveChanges();
            return deleted;
        }

        public IEnumerable<Person> Get()
        {
            return myContext.Persons.ToList();
        }

        public Person Get(string NIK)
        {
            //throw new NotImplementedException();
            //var finding = myContext.Persons.Find(NIK);
            return myContext.Persons.Find(NIK);
            //return finding;
        }

        public int Insert(Person persons)
        {
            myContext.Persons.Add(persons);
            var insert = myContext.SaveChanges();
            return insert;
        }

        public int Update(Person persons)
        {
            var wantupdate = myContext.Persons.Find(persons);
            if (wantupdate != null)
            {
                myContext.Entry(persons).State = EntityState.Modified;
                var update = myContext.SaveChanges();
                return update;
            }
            //row new Exception();
            throw new NotImplementedException();
        }
    }
}
