﻿using CheckedListEF.Module;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using Microsoft.AspNetCore.Components;
using System.Collections;

namespace CheckedListEF.Blazor.Server.Editors.CheckedListBoxEditor {

    public class CheckedListBoxAdapter : ComponentAdapterBase {
        public CheckedListBoxAdapter(CheckedListBoxModel componentModel) {
            ComponentModel = componentModel ?? throw new ArgumentNullException(nameof(componentModel));
            ComponentModel.ValueChanged += ComponentModel_ValueChanged;
        }
        public override CheckedListBoxModel ComponentModel { get; }
        public override void SetAllowEdit(bool allowEdit) {
            ComponentModel.ReadOnly = !allowEdit;
        }
        public override object GetValue() {
            return ComponentModel.Values;
        }
        public override void SetValue(object value) {
            if (value is IList listValue) {
                ComponentModel.Values = listValue.Cast<object>().ToList();
            }
        }
        protected override RenderFragment CreateComponent() {
            return ComponentModelObserver.Create(ComponentModel, CheckedListBoxRenderer.Create(ComponentModel));
        }
        private void ComponentModel_ValueChanged(object sender, EventArgs e) => RaiseValueChanged();
        public override void SetAllowNull(bool allowNull) { /* ...*/ }
        public override void SetDisplayFormat(string displayFormat) { /* ...*/ }
        public override void SetEditMask(string editMask) { /* ...*/ }
        public override void SetEditMaskType(EditMaskType editMaskType) { /* ...*/ }
        public override void SetErrorIcon(ImageInfo errorIcon) { /* ...*/ }
        public override void SetErrorMessage(string errorMessage) { /* ...*/ }
        public override void SetIsPassword(bool isPassword) { /* ...*/ }
        public override void SetMaxLength(int maxLength) { /* ...*/ }
        public override void SetNullText(string nullText) { /* ...*/ }
    }
}
