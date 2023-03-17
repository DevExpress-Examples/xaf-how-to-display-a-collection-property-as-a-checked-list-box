using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using CheckedList.Module;
using DevExpress.Persistent.BaseImpl;
using System.Collections;

namespace CheckedList.Blazor.Server.Editors.CheckedListBoxEditor {

    [PropertyEditor(typeof(XPBaseCollection), false)]
    public class CheckedListBoxEditor : BlazorPropertyEditorBase, IComplexViewItem {
        public CheckedListBoxEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override IComponentAdapter CreateComponentAdapter() {
            this.AllowEdit.Clear();
            List<Object> dataSource = objectSpace.GetObjects(MemberInfo.ListElementTypeInfo.Type).Cast<Object>().ToList();
            //     var allDetails = objectSpace.GetObjects<Detail>();
            return new CheckedListBoxAdapter(new CheckedListBoxModel(dataSource));
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

        #region IComplexPropertyEditor Members

        public void Setup(IObjectSpace objectSpace, XafApplication application) {
            this.objectSpace = objectSpace;
        }

        #endregion
    }
}
