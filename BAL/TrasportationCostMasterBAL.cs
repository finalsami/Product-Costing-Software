using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class TrasportationCostMasterBAL
    {
        public int TransportationCostId { get; set; }
        public int FkStateId { get; set; }
        public decimal TruckLoadCharges { get; set; }
        public decimal NoCarton { get; set; }
        public int FkCompanyId { get; set; }
        public int action { get; set; }
        public int UserId { get; set; }
    }
    public class TrasportationCostFactorBAL
    {
        public int FkTransportationCostId { get; set; }
        public int TransportationCostFactorId { get; set; }
        public int UnloadingCostFactorId { get; set; }
        public int CartageCostFactorId { get; set; }
        public decimal Start { get; set; }
        public decimal End { get; set; }
        public decimal Amount { get; set; }
        public int FkCompanyId { get; set; }
        public int FkUnitMeasurementId { get; set; }
        public int action { get; set; }
        public int Type { get; set; }
        public int UserId { get; set; }


    }
}
