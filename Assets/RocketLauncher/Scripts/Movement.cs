using UnityEngine;

namespace roecketlauncher.RocketLauncher.Scripts
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody rigidbody;
        private AudioSource audioSource;

        [SerializeField] private float Thrust = 1000;
        [SerializeField] private float RotatThrust = 200;
        [SerializeField] private AudioClip engineSound;
        [SerializeField] private ParticleSystem leftBoster;
        [SerializeField] private ParticleSystem rightBoster;
        [SerializeField] private ParticleSystem mainBoster;

        // Start is called before the first frame update
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void Update()
        {
            ProcessThrust();
            processInput();
        }

        private void ProcessThrust()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartThrust();
            }
            else
            {
                audioSource.Stop();
                mainBoster.Stop();
            }
        }

        private void StartThrust()
        {
            rigidbody.AddRelativeForce(Vector3.up * Thrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineSound);
            }

            if (!mainBoster.isPlaying)
            {
                mainBoster.Play();
            }
        }

        private void processInput()
        {
            if (Input.GetKey(KeyCode.A))
            {
                RotateLeft();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                RotateRight();
            }
            else
            {
                rightBoster.Stop();
                leftBoster.Stop();
            }
        }

        private void RotateRight()
        {
            ApplyRotation(-RotatThrust);
            if (!rightBoster.isPlaying)
            {
                rightBoster.Play();
            }
        }

        private void RotateLeft()
        {
            ApplyRotation(RotatThrust);
            if (!leftBoster.isPlaying)
            {
                leftBoster.Play();
            }
        }

        private void ApplyRotation(float rotationthrustforframe)
        {
            rigidbody.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationthrustforframe * Time.deltaTime);
            rigidbody.freezeRotation = false;
            //testing source tree
        }

        private static void test()
        {
            test();
        }
    }
}