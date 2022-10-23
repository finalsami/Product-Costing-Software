using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class FormulationMasterBAL
    {
        public int UserId { get; set; }

        public int FormulationId { get; set; }
        public string FormulationName { get; set; }
        public int BatchSize { get; set; }
        public int Labours { get; set; }
        public int Supervisors { get; set; }
        public int PowerUnits { get; set; }
        public decimal MaintenanceCost { get; set; }
        public decimal OtherCost { get; set; }
        public decimal AdditionalBuffer { get; set; }
        public int UnitMeasurementId { get; set; }
        public int action { get; set; }
    }
}
