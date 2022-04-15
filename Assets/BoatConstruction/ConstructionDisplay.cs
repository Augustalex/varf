using System;
using TMPro;
using UnityEngine;

namespace BoatConstruction
{
    public class ConstructionDisplay : MonoBehaviour
    {
        private ConstructionJob _job;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        public void UpdateData(ConstructionJob job)
        {
            _job = job;

            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = $"Small freighter ({_job.completedWorkerDays}/{_job.neededWorkerDays} worker days)";
        }

        private int GetBoatProgressionPercentage()
        {
            return (int) (Mathf.CeilToInt((float) _job.completedWorkerDays / (float) _job.neededWorkerDays * 100f));
        }
    }
}