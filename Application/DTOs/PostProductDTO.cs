﻿using Domain.Entities;

namespace Application.DTOs;

public class PostProductDTO
{
    public string ProductName
    {
        get;
        set;
    }

    public string ImageUrl
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public double Price
    {
        get;
        set;
    }

    public Condition ProductCondition
    {
        get;
        set;
    }

    public SubCategory? SubCategory
    {
        get;
        set;
    }

    public int SubCategoryID
    {
        get;
        set;
    }

    public User? user
    {
        get;
        set;
    }

    public int userId
    {
        get;
        set;
    }
}