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
    

    public class WorkflowOperator
    {

        public WorkflowOperator()
        {
            WorkflowState = WorkflowOperatorState.Unstarted;
        }

        public WorkflowOperatorState WorkflowState { private set; get;}

    }

}