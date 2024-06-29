﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyWeb.DataAccess.Data;
using UdemyWeb.Models.Models;

namespace UdemyWeb.DataAccess.Repositories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void Update(Category category)
    {
        _db.Categories.Update(category);
    }

}
