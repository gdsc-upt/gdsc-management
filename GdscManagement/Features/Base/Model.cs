namespace GdscRecruitment.Common.Features.Base;

public abstract class Model : IModel
{
    protected Model()
    {
        Id = Guid.NewGuid().ToString();
        Created = Updated = DateTime.UtcNow;
    }

    public string Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }
}
