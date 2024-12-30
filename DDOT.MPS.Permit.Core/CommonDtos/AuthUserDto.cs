namespace DDOT.MPS.Permit.Core.CommonDtos
{
    public class AuthUserDto
    {
        public int Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Emailaddress { get; set; }
        public short RoleId { get; set; }
        public List<short> PermissionIds { get; set; } = new List<short>();
    }
}
