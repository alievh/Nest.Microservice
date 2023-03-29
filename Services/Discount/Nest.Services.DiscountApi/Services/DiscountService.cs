using Dapper;
using Nest.Services.DiscountApi.Models;
using Nest.Shared.DTO_s;
using Npgsql;
using System.Data;

namespace Nest.Services.DiscountApi.Services;

public class DiscountService : IDiscountService
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public DiscountService(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
    }


    public async Task<ResponseDto<List<Discount>>> GetAll()
    {
        var discounts = await _dbConnection.QueryAsync<Discount>("select * from discount");
        return ResponseDto<List<Discount>>.Success(discounts.ToList(), 200);
    }

    public async Task<ResponseDto<Discount>> GetById(int id)
    {
        var discount = (await _dbConnection.QueryAsync<Discount>("select * from discount where id = @Id", new { Id = id })).SingleOrDefault();

        if (discount == null)
        {
            return ResponseDto<Discount>.Fail("Discount not found!", 404);
        }


        return ResponseDto<Discount>.Success(discount, 200);
    }

    public async Task<ResponseDto<Discount>> GetByCodeAndUserId(string code, string userId)
    {
        var discounts = await _dbConnection.QueryAsync<Discount>("select * from discount where userid=@UserId and code=@Code", new
        {
            UserId = userId,
            Code = code
        });

        var hasDiscount = discounts.FirstOrDefault();

        if (hasDiscount == null)
        {
            return ResponseDto<Discount>.Fail("Discount not found!", 404);
        }

        return ResponseDto<Discount>.Success(hasDiscount, 200);
    }

    public async Task<ResponseDto<NoContent>> Save(Discount discount)
    {
        var saveStatus = await _dbConnection.ExecuteAsync("insert into discount(userid,rate,code) values(@UserId,@Rate,@Code)", discount);

        if (saveStatus > 0)
        {
            return ResponseDto<NoContent>.Success(204);
        }

        return ResponseDto<NoContent>.Fail("An error occuered while adding!", 500);
    }

    public async Task<ResponseDto<NoContent>> Update(Discount discount)
    {
        var updateStatus = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id", new
        {
            discount.Id,
            discount.UserId,
            discount.Code,
            discount.Rate
        });

        if (updateStatus > 0)
        {
            return ResponseDto<NoContent>.Success(204);
        }

        return ResponseDto<NoContent>.Fail("Discount not found!", 404);
    }

    public async Task<ResponseDto<NoContent>> Delete(int id)
    {
        var deleteStatus = await _dbConnection.ExecuteAsync("delete from discount where id = @Id", new { Id = id });

        return deleteStatus > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Discount not found!", 404);
    }
}
