using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace CheckedList.Blazor.Server.Editors.CheckedListBoxEditor {

    [PropertyEditor(typeof(string), false)]
    public class CheckedListBoxEditor : BlazorPropertyEditorBase {
        public CheckedListBoxEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override IComponentAdapter CreateComponentAdapter() => new CheckedListBoxAdapter(new CheckedListBoxModel());
    }
}
