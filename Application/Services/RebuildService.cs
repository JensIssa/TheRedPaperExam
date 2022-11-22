﻿using Application.InterfaceServices;

namespace Application.Services;

public class RebuildService
{
    private readonly IRebuildRepository _rebuild;

    public RebuildService(IRebuildRepository rebuild)
    {
        _rebuild = rebuild;
    }
    
    public void RebuildDB()
    {
        _rebuild.RebuildDB();
    }
}