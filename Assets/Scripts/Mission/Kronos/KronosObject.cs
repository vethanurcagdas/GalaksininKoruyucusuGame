using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Kronos
{
    public class KronosObject : MonoBehaviour
    {
        public List<int> ObjectId;
        protected Image image;

        protected virtual void Awake()
        {
            image = GetComponent<Image>();
        }
    }
}