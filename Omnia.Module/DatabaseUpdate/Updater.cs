using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using Omnia.Module.BusinessObjects.Common;
using System.Reflection;
using Xpand.ExpressApp.Security.Core;

namespace Omnia.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            CreateDefaultRole();
            CreateSampleRoles();
            CreateAccessPermissionRoles();
        }

        public const string DEFAULT_ADMIN_USERNAME = "omnia.admin@locanix.net";
        private void CreateSampleRoles()
        {
            //SecuritySystemUser sampleUser = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "User"));
            //if (sampleUser == null)
            //{
            //    sampleUser = ObjectSpace.CreateObject<SecuritySystemUser>();
            //    sampleUser.UserName = "User";
            //    sampleUser.SetPassword("");
            //}
            //SecuritySystemRole defaultRole = CreateDefaultRole();
            //sampleUser.Roles.Add(defaultRole);

            var userAdmin = ObjectSpace.FindObject<OmniaUser>(new BinaryOperator("UserName", DEFAULT_ADMIN_USERNAME));
            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<OmniaUser>();
                userAdmin.UserName = DEFAULT_ADMIN_USERNAME;
                userAdmin.Email = DEFAULT_ADMIN_USERNAME;
                userAdmin.FirstName = "Admin";
                userAdmin.LastName = "Omnia";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            SecuritySystemRole adminRole = ObjectSpace.FindObject<XpandRole>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<XpandRole>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
            userAdmin.Roles.Add(adminRole);
            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }

        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }

        private SecuritySystemRole CreateDefaultRole()
        {
            SecuritySystemRole defaultRole = ObjectSpace.FindObject<XpandRole>(new BinaryOperator("Name", "Default"));
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<XpandRole>();
                defaultRole.Name = "Default";

                defaultRole.AddObjectAccessPermission<SecuritySystemUser>("[Oid] = CurrentUserId()", SecurityOperations.ReadOnlyAccess);
                defaultRole.AddMemberAccessPermission<SecuritySystemUser>("ChangePasswordOnFirstLogon", SecurityOperations.Write, "[Oid] = CurrentUserId()");
                defaultRole.AddMemberAccessPermission<SecuritySystemUser>("StoredPassword", SecurityOperations.Write, "[Oid] = CurrentUserId()");
                defaultRole.SetTypePermissionsRecursively<SecuritySystemRole>(SecurityOperations.Read, SecuritySystemModifier.Allow);
                defaultRole.SetTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
                defaultRole.SetTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
                ObjectSpace.CommitChanges();
            }
            return defaultRole;
        }

        public static Type[] GetAllTypes(Type baseType, Assembly assembly)
        {
            return Array.FindAll(assembly.GetTypes(),
                delegate (Type type) {
                    return baseType == type || type.IsSubclassOf(baseType);
                }
            );
        }

        public static Type[] GetAllTypes(string baseTypePreffix, Assembly assembly)
        {
            return Array.FindAll(assembly.GetTypes(),
                delegate (Type type)
                {
                    return type.Name.Contains(baseTypePreffix);
                }
            );
        }

        private Session Session
        {
            get
            {
                return (ObjectSpace as XPObjectSpace).Session;
            }
        }

        private void CreateAccessPermissionRoles()
        {
            var aap = SecuritySystemRole.AutoAssociationPermissions;
            SecuritySystemRole.AutoAssociationPermissions = false;
            Type[] types = GetAllTypes(typeof(XPBaseObject), Assembly.GetAssembly(GetType()));
            CreateAccessRole(types, "Reader", SecurityOperations.Read);
            CreateAccessRole(types, "Writer", SecurityOperations.Write);
            CreateAccessRole(types, "CRUD", SecurityOperations.CRUDAccess);
            ObjectSpace.CommitChanges();
            SecuritySystemRole.AutoAssociationPermissions = aap;
        }

        private void CreateAccessRole(Type[] types, string name, string operations)
        {
            SecuritySystemRole role = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", name));
            if (role == null)
            {
                role = ObjectSpace.CreateObject<SecuritySystemRole>();
                role.Name = name;
            }

            foreach (var t in types)
            {
                //var test = (from tpo in reader.TypePermissions where tpo.TargetType == t select tpo).FirstOrDefault();
                //if (test != null)
                if (role.FindTypePermissionObject(t) != null)
                    continue;
                role.SetTypePermissions(t, operations, SecuritySystemModifier.Allow);
            }
        }
    }
}
