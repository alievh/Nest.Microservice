namespace Nest.Services.PictureStockApi.DTO_s;

public class PictureDto
{
	public PictureDto()
	{
		Urls = new List<string>();
	}

	public List<string> Urls { get; set; } = null!;
}
