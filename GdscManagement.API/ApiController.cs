using AutoMapper;
using GdscManagement.API.Features.Base;
using GdscManagement.Common.Features.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GdscManagement.API;

[ApiController]
public abstract class ApiController<T, TR> : ControllerBase where T : IModel where TR : IModelResponse
{
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IMapper Mapper;

    protected ApiController(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Mapper = ServiceProvider.GetRequiredService<IMapper>();
    }

    protected T Map(TR request)
    {
        return Mapper.Map<T>(request);
    }

    protected TR Map(T model)
    {
        return Mapper.Map<TR>(model);
    }

    protected IEnumerable<T> Map(IEnumerable<TR> request)
    {
        return Mapper.Map<IEnumerable<T>>(request);
    }

    protected IEnumerable<TR> Map(IEnumerable<T> model)
    {
        return Mapper.Map<IEnumerable<TR>>(model);
    }
}
