namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client
{
    public class RoleConst
    {
        /// <summary>
        /// 钱包所有人
        /// </summary>
        public const string WalletOwner = "WalletOwner";
        /// <summary>
        /// 钱包管理员
        /// </summary>
        public const string WalletAdmin = "WalletAdmin";
        /// <summary>
        /// 财务
        /// </summary>
        public const string FinancialStaff = "FinancialStaff";

        /// <summary>
        /// 运营人员
        /// </summary>
        public const string OperationalStaff = "OperationalStaff";

        /// <summary>
        /// ViewOnly
        /// </summary>
        public const string ViewOnly = "ViewOnly";

        public static string GetName(RoleEnum roleEnum)
        {
            string roleName = string.Empty;
            switch (roleEnum)
            {
                case RoleEnum.WalletOwner:
                    roleName = RoleConst.WalletOwner;
                    break;
                case RoleEnum.WalletAdmin:
                    roleName = RoleConst.WalletAdmin;
                    break;
                case RoleEnum.FinancialStaff:
                    roleName = RoleConst.FinancialStaff;
                    break;
                case RoleEnum.OperationalStaff:
                    roleName = RoleConst.OperationalStaff;
                    break;
                case RoleEnum.ViewOnly:
                    roleName = RoleConst.ViewOnly;
                    break;
            }
            return roleName;
        }

        public static RoleEnum GetRole(string roleName)
        {
            RoleEnum role = RoleEnum.None;
            switch (roleName)
            {
                case RoleConst.WalletOwner:
                    role = RoleEnum.WalletOwner;
                    break;
                case RoleConst.WalletAdmin:
                    role = RoleEnum.WalletAdmin;
                    break;
                case RoleConst.FinancialStaff:
                    role = RoleEnum.FinancialStaff;
                    break;
                case RoleConst.OperationalStaff:
                    role = RoleEnum.OperationalStaff;
                    break;
                case RoleConst.ViewOnly:
                    role = RoleEnum.ViewOnly;
                    break;
            }
            return role;
        }

        public static string GetRoleDiscrition(string roleName)
        {
            string descrition = "";
            switch (roleName)
            {
                case RoleConst.WalletOwner:
                    descrition = "账号注册人";
                    break;
                case RoleConst.WalletAdmin:
                    descrition = "管理员";
                    break;
                case RoleConst.FinancialStaff:
                    descrition = "财务";
                    break;
                case RoleConst.OperationalStaff:
                    descrition = "运营";
                    break;
                case RoleConst.ViewOnly:
                    descrition = "只读";
                    break;
            }
            return descrition;
        }

        public enum RoleEnum
        {
            None,
            WalletOwner,
            WalletAdmin,
            FinancialStaff,
            OperationalStaff,
            ViewOnly,

        }
    }
}