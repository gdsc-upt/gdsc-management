using System.ComponentModel;
using MudBlazor;

namespace GdscManagement.Features.Base;

public abstract class ViewModel
{
    [Label("Identifier")] [ReadOnly(true)] public string Id { get; set; }

    [Label("Created")] [ReadOnly(true)] public DateTime Created { get; set; }

    [Label("Updated")] [ReadOnly(true)] public DateTime Updated { get; set; }
}
