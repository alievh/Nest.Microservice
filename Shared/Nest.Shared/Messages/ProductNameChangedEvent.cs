namespace Nest.Shared.Messages;

public class ProductNameChangedEvent
{
    public string CourseId { get; set; }
    public string UpdatedName { get; set; }
}
