using System;


namespace Business.Workflow
{

    /// <summary>
    /// Enumeration of workflow state
    /// </summary>
    public enum WorkflowOperatorState
    {
        Unstarted,
        Working,
        Waiting,
        Finished
    }
    
    public class WorkflowOperatorState
    {

        public WorkFlowOperator()
        {
            State = WorkflowState.Unstarted;
        }

        public WorkflowState State { private set; get;}

    }

}