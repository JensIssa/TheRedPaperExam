using Domain.Entities;

namespace Application.DTOs;

public class PutUserDTO
{
    public int Id
    {
        get;
        set;
    }
    
    public Role AssignedRole
    {
        get;
        set;
    }
    
    public string FirstName
    {
        get;
        set;
    }

    public string LastName
    {
        get;
        set;
    }

    public string Username
    {
        get;
        set;
    }

    public string Password
    {
        get;
        set;
    }
    public DateTime BirthDay
    {
        get;
        set;
    }

    public string Email
    {
        get;
        set;
    }

    public int PhoneNumber
    {
        get;
        set;
    }

    public string Location
    {
        get;
        set;
    }
}