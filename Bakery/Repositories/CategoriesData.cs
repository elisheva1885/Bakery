using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoriesData : ICategoriesData
    {
        BakeryDBContext dBContext;//_dBContext;

        public CategoriesData(BakeryDBContext usersDBContext)
        {
            dBContext = usersDBContext;
        }
        public async Task<List<Catgory>> GetCatgories() // Changed to PascalCase
        {
            return await dBContext.Catgories.ToListAsync<Catgory>();
        }
    }
}
