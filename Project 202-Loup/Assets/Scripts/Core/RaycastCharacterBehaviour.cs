using UnityEngine;
using System.Collections;

/// <summary>
/// This class is the character controller of the Raycast version of the project
/// </summary>
public class RaycastCharacterBehaviour : MonoBehaviour
{

    public int playerNumber = 1;

    public int life = 2;

    public bool doRun = true;

#region Character State Machine

    public CharacterFSMNames m_currentState = CharacterFSMNames.FALLING;

#endregion

#region Physics-related Variables

    #region Acceleration-related Variables

    //acceleration calculated this frame 
    // TODO : make it private for publishing
    public Vector2 m_acceleration = new Vector2(0, 0);                          

    /// <summary>
    /// Nominal Acceleration
    /// </summary>
    public float horizontalAcceleration = 50.0f;
                              
    //cap the acceleration under these values 
    // TODO : make it private for publishing
    public Vector2 maxAcceleration = new Vector2(1500.0f, 800.0f);              
    
    #endregion

    #region Velocity-related Variables

    public Vector2 m_velocity = new Vector2(0, 0);

    #endregion

    #region Movement-related Variables

    private Vector2 m_tmpMovement = new Vector2(0,0);
    public Vector2 m_movement = new Vector2(0, 0);

    #endregion

    #region Gravity-related Variables

    private Vector2 m_gravity = new Vector2(0, 0);
    public Vector2 gravityDirection = new Vector2(0, -1);
    public float gravityStrength = 20f;

    #endregion

    #region Jump-related Variables

    public bool m_hasJumped = false;
    public bool m_canJump = false;
    public float m_jumpForce = 10.0f;
    public float m_jumpMaxTime = 2.0f;
    private float m_jumpTimer = 0.0f;

    public int m_jumpNumber = 0;
    public int m_maxJumpNumber = 2;

    #endregion

#endregion

    #region Input Values

    private float horizontalAxisInput = 0.0f;
    private bool isJumpPressed = false;

    #endregion

    #region Collision variables

    public float skinSize = 0.005f;
    private float horizontalMaxDetection = 15.0f;
    private float verticalMaxDetection = 15.0f;

    public LayerMask groundLayer;

    #endregion

    #region Components Variables

    public BarsDisplay lifeBar;
    public ScoreUIBehaviour scoreUI;

    public RaycasterBahaviour raycaster;
    public Animator animator;

    #endregion

    void Awake()
    {
        raycaster = this.gameObject.GetComponent<RaycasterBahaviour>();
        animator = this.gameObject.GetComponent<Animator>();
        raycaster.groundCollisionLayer = groundLayer;
        raycaster.skinSize = skinSize;
        raycaster.sideRayNumber = 3;
        raycaster.botRayNumber = 3;
        raycaster.topRayNumber = 3;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {

        m_acceleration = Vector2.zero;
        SetGravity();
        HandleInputs();

        switch (m_currentState)
        {
            case CharacterFSMNames.FALLING:
                UpdateFall();
                break;
            case CharacterFSMNames.JUMPING:
                UpdateJump();
                break;
            case CharacterFSMNames.GROUNDED:
                UpdateGrounded();
                break;

        }

        m_velocity = m_acceleration * Time.deltaTime;
        m_tmpMovement = m_velocity * Time.deltaTime;
        m_movement = FinalizeMove(m_tmpMovement);

        this.transform.Translate(m_movement);

    }

    void LateUpdate()
    {
        raycaster.UpdateRays();
        Animate();
    }

    private void Animate()
    {

    }

    private void SetGravity()
    {
        m_gravity = gravityStrength * gravityDirection;
    }

    private void HandleInputs()
    {
        if(m_currentState == CharacterFSMNames.GROUNDED)
        {
            m_canJump = true;
        }
        else
        {
            m_canJump = false;
        }


        bool lastJumpInput = isJumpPressed;

        if (doRun)
        {
            horizontalAxisInput = 1.0f;
        }
        else
        {
            horizontalAxisInput = 0.0f;
        }

        if (playerNumber == 1 && m_canJump)
        {
            isJumpPressed = Input.GetButton("UpScreenTouch");
        }
        else if (playerNumber == 2 && m_canJump)
        {
            isJumpPressed = Input.GetButton("DownScreenTouch");
        }

        if (isJumpPressed)
        {
            m_jumpTimer += Time.deltaTime;
            if (m_jumpTimer < m_jumpMaxTime)
            {
                m_currentState = CharacterFSMNames.JUMPING;

                if (raycaster.CheckTopCollision())
                {
                    m_jumpTimer = m_jumpMaxTime;
                    m_currentState = CharacterFSMNames.FALLING;
                }
            }
            else
            {

                m_currentState = CharacterFSMNames.FALLING;
            }


        }
        else
        {
            if (m_currentState != CharacterFSMNames.GROUNDED)
            {
                m_currentState = CharacterFSMNames.FALLING;
            }
            
        }

    }

    private void UpdateJump()
    {
        m_acceleration.y += m_jumpForce;
        m_acceleration.x = horizontalAxisInput * horizontalAcceleration;

        m_acceleration.x = Mathf.Clamp(m_acceleration.x, -maxAcceleration.x, maxAcceleration.x);
        m_acceleration.y = Mathf.Clamp(m_acceleration.y, -maxAcceleration.y, maxAcceleration.y);
    }

    private void UpdateFall()
    {
        m_acceleration += m_gravity;
        m_acceleration.x = horizontalAxisInput * horizontalAcceleration;

        m_acceleration.x = Mathf.Clamp(m_acceleration.x, -maxAcceleration.x, maxAcceleration.x);
        m_acceleration.y = Mathf.Clamp(m_acceleration.y, -maxAcceleration.y, maxAcceleration.y);
    }

    private void UpdateGrounded()
    {
        m_acceleration += m_gravity;
        m_acceleration.x = horizontalAxisInput * horizontalAcceleration;

        m_acceleration.x = Mathf.Clamp(m_acceleration.x, -maxAcceleration.x, maxAcceleration.x);
        m_acceleration.y = Mathf.Clamp(m_acceleration.y, -maxAcceleration.y, maxAcceleration.y);
    }

    Vector2 FinalizeMove(Vector2 m_moveAmmount)
    {


        if (m_moveAmmount.y < 0.0f)
        {

            if (Mathf.Abs(m_moveAmmount.y) > raycaster.ReturnBottomDistance())
            {
                //Debug.Log(gameObject.name + " IsGoingGrounded");
                m_moveAmmount.y = -(raycaster.ReturnBottomDistance() - skinSize);
                m_currentState = CharacterFSMNames.GROUNDED;
                m_jumpTimer = 0.0f;
                m_jumpNumber = 0;
            }
            else
            {

               m_currentState = CharacterFSMNames.FALLING;
                

            }
        }
        else if (m_moveAmmount.y > 0.0f)
        {
            if (Mathf.Abs(m_moveAmmount.y) > raycaster.ReturnTopDistance())
            {
                m_moveAmmount.y = (raycaster.ReturnTopDistance() - skinSize);
            }
        }


        if (m_moveAmmount.x < 0.0f)
        {
            if (Mathf.Abs(m_moveAmmount.x) > raycaster.ReturnLeftDistance())
            {
                m_moveAmmount.x = -(raycaster.ReturnLeftDistance() - skinSize);
            }
        }
        else if (m_moveAmmount.x > 0.0f)
        {
            if (Mathf.Abs(m_moveAmmount.x) > raycaster.ReturnRightDistance())
            {
                m_moveAmmount.x = (raycaster.ReturnRightDistance() - skinSize);
            }
        }

        if (m_moveAmmount.x > 0 && m_moveAmmount.y > 0)
        {
            if (m_moveAmmount.magnitude > raycaster.CheckDiagonals(DiagonalDirections.TopRight) && !raycaster.CheckTopCollision() && !raycaster.CheckRightCollision())
            {
                Debug.Log(raycaster.CheckDiagonals(DiagonalDirections.TopRight));
                m_moveAmmount.x = 0;
                //m_moveAmmount.y = 0.0f;
            }
        }

        if (m_moveAmmount.x > 0 && m_moveAmmount.y < 0)
        {
            if (m_moveAmmount.magnitude > raycaster.CheckDiagonals(DiagonalDirections.BotRight) && !raycaster.CheckBotCollision() && !raycaster.CheckRightCollision())
            {
                Debug.Log(raycaster.CheckDiagonals(DiagonalDirections.BotRight));
                m_moveAmmount.x = 0;
                //m_moveAmmount.y = 0.0f;
            }
        }

        if (m_moveAmmount.x < 0 && m_moveAmmount.y > 0)
        {
            if (m_moveAmmount.magnitude > raycaster.CheckDiagonals(DiagonalDirections.TopLeft) && !raycaster.CheckTopCollision() && !raycaster.CheckLeftCollision())
            {
                Debug.Log(raycaster.CheckDiagonals(DiagonalDirections.TopLeft));
                m_moveAmmount.x = 0;
                //m_moveAmmount.y = 0.0f;
            }
        }

        if (m_moveAmmount.x < 0 && m_moveAmmount.y < 0)
        {
            if (m_moveAmmount.magnitude > raycaster.CheckDiagonals(DiagonalDirections.BotLeft) && !raycaster.CheckBotCollision() && !raycaster.CheckLeftCollision())
            {
                Debug.Log(raycaster.CheckDiagonals(DiagonalDirections.BotLeft));
                m_moveAmmount.x = 0;
                //m_moveAmmount.y = 0.0f;
            }
        }

        return m_moveAmmount;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("DamageCollider"))
        {
            
            StartCoroutine(Death(true, col.transform.parent.gameObject.GetComponent<Jump_LDO>()._colliderRespawn.gameObject.transform.position));
        }
    }

    public IEnumerator Death(bool doRespawn, Vector3 respawnPoint)
    {
        doRun = false;

        if(!doRespawn)
        {
            yield return new WaitForSeconds(0.0f);
            Destroy(this.gameObject);
        }
        else
        {
            Color tmpCol;

            tmpCol = this.gameObject.GetComponent<SpriteRenderer>().color;

            tmpCol.a = 0.0f;

            this.gameObject.GetComponent<SpriteRenderer>().color = tmpCol;

            yield return new WaitForSeconds(1.0f);
            this.transform.position = respawnPoint + new Vector3(0,0.1f,0);
            m_currentState = CharacterFSMNames.FALLING;
            life--;

            tmpCol = this.gameObject.GetComponent<SpriteRenderer>().color;

            tmpCol.a = 1.0f;

            this.gameObject.GetComponent<SpriteRenderer>().color = tmpCol;

            if(life <= 0)
            {
                Destroy(this.gameObject);
            }
            doRun = true;
        }
    }


}

public enum Directions
{
    up,
    right,
    down,
    left
}

public enum CharacterFSMNames
{
    GROUNDED,
    JUMPING,
    FALLING,
    DOUBLEJUMPING,
    WALLSLIDE,
    GLIDING,
    DASHING
}
