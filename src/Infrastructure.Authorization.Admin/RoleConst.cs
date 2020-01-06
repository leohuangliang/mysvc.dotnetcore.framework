namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Admin
{
    public class RoleConst
    {
        /// <summary>
        /// 管理员
        /// </summary>
        public const string Admin = "Admin";


        public static string GetName(RoleEnum roleEnum)
        {
            string roleName = string.Empty;
            switch (roleEnum)
            {
                case RoleEnum.Admin:
                    roleName = RoleConst.Admin;
                    break;
            }
            return roleName;
        }

        public static RoleEnum GetRole(string roleName)
        {
            RoleEnum role = RoleEnum.None;
            switch (roleName)
            {
                case RoleConst.Admin:
                    role = RoleEnum.Admin;
                    break;
               
            }
            return role;
        }

        public static string GetRoleDiscrition(string roleName)
        {
            string descrition = "";
            switch (roleName)
            {
                case RoleConst.Admin:
                    descrition = "系统管理员";
                    break;
            }
            return descrition;
        }

        public enum RoleEnum
        {
            None,
            Admin,

        }
    }
}