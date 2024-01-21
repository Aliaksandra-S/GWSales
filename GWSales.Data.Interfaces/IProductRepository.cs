using GWSales.Data.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWSales.Data.Interfaces;

public interface IProductRepository : IRepository<ProductEntity>
{
    Task<ProductEntity> GetByArticleAsync (string articleNumber);
}
