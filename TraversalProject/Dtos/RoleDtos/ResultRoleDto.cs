namespace TraversalProject.Dtos.RoleDtos
{
    public class ResultRoleDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string NormalizedName { get; set; }
        public DateTime ConcurrencyStamp { get; set; }
    }
}
