using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace MySvc.Framework.Infrastructure.Authorization.Merchant.Permissions
{
    public static class PermissionManage
    {
        private readonly static List<RolePermission> _roleList = new List<RolePermission>();


        static PermissionManage()
        {
        }

        /// <summary>
        /// 根据角色获取Permission
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public static List<string> GetPermissions(string role)
        {

            if (!_roleList.Any())
            {
                XmlDocument xmlDoc = new XmlDocument();
                string[] ss = typeof(PermissionManage).Assembly.GetManifestResourceNames();
                Console.WriteLine(ss.Count());
                foreach (var i in ss)
                {
                    Console.WriteLine(i);
                }

                using (Stream stream = typeof(PermissionManage).GetTypeInfo().Assembly.GetManifestResourceStream("MySvc.Framework.Infrastructure.Authorization.Client.Permissions.Permission.xml"))
                {
                    if (stream == null)
                    {
                        Console.WriteLine("null");
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            xmlDoc.Load(sr);
                        }

                        foreach (XmlNode node in xmlDoc.SelectNodes("//Permission"))
                        {
                            string roles = node.Attributes["Roles"].Value;
                            if (roles == "all")
                            {
                                _roleList.AddRange(new List<RolePermission>()
                                 {
                                     new RolePermission(){ Role = RoleConst.WalletOwner,Permission= node.Attributes["Name"].Value },
                                     new RolePermission(){ Role = RoleConst.WalletAdmin,Permission= node.Attributes["Name"].Value  },
                                     new RolePermission(){ Role = RoleConst.FinancialStaff  ,Permission= node.Attributes["Name"].Value },
                                     new RolePermission(){ Role = RoleConst.OperationalStaff ,Permission= node.Attributes["Name"].Value },
                                     new RolePermission(){ Role = RoleConst.ViewOnly ,Permission= node.Attributes["Name"].Value },

                                 });
                            }
                            else
                            {
                                List<string> roleList = roles.Split(',').ToList();
                                if (roleList.Any())
                                {
                                    foreach (var item in roleList)
                                    {
                                        _roleList.Add(new RolePermission() { Role = item, Permission = node.Attributes["Name"].Value });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return _roleList.Where(c => c.Role == role).Select(c => c.Permission).ToList();
        }

        

    }
    public class RolePermission
    {
        public string Role { get; set; }
        public string Permission { get; set; }
    }

}
