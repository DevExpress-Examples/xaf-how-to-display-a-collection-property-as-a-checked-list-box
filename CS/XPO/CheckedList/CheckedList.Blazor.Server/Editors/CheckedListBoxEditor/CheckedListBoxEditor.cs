using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using CheckedList.Module;
using DevExpress.Persistent.BaseImpl;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace CheckedList.Blazor.Server.Editors.CheckedListBoxEditor {

    [PropertyEditor(typeof(XPBaseCollection), false)]
    public class CheckedListBoxEditor : BlazorPropertyEditorBase, IComplexViewItem {
        public CheckedListBoxEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override IComponentAdapter CreateComponentAdapter() {
            this.AllowEdit.Clear();
            List<Object> dataSource = objectSpace.GetObjects(MemberInfo.ListElementTypeInfo.Type).Cast<Object>().ToList();
            IModelClass classInfo = application.Model.BOModel.GetClass(MemberInfo.ListElementTypeInfo.Type);
            var displayMember = classInfo.DefaultProperty;
            return new CheckedListBoxAdapter(new CheckedListBoxModel(dataSource, displayMember));
        }

        protected override void WriteValueCore() {
            var xpColl = (XPBaseCollection)PropertyValue;
            var coll = (IEnumerable<Object>)ControlValue;
            foreach (var it in xpColl) {
                var cnt = coll.Where(x => ((BaseObject)x).Oid == ((BaseObject)it).Oid).Count();
                if (cnt == 0) {
                    xpColl.BaseRemove(it);
                }
            }
            foreach (var item in coll) {
                var cnt = ((IList)xpColl).Cast<BaseObject>().Where(x => x.Oid == ((BaseObject)item).Oid).Count();
                if (cnt == 0) {
                    xpColl.BaseAdd(item);
                }
            }
            objectSpace.SetModified(CurrentObject);
        }
        IObjectSpace objectSpace;
        XafApplication application;

        #region IComplexPropertyEditor Members

        public void Setup(IObjectSpace _objectSpace, XafApplication _application) {
            this.objectSpace = _objectSpace;
            this.application = _application;
        }

        #endregion
    }
}
