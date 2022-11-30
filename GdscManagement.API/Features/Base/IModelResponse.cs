namespace GdscManagement.API.Features.Base;

public interface IModelResponse
{
    string Id { get; set; }
    DateTime Created { get; set; }
    DateTime Updated { get; set; }
}
