﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CheckedList.Blazor.Server.Editors.CheckedListBoxEditor {

    [PropertyEditor(typeof(XPBaseCollection), false)]
    public class CheckedListBoxEditor : BlazorPropertyEditorBase, IComplexViewItem {
        private IObjectSpace objectSpace;
        private XafApplication application;

        public CheckedListBoxEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        protected override IComponentAdapter CreateComponentAdapter() {
            List<object> dataSource = GetDataSource();
            IModelClass classInfo = application.Model.BOModel.GetClass(MemberInfo.ListElementTypeInfo.Type);
            var displayMember = classInfo.DefaultProperty;
            return new CheckedListBoxAdapter(new CheckedListBoxModel(dataSource, displayMember));
        }
        public override CheckedListBoxModel ComponentModel => (Control as CheckedListBoxAdapter)?.ComponentModel;

        private List<object> GetDataSource() =>
            objectSpace.GetObjects(MemberInfo.ListElementTypeInfo.Type).Cast<object>().ToList();

        protected override void WriteValueCore() {
            var xpColl = (XPBaseCollection)PropertyValue;
            var coll = (IEnumerable<object>)ControlValue;
            foreach (var it in xpColl.Cast<BaseObject>().Where(o => !coll.Any(x => ((BaseObject)x).Oid == o.Oid)).ToList()) {
                xpColl.BaseRemove(it);
            }
            foreach (var item in coll) {
                if (!xpColl.Cast<BaseObject>().Any(x => x.Oid == ((BaseObject)item).Oid)) {
                    xpColl.BaseAdd(item);
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
            if (ComponentModel is not null) {
                ComponentModel.DataSource = null;
            }
        }

        protected override void OnCurrentObjectChanged() {
            base.OnCurrentObjectChanged();
            if (ComponentModel is not null) {
                ComponentModel.DataSource = GetDataSource();
            }
        }

        #region IComplexPropertyEditor Members
        public void Setup(IObjectSpace _objectSpace, XafApplication _application) {
            this.objectSpace = _objectSpace;
            this.application = _application;
        }
        #endregion
    }
}
