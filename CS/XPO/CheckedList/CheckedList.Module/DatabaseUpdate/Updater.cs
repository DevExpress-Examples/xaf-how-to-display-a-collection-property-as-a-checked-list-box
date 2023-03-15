using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;

namespace CheckedList.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        var masterCount = ObjectSpace.GetObjectsCount(typeof(Master), null);
        if (masterCount == 0) {
            var master = ObjectSpace.CreateObject<Master>();
            master.MasterName = "TestMaster";
            for (int i = 0; i < 5; i++) {
                var detail = ObjectSpace.CreateObject<Detail>();
                detail.DetailName = "Detail" + i;
                if (i == 2 || i == 3) {
                    master.Details.Add(detail);
                }
            }
            ObjectSpace.CommitChanges();
        }
    }

    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
}
