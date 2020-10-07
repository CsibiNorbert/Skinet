using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skiner.Data.Contexts;
using Skiner.Infrastructure.Repositories;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Skinet.Dtos;
using Skinet.Errors;
using Skinet.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> brandRepository,
            IGenericRepository<ProductType> typeRepository,
            IMapper autoMapper)
        {
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _productRepo = productRepository;
            _mapper = autoMapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ReturnProductDto>>> GetProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(productSpecParams);
            var countSpec = new ProductFiltersWithCountSpecification(productSpecParams);

            var totalItems = await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ReturnProductDto>>(products);
            return Ok(new Pagination<ReturnProductDto>(productSpecParams.PageIndex, productSpecParams.PageSize, totalItems, data));
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) ,StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(productId);
            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null)
            {
                // 404: Not Found
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<ReturnProductDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepository.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typeRepository.ListAllAsync();
            return Ok(types);
        }
    }
}
