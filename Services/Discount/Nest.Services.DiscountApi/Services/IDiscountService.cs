using Nest.Services.DiscountApi.Models;
using Nest.Shared.DTO_s;

namespace Nest.Services.DiscountApi.Services
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<Discount>>> GetAll();
        Task<ResponseDto<Discount>> GetById(int id);
        Task<ResponseDto<NoContent>> Save(Discount discount);
        Task<ResponseDto<NoContent>> Update(Discount discount);
        Task<ResponseDto<NoContent>> Delete(int id);
        Task<ResponseDto<Discount>> GetByCodeAndUserId(string code, string userId);
    }
}
