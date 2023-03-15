using System;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;

using System.Windows.Forms;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using CheckedListEF.Module;

namespace CheckedList.Module.Win {
    [PropertyEditor(typeof(IList<Detail>), false)]
    public class WinCheckedListBoxPropertyEditor : WinPropertyEditor, IComplexViewItem {
        public WinCheckedListBoxPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override object CreateControlCore() {
            return new CheckedListBoxControl();
        }
        IList<Detail> checkedItems;
        XafApplication application;
        IObjectSpace objectSpace;
        protected override void ReadValueCore() {
            base.ReadValueCore();
            if (PropertyValue is IList<Detail>) {
                Control.ItemCheck -= new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(control_ItemCheck);
                checkedItems = (IList<Detail>)PropertyValue;
                IList<Detail> dataSource = objectSpace.GetObjects<Detail>(); 

                Control.DataSource = dataSource;
                Control.DisplayMember = nameof(Detail.DetailName);
                foreach (Detail obj in checkedItems) {
                    Control.SetItemChecked(dataSource.IndexOf(obj), true);
                }
                Control.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(control_ItemCheck);
            }
        }
        void control_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e) {
            Detail obj = (Detail)Control.GetItemValue(e.Index);
            switch (e.State) {
                case CheckState.Checked:
                    checkedItems.Add(obj);
                    break;
                case CheckState.Unchecked:
                    checkedItems.Remove(obj);
                    break;
            }
            OnControlValueChanged();
            objectSpace.SetModified(CurrentObject);
        }
        public new CheckedListBoxControl Control {
            get {
                return (CheckedListBoxControl)base.Control;
            }
        }

        #region IComplexPropertyEditor Members

        public void Setup(IObjectSpace objectSpace, XafApplication application) {
            this.application = application;
            this.objectSpace = objectSpace;
        }

        #endregion
    }
}
