﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreMart.BLL.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }

        //   IShoppingCartRepository ShoppingCart { get; }
        IBrandRepository Brands { get; }
        Task<int> CompleteAsync();
    }
}
