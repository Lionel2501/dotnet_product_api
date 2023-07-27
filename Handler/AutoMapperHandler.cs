using AutoMapper;
using ProductApiVSC.Entity;
using ProductApiVSC.Models;

namespace ProductApiVSC.Handler;

public class AutoMapperHandler:Profile
{
   public AutoMapperHandler(){
    CreateMap<Product,ProductEntity>().ForMember(item=>item.ProductName,opt=>opt.MapFrom(item=>item.Name))
    .ForMember(item=>item.Price,opt=>opt.MapFrom(item=>Convert.ToDecimal(item.Price))).ReverseMap();

   }
}