using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using CheckedListEF.Module;
using DevExpress.Persistent.BaseImpl;

namespace CheckedListEF.Blazor.Server.Editors.CheckedListBoxEditor {

    [PropertyEditor(typeof(IList<Detail>), false)]
    public class CheckedListBoxEditor : BlazorPropertyEditorBase, IComplexViewItem {
        public CheckedListBoxEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override IComponentAdapter CreateComponentAdapter() {
            this.AllowEdit.Clear();
            var allDetails = objectSpace.GetObjects<Detail>();
            return new CheckedListBoxAdapter(new CheckedListBoxModel(allDetails));
        }

        protected override void WriteValueCore() {
            var propertyColl = (IList<Detail>)PropertyValue;
            var controlColl = (IEnumerable<Detail>)ControlValue;
            foreach (var it in propertyColl) {
                var cnt = controlColl.Where(x => x.ID == it.ID).Count();
                if (cnt == 0) {
                    propertyColl.Remove(it);
                }
            }
            foreach (var item in controlColl) {
                var cnt = propertyColl.Where(x => x.ID == item.ID).Count();
                if (cnt == 0) {
                    propertyColl.Add(item);
                }
            }
            objectSpace.SetModified(CurrentObject);
        }
        IObjectSpace objectSpace;

        #region IComplexPropertyEditor Members

        public void Setup(IObjectSpace objectSpace, XafApplication application) {
            this.objectSpace = objectSpace;
        }

        #endregion
    }
}
