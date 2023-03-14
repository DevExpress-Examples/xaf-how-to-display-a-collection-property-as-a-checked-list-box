using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using CheckedList.Module;

namespace CheckedList.Blazor.Server.Editors.CheckedListBoxEditor {

    [PropertyEditor(typeof(XPBaseCollection), false)]
    public class CheckedListBoxEditor : BlazorPropertyEditorBase, IComplexViewItem {
        public CheckedListBoxEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override IComponentAdapter CreateComponentAdapter() {
            var allDetails =(XPCollection<Detail>) objectSpace.GetObjects<Detail>();
            return new CheckedListBoxAdapter(new CheckedListBoxModel(allDetails));
        }

        XPBaseCollection checkedItems;
        XafApplication application;
        IObjectSpace objectSpace;

        #region IComplexPropertyEditor Members

        public void Setup(IObjectSpace objectSpace, XafApplication application) {
            this.application = application;
            this.objectSpace = objectSpace;
        }

        #endregion
    }
}
