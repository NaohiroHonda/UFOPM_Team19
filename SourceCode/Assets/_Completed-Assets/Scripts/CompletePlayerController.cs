using UnityEngine;
using System.Collections;
using GamepadInput;
//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour
{

    public float speed;             //Floating point variable to store the player's movement speed.
    public Text countText;          //Store a reference to the UI Text component which will display the number of pickups collected.
    public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.
    public Transform aim;
    public Rigidbody2D beam;
    public GameObject guard;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private int count;				//Integer to store the number of pickups collected so far.
    public GamePad.Index index;
    private LineRenderer shotLine;
    public bool isGuard;
    private bool isDamage, isBlowOff;
    private SpriteRenderer sr;
    private float interval,interval2;

    private AudioSource[] audio;
    private AudioClip beamSE, damageSE;

    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        audio = GetComponents<AudioSource>();
        beamSE = audio[0].clip;
        damageSE = audio[1].clip;

        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        // shotLine = aim.Find("ShotLine").GetComponent<LineRenderer>();

        //Initialize count to zero.
        count = 0;

        //Initialze winText to a blank string since we haven't won yet at beginning.
        winText.text = "";

        //Call our SetCountText function which will update the text with the current value for count.
        SetCountText();
    }

    void SetAim()
    {
        aim.position += (Vector3)GamePad.GetAxis(GamePad.Axis.RightStick, index) * Time.deltaTime * 30;
        if ((Vector3.Distance(transform.position, aim.position)) >= 20)
            aim.position -= -(transform.position - aim.position).normalized;
    }

    void Shot()
    {
        if (GamePad.GetButtonDown(GamePad.Button.RightShoulder, index) && !isGuard && !isDamage)
        {
            Rigidbody2D rig = Instantiate(beam, transform.position, Quaternion.identity) as Rigidbody2D;
            if (rig != null)
                rig.velocity = (aim.position - transform.position).normalized * 100;
            audio[0].PlayOneShot(audio[0].clip);
        }
    }

    void Update()
    {
        if (isDamage)
        {
            interval++;

            if (interval >= 50)
            {
                isDamage = false;
                interval = 0;
            }

            float alfa = sr.color.a;
            alfa -= 1f;
            if (alfa < 0)
                alfa = 1;
            sr.color = new Color(1, 1, 1, alfa);

            gameObject.layer = LayerMask.NameToLayer("Damage");
        }
        else gameObject.layer = 0;

        if (isBlowOff)
        {
            interval2++;

            if (interval2 >= 50)
            {
                isBlowOff = false;
                interval2 = 0;
            }

        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        SetAim();
        Shot();
        Guard();
        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = GamePad.GetAxis(GamePad.Axis.LeftStick, index);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        if (!isGuard)
            rb2d.AddForce(movement * speed);
    }

    void Tackle(GameObject obj)
    {
        Rigidbody2D enemyRig = obj.GetComponent<Rigidbody2D>();
        CompletePlayerController enemy = obj.GetComponent<CompletePlayerController>();
        if (rb2d.velocity.magnitude > enemyRig.velocity.magnitude)
        {
            enemy.BlowOff(rb2d);
            rb2d.AddForce(-rb2d.velocity, ForceMode2D.Impulse);
        }
    }

    void BlowOff(Rigidbody2D enemy)
    {
        isBlowOff = true;
        if (isGuard) rb2d.AddForce(enemy.velocity * 1.3f, ForceMode2D.Impulse);
        else rb2d.AddForce(enemy.velocity * 1.15f, ForceMode2D.Impulse);
    }

    void Guard()
    {
        if (GamePad.GetButton(GamePad.Button.LeftShoulder, index) && !isDamage)
            isGuard = true;
        else isGuard = false;

        if (isGuard)
        {
            guard.SetActive(true);
            if (!isBlowOff)
                rb2d.velocity = Vector2.zero;
        }
        else guard.SetActive(false);
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            Tackle(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Beam1" && (int)index == 2)
        {
            isDamage = true;
            audio[1].PlayOneShot(audio[1].clip);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "Beam2" && (int)index == 1)
        {
            isDamage = true;
            audio[1].PlayOneShot(audio[1].clip);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    void SetCountText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        countText.text = "Count: " + count.ToString();

        //Check if we've collected all 12 pickups. If we have...
        if (count >= 12)
            //... then set the text property of our winText object to "You win!"
            winText.text = "You win!";
    }
}
