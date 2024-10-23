using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.DataAccess.AdvanceRecommendation
{
    public class AdvanceRecommendationData : IAdvanceRecommendationData
    {

        private readonly string _spStatusParam;

        public AdvanceRecommendationData()
        {
            _spStatusParam = "@p_Status";
        }

    }
}
