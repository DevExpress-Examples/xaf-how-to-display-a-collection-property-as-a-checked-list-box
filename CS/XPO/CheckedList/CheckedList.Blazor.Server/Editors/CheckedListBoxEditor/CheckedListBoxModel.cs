using CheckedList.Module;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CheckedList.Blazor.Server.Editors.CheckedListBoxEditor {

    public class CheckedListBoxModel : ComponentModelBase {

        public CheckedListBoxModel(XPCollection<Detail> _details) {
            DataSource = _details;
        }
        //public List<Detail> Value {
        //    get => GetPropertyValue<List<Detail>>();
        //    set => SetPropertyValue(value);
        //}
        //IEnumerable<Detail> _values;
        //public IEnumerable<Detail> Values {
        //    get { return _values; }
        //    set {
        //        _values = value.ToList();
        //    SetValueFromUI(value);
        //    }
        //}
        public IEnumerable<Detail> Values {
            get => GetPropertyValue<IEnumerable<Detail>>();
            set { SetPropertyValue(value); 
                  SetValueFromUI(value);
            }

        }
        public XPCollection<Detail> DataSource { get; set; }
        public bool ReadOnly {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        // ...
        public void SetValueFromUI(IEnumerable<Detail> value) {
            SetPropertyValue(value, notify: false, nameof(Values));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ValueChanged;
    }
}
