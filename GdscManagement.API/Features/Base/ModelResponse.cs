namespace GdscManagement.API.Features.Base;

public class ModelResponse: IModelResponse
{
    public string Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
