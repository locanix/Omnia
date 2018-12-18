using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security.Strategy;

namespace Omnia.Module.BusinessObjects.Common
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [CreatableItem(false)]
    public class AuditLogon : BaseObject
    {
        public AuditLogon(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            EventDateTime = DateTime.Now;
            User = Session.GetObjectByKey<SecuritySystemUser>(SecuritySystem.CurrentUserId);
        }

        private DateTime _EventDateTime;
        public DateTime EventDateTime
        {
            get { return _EventDateTime; }
            set { SetPropertyValue("EventDateTime", ref _EventDateTime, value); }
        }

        private AuditLogonEvent _Event;
        public AuditLogonEvent Event
        {
            get { return _Event; }
            set { SetPropertyValue("Event", ref _Event, value); }
        }

        private string _SysName;
        public string SysName
        {
            get { return _SysName; }
            set { SetPropertyValue("SysName", ref _SysName, value); }
        }

        private string _Workstation;
        public string Workstation
        {
            get { return _Workstation; }
            set { SetPropertyValue("Workstation", ref _Workstation, value); }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { SetPropertyValue("UserName", ref _UserName, value); }
        }

        private SecuritySystemUser _User;
        public SecuritySystemUser User
        {
            get { return _User; }
            set { SetPropertyValue("User", ref _User, value); }
        }

    }

    public enum AuditLogonEvent
    {
        LoggedOn,
        LoggedOff,
        LogonFailed
    }
}