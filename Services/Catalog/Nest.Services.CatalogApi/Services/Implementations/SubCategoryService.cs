using AutoMapper;
using MongoDB.Driver;
using Nest.Services.CatalogApi.DTO_s.SubCategory;
using Nest.Services.CatalogApi.Models;
using Nest.Services.CatalogApi.Services.Interfaces;
using Nest.Services.CatalogApi.Settings;
using Nest.Shared.DTO_s;

namespace Nest.Services.CatalogApi.Services.Implementations;

public class SubCategoryService : ISubCategoryService
{
    private readonly IMongoCollection<SubCategory> _subCategoryCollection;
    private readonly IMapper _mapper;

    public SubCategoryService(IDatabaseSettings databaseSettings, IMapper mapper)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _subCategoryCollection = database.GetCollection<SubCategory>(databaseSettings.SubCategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<ResponseDto<List<SubCategoryDto>>> GetAllAsync()
    {
        List<SubCategory> subCategories = await _subCategoryCollection.Find(c => true).ToListAsync();
        return ResponseDto<List<SubCategoryDto>>.Success(_mapper.Map<List<SubCategoryDto>>(subCategories), 200);
    }

    public async Task<ResponseDto<SubCategoryDto>> GetByIdAsync(string id)
    {
        SubCategory subCategory = await _subCategoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (subCategory == null) return ResponseDto<SubCategoryDto>.Fail("SubCategory not found", 404);
        return ResponseDto<SubCategoryDto>.Success(_mapper.Map<SubCategoryDto>(subCategory), 200);
    }

    public async Task<ResponseDto<SubCategoryDto>> CreateAsync(SubCategoryDto subCategoryDto)
    {
        SubCategory subCategory = _mapper.Map<SubCategory>(subCategoryDto);
        await _subCategoryCollection.InsertOneAsync(subCategory);

        return ResponseDto<SubCategoryDto>.Success(_mapper.Map<SubCategoryDto>(subCategory), 200);
    }
}
