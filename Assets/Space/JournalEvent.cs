using System;
using UnityEngine;

namespace Space
{
    public class JournalEvent : MonoBehaviour
    {
        public event Action Consumed;

        public void Consume()
        {
            Consumed?.Invoke();
        }
    }
}