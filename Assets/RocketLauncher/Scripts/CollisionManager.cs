using roecketlauncher.RocketLauncher.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketLauncher.Scripts
{
    public class CollisionManager : MonoBehaviour
    {
        private bool isTransitioning = false;
        private static string tag;
        private static Movement move;
        private static AudioSource audioSource;
        private static bool collisionDisabled = false;
        
        [SerializeField] private int time = 1;
        [SerializeField] private AudioClip crash;
        [SerializeField] private AudioClip success;
        [SerializeField] private ParticleSystem successParticle;
        [SerializeField] private ParticleSystem crashParticle;
        [SerializeField] private bool debug;
        

        private void Start()
        {
            move = GetComponent<Movement>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (debug)
            {
                DebugCommand();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isTransitioning||collisionDisabled) { return; }
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    Succes();
                    Invoke("LoadNextLevel", time);
                    break;
                case "Fuel":
                    UnityEngine.Debug.Log("You fuel got full");
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }

        private void StartCrashSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(crash);
            crashParticle.Play();
            move.enabled = false;
            Invoke("LoadingActiveScene", time); }


        private void Succes()
        {
            isTransitioning = true;
            audioSource.Stop();
            move.enabled = false;
            audioSource.PlayOneShot(success);
            successParticle.Play();
        }

        public void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }

            SceneManager.LoadScene(nextSceneIndex);
        }


        public void LoadingActiveScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
        private void DebugCommand()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                collisionDisabled = !collisionDisabled;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                LoadNextLevel();
            }
        }
    }
}