﻿using GdscManagement.Common.Features.Base;
using GdscManagement.Common.Features.Clients.Models;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.Common.Features.Projects.Models;


public enum ProjectStatuses{OnGoing, Completed, ToBeDone}
public class Project : Model
{
    public string Title { get; set; }
    
    public User Manager { get; set; }
    
    public ProjectStatuses Status { get; set; }  //{Ongoing, Completed, NotStarted}
    
    public Client? Client { get; set; }

    public List<User> Developers { get; set; } 
}
    
