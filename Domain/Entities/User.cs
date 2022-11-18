namespace Domain.Entities;

public class User
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

    public string UserName
    {
        get;
        set;
    }

    public string Hash
    {
        get;
        set;
    }

    public string Salt
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