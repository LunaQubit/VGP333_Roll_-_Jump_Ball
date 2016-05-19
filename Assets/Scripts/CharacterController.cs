using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float jumpForce = 10f;
    public Text scoreText;

    private Rigidbody cachedRigidbody;
    private GameObject[] pickups;
    private Vector3 lastCheckpoint = Vector3.zero;
    private Vector3 startPosition = Vector3.zero;

    private bool jumped = false;
    private bool isOnGround = false;
    private int score;
    private int lastCheckpointIndex = -1;

    public void Start()
    {
        pickups = GameObject.FindGameObjectsWithTag( "Pickup" );

        cachedRigidbody = GetComponent<Rigidbody>();
        lastCheckpoint = transform.position;
        startPosition = transform.position;

        UpdateScore( 0 );
    }

    public void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            for( int i = 0; i < pickups.Length; ++i )
            {
                lastCheckpoint = startPosition;
                Die();
                UpdateScore(0);
                pickups[i].SetActive( true );
                Vector2 rand = Random.insideUnitCircle;
                Vector3 oldPosition = pickups[i].transform.position;
                Vector3 position = new Vector3( rand.x * 9f, oldPosition.y, rand.y * 9f);
                pickups[i].transform.position = position;
            }
        }
    }

    public void FixedUpdate()
    {
        jumped = false;
        if( Input.GetButtonDown( "Jump" ) && isOnGround )
        {
            jumped = true;
            isOnGround = false;
        }

        float h = Input.GetAxis( "Horizontal" );
        float v = Input.GetAxis( "Vertical" );

        float j = 0f;
        if( jumped )
        {
            j = jumpForce;
        }
        cachedRigidbody.AddForce(h * speed, j, v * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat( "Hit something: {0}", other.name );
        
        if( other.CompareTag( "Pickup" ) )
        {
            other.gameObject.SetActive( false );
            UpdateScore(score + 1);
        }
        Checkpoint ckp = other.GetComponent<Checkpoint>();
        //if( other.CompareTag( "Checkpoint"))
        if( ckp != null && ckp.wasTriggered == false && ckp.index > lastCheckpointIndex)
        {
            lastCheckpoint = other.transform.position;
            ckp.wasTriggered = true;
            lastCheckpointIndex = ckp.index;
        }
        if( other.CompareTag( "DeathBound" ) )
        {
            Die();
        }
    }

    private void SensorHit( Sensor sensor )
    {
        if( sensor.name == "FootSensor" )
        {
            isOnGround = true;
        }
    }

    private void Die()
    {
        transform.position = lastCheckpoint;

        cachedRigidbody.velocity = Vector3.zero;
        cachedRigidbody.angularVelocity = Vector3.zero;
    }

    private void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + score.ToString();
    }
}
