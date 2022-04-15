using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoatConstruction
{
    public class ConstructionDisplayList : MonoBehaviour
    {
        public GameObject displayTemplate;
        private readonly List<ConstructionDisplay> _displays = new List<ConstructionDisplay>();
        private ConstructionManager _constructionManager;

        private const float OffsetY = 65;

        void Start()
        {
            _constructionManager = GameManager.Get().GetConstructionManager();

            _constructionManager.JobsChanged += OnJobsChanged;
            _constructionManager.JobCompleted += (ConstructionJob job) =>
                OnJobsChanged(_constructionManager.GetAllJobs().ToArray());
        }

        private void OnJobsChanged(ConstructionJob[] jobs)
        {
            foreach (var constructionDisplay in _displays)
            {
                Destroy(constructionDisplay.gameObject);
            }

            _displays.Clear();
            for (var i = 0; i < jobs.Length; i++)
            {
                var displayRoot = Instantiate(displayTemplate, transform.position + Vector3.down * (OffsetY * i),
                    transform.rotation, transform);
                var display = displayRoot.GetComponent<ConstructionDisplay>();

                display.UpdateData(jobs[i]);

                _displays.Add(display);
            }
        }

        void Update()
        {
        }
    }
}