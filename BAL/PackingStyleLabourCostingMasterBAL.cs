using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PackingStyleLabourCostingMasterBAL
    {
        public int PackingStyleLabourCostingId { get; set; }
        public int FkPackingSizeCategoryId { get; set; }
        public int FkPackingStyleId { get; set; }
        public decimal PackingSize { get; set; }
        public int FkUnitMeasurementId { get; set; }
        public int TaskBulkCharge { get; set; }
        public int TaskPouchFilling { get; set; }
        public int TaskBottleKeeping { get; set; }
        public int TaskLiftingWeight { get; set; }
        public int TaskBlacklinnerPouch { get; set; }
        public int TaskInnerPlug { get; set; }
        public int TaskMesuringCap { get; set; }
        public int TaskCaping { get; set; }
        public int TaskTearDownSeal { get; set; }
        public int TaskInduction { get; set; }
        public int TaskPouchSealing { get; set; }
        public int TaskBottlePouchCleaning { get; set; }
        public int TaskLabeling { get; set; }
        public int TaskSleeve { get; set; }
        public int TaskInnerbox { get; set; }
        public int TaskSSTinDrumBucketBag { get; set; }
        public int TaskInnerBoxCelloTape { get; set; }
        public int TaskKitchenTray { get; set; }
        public int TaskOuterLabelBOPPBoxFilling { get; set; }
        public int TaskStappingWeight { get; set; }
        public int TaskAdditionalOther { get; set; }

        public decimal PowerFilling { get; set; }
        public decimal PowerCapping { get; set; }
        public decimal PowerInduction { get; set; }
        public decimal PowerLableling { get; set; }
        public decimal PowerShrinking { get; set; }
        public decimal PowerBOPP { get; set; }
        public decimal PowerStepping { get; set; }
        public decimal PowerStealingMC { get; set; }
        public decimal PowerDetail9 { get; set; }
        public decimal PowerDetail10 { get; set; }
        public decimal PowerUnitPerHour { get; set; }
        public decimal PowerOther { get; set; }

        public int action { get; set; }
        public int UserId { get; set; }

    }
}
