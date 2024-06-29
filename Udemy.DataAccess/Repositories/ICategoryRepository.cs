using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyWeb.Models.Models;

namespace UdemyWeb.DataAccess.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
    void Save();
}
