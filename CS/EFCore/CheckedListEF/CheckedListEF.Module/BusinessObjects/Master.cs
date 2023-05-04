using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System.Collections.ObjectModel;

namespace CheckedListEF.Module {
    [DefaultClassOptions]
    public class Master : BaseObject {
        public virtual string MasterName { get; set; }
        public virtual IList<Detail> Details { get; set; } = new ObservableCollection<Detail>();
    }
}