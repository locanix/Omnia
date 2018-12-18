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

namespace Omnia.Module.BusinessObjects.Common
{
    //[DefaultClassOptions]
    [ImageName("BO_Employee")]
    [RuleCriteria("Employee.HireDate", DefaultContexts.Save, "HireDate <= Now()", UsedProperties ="HireDate", CustomMessageTemplate = "Hire date shoul be not in the future")]
    public class Employee : OmniaUser
    { 
        public Employee(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        //[RuleRequiredField]
        //[RuleRegularExpression("OmniaUser.Email", DefaultContexts.Save, @"^([a-z0-9_.-])+@([a-zA-Z_.-])+\.([a-zA-Z0-9])+([a-zA-Z])+$", CustomMessageTemplate = "Email should be in valid format")]
        //public string Email
        //{
        //    get { return UserName; }
        //    set { UserName = value; }
        //}

        private DateTime _HireDate;
        [XafDisplayName("Hire Date"), ToolTip("Employee`s Joining Data for Company")]
        [RuleRequiredField]
        public DateTime HireDate
        {
            get { return _HireDate; }
            set { SetPropertyValue("HireDate", ref _HireDate, value); }
        }

        private string _Designation;
        [XafDisplayName("Designation")]
        //[Persistent("")]
        public string Designation
        {
            get { return _Designation; }
            set { SetPropertyValue("Designation", ref _Designation, value); }
        }

        private Department _Departmant;
        [XafDisplayName("Departmant")]
        [Association("Employee-Department")]
        public Department Departmant
        {
            get { return _Departmant; }
            set { SetPropertyValue("Departmant", ref _Departmant, value); }
        }

    }
}