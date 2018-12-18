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
using Xpand.ExpressApp.Security.Core;
using DevExpress.ExpressApp.Utils;

namespace Omnia.Module.BusinessObjects.Common
{
    //[DefaultClassOptions]
    [ImageName("BO_User")]
    [DefaultProperty("FullName")]
    [XafDisplayName("User")]
    public class OmniaUser : XpandUser
    { 
        public OmniaUser(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TempPassword = Rfc2898PasswordCryptographer.GenerateSalt(8);
            SetPassword(TempPassword);
            ChangePasswordOnFirstLogon = true;
        }

        //[RuleRequiredField]
        //[RuleRegularExpression("OmniaUser.Email", DefaultContexts.Save, @"^([a-z0-9_.-])+@([a-zA-Z_.-])+\.([a-zA-Z0-9])+([a-zA-Z])+$", CustomMessageTemplate = "Email should be in valid format")]
        //public string Email
        //{
        //    get { return UserName; }
        //    set { UserName = value; }
        //}

        private string _FirstName;
        [XafDisplayName("First Name")]
        [RuleRequiredField]
        [RuleRegularExpression("Contact.FirstName", DefaultContexts.Save, @"^([A-Z])([a-z])*$", CustomMessageTemplate = "Only Alphabets allowed")]
        //[Persistent("")]
        public string FirstName
        {
            get { return _FirstName; }
            set { SetPropertyValue("FirstName", ref _FirstName, value); }
        }

        private string _LastName;
        [XafDisplayName("LastName")]
        [RuleRequiredField]
        [RuleRegularExpression("Contact.LastName", DefaultContexts.Save, @"^([A-Z])([a-z])*$", CustomMessageTemplate = "Only Alphabets allowed")]
        public string LastName
        {
            get { return _LastName; }
            set { SetPropertyValue("LastName", ref _LastName, value); }
        }

        [PersistentAlias("FirstName + ' ' + LastName")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string FullName
        {
            get
            {
                return (string)EvaluateAlias("FullName");
            }
        }

        private string _Mobile1;
        [XafDisplayName("Mobile(1)")]
        [Size(15)]
        [RuleRegularExpression("Contact.Mobile1", DefaultContexts.Save, @"^([0-9]){6,}$", CustomMessageTemplate = "Only Number allowed minimum length is 6")]
        public string Mobile1
        {
            get { return _Mobile1; }
            set { SetPropertyValue("Mobile1", ref _Mobile1, value); }
        }

        private string _Mobile2;
        [XafDisplayName("Mobile(2)")]
        [Size(15)]
        [RuleRegularExpression("Contact.Mobile2", DefaultContexts.Save, @"^([0-9]){6,}$", CustomMessageTemplate = "Only Number allowed minimum length is 6")]
        public string Mobile2
        {
            get { return _Mobile2; }
            set { SetPropertyValue("Mobile2", ref _Mobile2, value); }
        }

        private string _Language;
        [XafDisplayName("Language")]
        [Size(15)]
        public string Language
        {
            get { return _Language; }
            set { SetPropertyValue("Language", ref _Language, value); }
        }

        private string _Notes;
        [XafDisplayName("Notes")]
        [Size(150)]
        //[Persistent("")]
        public string Notes
        {
            get { return _Notes; }
            set { SetPropertyValue("Notes", ref _Notes, value); }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInReports(false)]
        public string TempPassword { get; set; }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == "Email")
            {
                UserName = Email;
            }
        }
    }
}