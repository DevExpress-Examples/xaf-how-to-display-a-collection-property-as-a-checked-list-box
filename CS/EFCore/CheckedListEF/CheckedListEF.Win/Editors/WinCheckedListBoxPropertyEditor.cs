using System;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;

using System.Windows.Forms;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using CheckedListEF.Module;
using DevExpress.Persistent.BaseImpl.EF;
using System.Collections.ObjectModel;
using System.Collections;

namespace CheckedList.Module.Win {
    [PropertyEditor(typeof(IList<Object>), false)]
    public class WinCheckedListBoxPropertyEditor : WinPropertyEditor, IComplexViewItem {
        public WinCheckedListBoxPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override object CreateControlCore() {
            return new CheckedListBoxControl();
        }
        IList checkedItems;
        XafApplication application;
        IObjectSpace objectSpace;

        protected override void ReadValueCore() {
            base.ReadValueCore();
            Control.ItemCheck -= new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(control_ItemCheck);
            checkedItems = (IList)PropertyValue;
            List<Object> dataSource = objectSpace.GetObjects(MemberInfo.ListElementTypeInfo.Type).Cast<Object>().ToList();

            Control.DataSource = dataSource;
            IModelClass classInfo = application.Model.BOModel.GetClass(MemberInfo.ListElementTypeInfo.Type);
            Control.DisplayMember = classInfo.DefaultProperty;
            foreach (object obj in checkedItems) {
                Control.SetItemChecked(dataSource.IndexOf(obj), true);
            }
            Control.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(control_ItemCheck);
            //   }
        }
        void control_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e) {
            object obj = Control.GetItemValue(e.Index);
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
