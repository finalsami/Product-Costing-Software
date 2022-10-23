using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class CostVariableMasterBAL
    {
        public int OtherVariableId { get; set; }
        public decimal ShiftHours { get; set; }
        public decimal BreakHours { get; set; }
        public decimal NetShiftHours { get; set; }
        public decimal PowerUnitPrice { get; set; }
        public decimal LabourCharge { get; set; }
        public decimal SuperVisorCharge { get; set; }
        public decimal UnloadingExpenseLtr { get; set; }
        public decimal UnloadingExpenseKg { get; set; }
        public decimal UnloadingExpenseUnit { get; set; }
        public decimal LoadingExpenceLtr { get; set; }
        public decimal LoadingExpenceKg { get; set; }
        public decimal LoadingExpenceUnit { get; set; }
        public decimal MachinaryExpenceLtr { get; set; }
        public decimal MachinaryExpenceKg { get; set; }
        public decimal MachinaryExpenceUnit { get; set; }
        public decimal AdminExpenceLtr { get; set; }
        public decimal AdminExpenceKg { get; set; }
        public decimal AdminExpenceUnit { get; set; }
        public decimal ExtraExpenceLtr { get; set; }
        public decimal ExtraExpenceKg { get; set; }
        public decimal ExtraExpenceUnit { get; set; }
        public int UserId { get; set; }
        public decimal action { get; set; }

    }
}
