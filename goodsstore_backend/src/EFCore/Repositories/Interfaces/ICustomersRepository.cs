﻿using goodsstore_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace goodsstore_backend.EFCore.Repositories.Interfaces
{
    public interface ICustomersRepository : ICrud<Customer>
    {      
    }
}
