using AutoMapper;
using MongoDB.Driver;
using Nest.Services.CatalogApi.DTO_s.Product;
using Nest.Services.CatalogApi.Models;
using Nest.Services.CatalogApi.Services.Interfaces;
using Nest.Services.CatalogApi.Settings;
using Nest.Shared.DTO_s;

namespace Nest.Services.CatalogApi.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IMongoCollection<Product> _productService;
    private readonly IMongoCollection<SubCategory> _subCategoryService;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _productService = database.GetCollection<Product>(databaseSettings.CourseCollectionName);
        _subCategoryService = database.GetCollection<SubCategory>(databaseSettings.SubCategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productService.Find(c => true).ToListAsync();

        if (products.Any())
        {
            foreach (Product product in products)
            {
                product.SubCategory = await _subCategoryService.Find(c => c.Id == product.SubCategoryId).FirstAsync();
            }
        }
        else products = new List<Product>();

        return ResponseDto<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
    }

    public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
    {
        var product = await _productService.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (product == null) return ResponseDto<ProductDto>.Fail("Product not found", 404);
        product.SubCategory = await _subCategoryService.Find(c => c.Id == product.SubCategoryId).FirstAsync();
        return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
    }

    public async Task<ResponseDto<List<ProductDto>>> GetByUserIdAsync(string userId)
    {
        var products = await _productService.Find(c => c.UserId == userId).ToListAsync();
        if (products.Any())
        {
            foreach (Product product in products)
            {
                product.SubCategory = await _subCategoryService.Find(c => c.Id == product.SubCategoryId).FirstAsync();
            }
        }
        else products = new List<Product>();

        return ResponseDto<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
    }

    public async Task<ResponseDto<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
    {
        var newProduct = _mapper.Map<Product>(productCreateDto);

        newProduct.CreatedTime = DateTime.Now;
        await _productService.InsertOneAsync(newProduct);

        return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(newProduct), 200);
    }

    public async Task<ResponseDto<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto)
    {
        var updateProduct = _mapper.Map<Product>(productUpdateDto);
        var result = await _productService.FindOneAndReplaceAsync(c => c.Id == productUpdateDto.Id, updateProduct);

        if (result == null) return ResponseDto<NoContent>.Fail("Product not found", 404);

        return ResponseDto<NoContent>.Success(204);
    }

    public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
    {
        var result = await _productService.DeleteOneAsync(c => c.Id == id);
        if (result.DeletedCount > 0) return ResponseDto<NoContent>.Success(204);
        else return ResponseDto<NoContent>.Fail("Product not found", 404);
    }
}
