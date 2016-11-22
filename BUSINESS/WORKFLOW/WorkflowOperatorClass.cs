using System;


namespace Business.Workflow
{

    /// <summary>
    /// Enumeration of workflow state
    /// </summary>
    public enum WorkflowState
    {
        Unstarted,
        Started,
        Finished
    }

    
    public class WorkFlowOperator
    {

        public WorkFlowOperator()
        {
            State = WorkflowState.Unstarted;
        }

        public WorkflowState State { private set; get;}

    }

}