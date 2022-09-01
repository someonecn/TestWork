using _Code_Hub;
using UnityEngine;

namespace _Code__FireController
{
    public class SystemUpdater : MonoBehaviour
    {
        private void Update()
        {
            Hub.onSystemUpdate?.Invoke(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            Hub.onSystemFixedUpdate?.Invoke(Time.fixedDeltaTime);
        }
        private void LateUpdate()
        {
            Hub.onSystemLateUpdate?.Invoke(Time.deltaTime);
        }
    }
}
