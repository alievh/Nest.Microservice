namespace Nest.Services.Order.Application.DTO_s;

public class AddressDto
{
    public string Province { get; set; } = null!;
    public string District { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Line { get; set; } = null!;
}
