using CheckedListEF.Module;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CheckedListEF.Blazor.Server.Editors.CheckedListBoxEditor {

    public class CheckedListBoxModel : ComponentModelBase {

        public CheckedListBoxModel(List<object> _source, string _fieldName) {
            DataSource = _source;
            FieldName = _fieldName;
        }

        public IEnumerable<object> Values {
            get => GetPropertyValue<IEnumerable<object>>();
            set => SetPropertyValue(value);
        }
        public string FieldName {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public List<object> DataSource {
            get => GetPropertyValue<List<object>>();
            set => SetPropertyValue(value);
        }
        public bool ReadOnly {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        // ...
        public void SetValueFromUI(IEnumerable<object> value) {
            SetPropertyValue(value, notify: false, nameof(Values));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ValueChanged;
    }
}
