using PAP.Business.DbContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAP.Business.Managers
{
    public class BaseManager
    {
        protected ApplicationDatabaseContext _context;

        public BaseManager(ApplicationDatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}