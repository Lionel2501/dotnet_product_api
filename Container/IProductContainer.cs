using ProductApiVSC.Models;
using ProductApiVSC.Entity;

namespace ProductApiVSC.Container;

public interface IProductContainer
{
    Task<List<ProductEntity>> GetAll();
    Task<ProductEntity> GetbyCode(int code);
    Task<bool> Remove(int code);
    Task<bool> Save(ProductEntity _product);
}