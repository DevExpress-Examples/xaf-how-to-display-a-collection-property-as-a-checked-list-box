using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Xpo;

namespace CheckedListEF.Module {
    [DefaultClassOptions]
    public class Detail : BaseObject {
    
    
        public virtual string DetailName { get; set; }
      
    
    }
}