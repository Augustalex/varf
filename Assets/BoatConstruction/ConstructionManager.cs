using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoatConstruction
{
    public class ConstructionManager
    {
        private readonly Queue<ConstructionJob> _jobs = new Queue<ConstructionJob>();

        public event Action<ConstructionJob> JobCompleted;
        public event Action<ConstructionJob[]> JobsChanged;

        public void AddJob(int workerDaysNeeded)
        {
            _jobs.Enqueue(new ConstructionJob
            {
                neededWorkerDays = workerDaysNeeded,
                completedWorkerDays = 0
            });
        }

        public void DoDailyWork(int workers)
        {
            if (_jobs.Count == 0) return;

            var topJob = _jobs.Peek();
            topJob.completedWorkerDays = Mathf.Min(topJob.neededWorkerDays, topJob.completedWorkerDays + workers);
            if (topJob.completedWorkerDays == topJob.neededWorkerDays)
            {
                JobCompleted?.Invoke(topJob);
                _jobs.Dequeue();
            }

            JobsChanged?.Invoke(_jobs.ToArray());
        }

        public ConstructionJob[] GetAllJobs()
        {
            return _jobs.ToArray();
        }
    }
}