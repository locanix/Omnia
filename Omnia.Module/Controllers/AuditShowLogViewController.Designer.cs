namespace Omnia.Module.Controllers
{
    partial class AuditShowLogViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AuditShowLogAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // AuditShowLogAction
            // 
            this.AuditShowLogAction.Caption = "AuditShowLog";
            this.AuditShowLogAction.ConfirmationMessage = null;
            this.AuditShowLogAction.Id = "AuditShowLog";
            this.AuditShowLogAction.ImageName = "BO_Contract";
            this.AuditShowLogAction.ToolTip = null;
            this.AuditShowLogAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AuditShowLogAction_Execute);
            // 
            // AuditShowLogViewController
            // 
            this.Actions.Add(this.AuditShowLogAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction AuditShowLogAction;
    }
}
