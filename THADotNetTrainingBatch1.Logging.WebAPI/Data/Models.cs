using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Data;

public class Student
{
    public int Id { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentEmail { get; set; } = null!;
    public int StudentAge { get; set; }
}

[Table("Tbl_User")]
public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

[Table("Tbl_Role")]
public class Role
{
    [Key]
    public int Id { get; set; }
    public string RoleName { get; set; } = null!;
}

[Table("Tbl_Permission")]
public class Permission
{
    [Key]
    public int Id { get; set; }
    public string Menu { get; set; } = null!;
    public string Action { get; set; } = null!;
}

[Table("Tbl_UserRole")]
public class UserRole
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
}

[Table("Tbl_RolePermission")]
public class RolePermission
{
    [Key]
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}
