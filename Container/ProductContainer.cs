using Microsoft.EntityFrameworkCore;
using ProductApiVSC.Models;
using ProductApiVSC.Entity;
using AutoMapper;

namespace ProductApiVSC.Container;

public class ProductContainer : IProductContainer
{
    private readonly LearnDbContext _DBContext;
    private readonly IMapper _mapper;

    public ProductContainer(LearnDbContext dBContext,IMapper mapper1)
    {
        _DBContext = dBContext;
        _mapper=mapper1;
    }

    public async Task<String[]> GetAll()
    // public async Task<List<ProductEntity>> GetAll()
    {
        try
        {
            List<ProductEntity> resp = new List<ProductEntity>();
            var product = await _DBContext.Products.ToListAsync();
            // var finalProduct = Product.GetProduct(product);

            List<String[]> arr = {};

            if (product != null)
            {
                resp=_mapper.Map<List<ProductEntity>>(product);
            }

            if(resp != null)
            {
                foreach (var item in resp)
                {
                    arr.Add(item.Price.ToString()),
                    arr.Add(item.Name).ToString()
                    arr.Add(item.Brand).ToCharArray()
                }
            }
            return arr;
            // return resp;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<ProductEntity> GetbyCode(int code)
    {
        var product = await _DBContext.Products.FindAsync(code);
        if (product != null)
        {
            ProductEntity resp=_mapper.Map<Product,ProductEntity>(product);
            return resp;
        }
        else
        {
            return new ProductEntity();
        }
    }

    public async Task<bool> Remove(int code)
    {
        var product = await _DBContext.Products.FindAsync(code);
        if (product != null)
        {
            this._DBContext.Remove(product);
            await this._DBContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> Save(ProductEntity _product)
    {
        var product = this._DBContext.Products.FirstOrDefault(o => o.Id == _product.Id);
        if (product != null)
        {
            product.Name = _product.ProductName;
            product.Price = _product.Price;
            await this._DBContext.SaveChangesAsync();
        }
        else
        {
            Product _prod=_mapper.Map<ProductEntity,Product>(_product);
            this._DBContext.Products.Add(_prod);
            await this._DBContext.SaveChangesAsync();
        }
        return true;
    }
}