using AutoMapper;
using MongoDB.Driver;
using Nest.Services.CatalogApi.DTO_s.Category;
using Nest.Services.CatalogApi.Models;
using Nest.Services.CatalogApi.Services.Interfaces;
using Nest.Services.CatalogApi.Settings;
using Nest.Shared.DTO_s;

namespace Nest.Services.CatalogApi.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _mapper = mapper;
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    }

    public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(c => true).ToListAsync();
        return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
    }

    public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (category == null) return ResponseDto<CategoryDto>.Fail("Category not found", 404);
        return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }

    public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryCollection.InsertOneAsync(category);

        return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }
}
