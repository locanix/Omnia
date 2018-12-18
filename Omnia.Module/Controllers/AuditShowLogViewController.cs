using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using Omnia.Module.BusinessObjects.AccessControlObjects;

namespace Omnia.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AuditShowLogViewController : ViewController
    {
        public const string ObjectKey = "The object hase Oid key";
        public AuditShowLogViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            AuditShowLogAction.Active.SetItemValue("Security", DataManipulationRight.CanEdit(typeof(Audit_ShowLogPermission), null, null, null, null));
            AuditShowLogAction.Active.SetItemValue(ObjectKey, false);

            var ov = View as ObjectView;
            if (ov == null || ov.ObjectTypeInfo == null || ov.ObjectTypeInfo.KeyMember == null)
                return;

            AuditShowLogAction.Active.SetItemValue(ObjectKey, ov.ObjectTypeInfo.KeyMember.MemberType == typeof(Guid) || ov.ObjectTypeInfo.KeyMember.MemberType == typeof(int));
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void AuditShowLogAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var obj = View.CurrentObject as XPBaseObject;
            if (obj == null && View.SelectedObjects.Count > 0)
                obj = View.SelectedObjects[0] as XPBaseObject;

            if (obj == null)
                return;

            var os = Application.CreateObjectSpace();
            string keyName;
            if (os.GetKeyPropertyType(obj.GetType()) == typeof(Guid))
                keyName = "GuidId";
            else
                keyName = "IntId";

            var log = os.FindObject<AuditedObjectWeakReference>(CriteriaOperator.Parse(keyName + " == ?", obj.GetMemberValue(os.GetKeyPropertyName(obj.GetType()))));
            var view = Application.CreateDetailView(os, log);
            e.ShowViewParameters.CreatedView = view;
            if (Application.GetType().FullName.Contains(".Win."))
            {
                e.ShowViewParameters.NewWindowTarget = NewWindowTarget.MdiChild;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }
    }
}
