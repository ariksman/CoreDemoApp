﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Core.Domain;
using Repository.Core.Repositories;

namespace Repository.EFCore.Repositories
{
    public class EmployerRepository : Repository<Employer>, IEmployerRepository
    {
        public EmployerRepository(DbContext context) : base(context)
        {
        }

        public bool AddEmployer(Employer employer)
        {
            if (!DatabaseContext.Employers.Any(n => n.Name == employer.Name))
            {
                DatabaseContext.Employers.Add(employer);
                return true;
            }
            return false;
        }

        public bool RemoveEmployer(Employer employer)
        {
            var selectedEmployer = DatabaseContext.Employers.SingleOrDefault(n => n.Name == employer.Name);
            if (selectedEmployer == null) return false;

            DatabaseContext.Employers.Remove(selectedEmployer);
            return true;
        }

        public IEnumerable<Worker> GetAllWorkers(Employer employer)
        {
            throw new NotImplementedException();
        }

        public DatabaseContext DatabaseContext => Context as DatabaseContext;
    }
}