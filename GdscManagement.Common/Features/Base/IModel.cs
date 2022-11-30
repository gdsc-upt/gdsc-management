namespace GdscManagement.Common.Features.Base;

public interface IModel
{
    string Id { get; }

    DateTime Created { get; set; }

    DateTime Updated { get; set; }
}
