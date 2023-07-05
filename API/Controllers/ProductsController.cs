using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using API.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<ProductBrand> _brandRepo;
        private readonly IRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepo;

        public ProductsController(IRepository<Product> productRepo,
         IRepository<ProductBrand> brandRepo,
         IRepository<ProductType> typeRepo,
         IMapper mapper)
        {
            _brandRepo = brandRepo ?? throw new ArgumentNullException(nameof(brandRepo));
            _typeRepo = typeRepo ?? throw new ArgumentNullException(nameof(typeRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetAll()
        {
            var spec = new ProductsWithTypeAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> Get(int id)
        {
            var spec = new ProductsWithTypeAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            return Ok(await _brandRepo.GetAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {
            return Ok(await _typeRepo.GetAsync());
        }
    }
}