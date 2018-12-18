using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Validation;
using Omnia.Module.Validation;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security;
using Omnia.Module.BusinessObjects.AccessControlObjects;

namespace Omnia.Module {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
    public sealed partial class OmniaModule : ModuleBase {
        public OmniaModule() {
            InitializeComponent();
			BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(ApplicationModulesManager moduleManager)
        {
            base.Setup(moduleManager);
            ValidationRulesRegistrator.RegisterRule(moduleManager,
                typeof(PasswordStrengthCodeRule),
                typeof(IRuleBaseProperties));
        }

        public override void Setup(XafApplication application) {
            application.SetupComplete += new EventHandler<EventArgs>(application_SetupComplete);
            base.Setup(application);

            SecurityStrategy.AdditionalSecuredTypes.Add(typeof(Audit_ShowLogPermission));
        }

        private void application_SetupComplete(object sender, EventArgs e)
        {
            ValidationModule module = (ValidationModule)((XafApplication)sender).Modules.FindModule(typeof(ValidationModule));
            if (module != null)
            {
                module.InitializeRuleSet();
            }
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }
    }
}
