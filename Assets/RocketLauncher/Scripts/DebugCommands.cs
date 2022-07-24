using UnityEngine;

namespace roecketlauncher.RocketLauncher.Scripts
{
    public class DebugCommands : MonoBehaviour
    {
        private CollisionManager Collider;
        [SerializeField]private bool debug;

        private void Start()
        {
            Collider = GetComponent<CollisionManager>();
        }

        private void Update()
        {
            if (debug)
            {
                Debug();
            }
                
        }

        private void Debug()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Collider.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
               Collider.LoadNextLevel();
            }
        }
    }
}