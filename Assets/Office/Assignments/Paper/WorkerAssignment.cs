using UnityEngine;

public class WorkerAssignment : Paper
{
    public int workerCount;
    
    public override void Enact()
    {
        FindObjectOfType<GameManager>().AddWorkers(workerCount);
    }
}