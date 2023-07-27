using Microsoft.AspNetCore.Mvc;
using ProductApiVSC.Models;
using Microsoft.AspNetCore.Authorization;  
using Microsoft.EntityFrameworkCore;
using ProductApiVSC.Container;
using ProductApiVSC.Entity;

namespace ProductApiVSC.Controllers;

// [Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductContainer _DBContext;
    // private readonly LearnDbContext _DBContext;

    // public ProductController(LearnDbContext dbContext)
    public ProductController(IProductContainer dbContext)
    {
        this._DBContext = dbContext;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> GetAll()
    {
        // var product = this._DBContext.Products.ToList();
        var product = await this._DBContext.GetAll();
        return Ok(product);
    }

    [HttpGet("GetbyCode/{code}")]
    public async Task<IActionResult> GetbyCode(int code)
    {
      var product = await this._DBContext.GetbyCode(code);
      // var product = this._DBContext.Products.FirstOrDefault(p => p.Id == code);
      return Ok(product);
    }

    [HttpDelete(Name = "Delete/{code}")]
    public async Task<IActionResult> DeleteByCode(int code)
    {
      var product = await this._DBContext.Remove(code);
      // var product = this._DBContext.Products.FirstOrDefault(p => p.Id == code);
      // if(product != null){
      //   this._DBContext.Remove(product);
      //   this._DBContext.SaveChanges();
      //   return Ok(true);
      // }

      return Ok(false);
    }

    // [HttpPost(Name = "Create")]
    // public async Task<IActionResult> Create([FromBody] Product _product)
    // {
    //   var product = this._DBContext.Save(_product);
    //   // var product = this._DBContext.Products.FirstOrDefault(p => p.Id == _product.Id);
    //   // if(product != null){
    //   //   product.Name = _product.Name;
    //   //   product.Price = _product.Price;
    //   //   this._DBContext.SaveChanges();
    //   // } else {
    //   //   this._DBContext.Products.Add(_product);
    //   //   this._DBContext.SaveChanges();
    //   // }
    //   return Ok(true);
    // }
}