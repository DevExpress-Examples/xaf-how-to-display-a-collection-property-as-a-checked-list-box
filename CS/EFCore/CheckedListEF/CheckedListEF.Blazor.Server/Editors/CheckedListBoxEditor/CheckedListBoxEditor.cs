using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using CheckedListEF.Module;
using DevExpress.Persistent.BaseImpl;
using static System.Net.Mime.MediaTypeNames;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.XtraExport.Implementation;

namespace CheckedListEF.Blazor.Server.Editors.CheckedListBoxEditor {

    [PropertyEditor(typeof(IList<Object>), false)]
    public class CheckedListBoxEditor : BlazorPropertyEditorBase, IComplexViewItem {
        public CheckedListBoxEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        private XafApplication application;
        private CheckedListBoxAdapter _adapter;
        IObjectSpace objectSpace;
        protected override IComponentAdapter CreateComponentAdapter() {
            List<object> dataSource = GetDataSource();
            IModelClass classInfo = application.Model.BOModel.GetClass(MemberInfo.ListElementTypeInfo.Type);
            var displayMember = classInfo.DefaultProperty;
            _adapter = new CheckedListBoxAdapter(new CheckedListBoxModel(dataSource, displayMember));
            return _adapter;
        }
        private List<object> GetDataSource() =>
          objectSpace.GetObjects(MemberInfo.ListElementTypeInfo.Type).Cast<object>().ToList();
        protected override void WriteValueCore() {
            var propertyColl = (IList<object>)PropertyValue;
            var controlColl = (IEnumerable<object>)ControlValue;
            foreach (var it in propertyColl) {
                if (!controlColl.Any(x => ((BaseObject)x).ID == ((BaseObject)it).ID)) {
                    propertyColl.Remove(it);
                }
            }
            foreach (var item in controlColl) {
                if (!propertyColl.Cast<BaseObject>().Any(x => x.ID == ((BaseObject)item).ID)) {
                    propertyColl.Add(item);
                }
            }
            objectSpace.SetModified(CurrentObject);
        }
    
        protected override void RefreshReadOnly() {
            base.RefreshReadOnly();
            AllowEdit[MemberIsNotReadOnly] = true;
        }

        protected override void OnCurrentObjectChanging() {
            base.OnCurrentObjectChanging();
            if (_adapter?.ComponentModel is not null) {
                _adapter.ComponentModel.DataSource = null;
            }
        }

        protected override void OnCurrentObjectChanged() {
            base.OnCurrentObjectChanged();
            if (_adapter?.ComponentModel is not null) {
                _adapter.ComponentModel.DataSource = GetDataSource();
            }
        }
        #region IComplexPropertyEditor Members

        public void Setup(IObjectSpace objectSpace, XafApplication _application) {
            this.objectSpace = objectSpace;
            this.application = _application;
        }

        #endregion
    }
}
