using System;
using System.Linq;
using System.Text;
using System.Collections;
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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Xpo.Metadata;

namespace Omnia.Module.BusinessObjects.AccessControlObjects
{
    [DomainComponent]
    [XafDisplayName("Audit Log: Watch Audit Log")]
    public class Audit_ShowLogPermission { }
}
