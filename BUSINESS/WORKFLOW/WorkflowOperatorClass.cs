using System;


namespace Business.Workflow
{

    /// <summary>
    /// Enumeration of workflow state
    /// </summary>
    Public enum WorkflowState
    {
        Unstarted;
        Started;
        Finished;
    }

    
    public class WorkFlowOperator
    {

        public WorkFlowOperator()
        {
            State = WorkflowState.Unstarted
        }

        WorkflowState State { private set; public get;}

    }

}