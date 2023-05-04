using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Xpo;

namespace CheckedListEF.Module {
    [DefaultClassOptions]
    public class Detail : BaseObject , IComparable {
    
    
        public virtual string DetailName { get; set; }

        public int CompareTo(object obj) {
            return this.ID.CompareTo(((Detail)obj).ID);
        }
    }
}