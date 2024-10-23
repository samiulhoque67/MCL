namespace SILDMS.DataAccess.AdvanceApproval
{
    public class AdvanceApprovalData : IAdvanceApprovalData
    {
        private readonly string _spStatusParam;

        public AdvanceApprovalData()
        {
            _spStatusParam = "@p_Status";
        }

    }
}
