using CheckedList.Module;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CheckedList.Blazor.Server.Editors.CheckedListBoxEditor {

    public class CheckedListBoxModel : ComponentModelBase {

        public CheckedListBoxModel(List<Object> _details) {
            DataSource = _details;
        }

        public IEnumerable<Object> Values {
            get => GetPropertyValue<IEnumerable<Object>>();
            set { SetPropertyValue(value); 
                  SetValueFromUI(value);
            }

        }
        public List<Object> DataSource { get; set; }
        public bool ReadOnly {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        // ...
        public void SetValueFromUI(IEnumerable<Object> value) {
            SetPropertyValue(value, notify: false, nameof(Values));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ValueChanged;
    }
}
