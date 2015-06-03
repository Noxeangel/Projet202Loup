using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaycasterBahaviour : MonoBehaviour {

    public int botRayNumber = 2;
    public int topRayNumber = 2;
    public int sideRayNumber = 2;

    public LayerMask groundCollisionLayer;
    //public LayerMask wallCollisionLayer;

    private BoxCollider collider;

    private List<Ray> BotRayOrigin = new List<Ray>();
    private List<Ray> TopRayOrigin = new List<Ray>();
    private List<Ray> LeftRayOrigin = new List<Ray>();
    private List<Ray> RightRayOrigin = new List<Ray>();

    private List<Ray> DiagonalRays = new List<Ray>();

    public float skinSize = 0.1f;

	// Use this for initialization
	void Awake () {
        collider = this.gameObject.GetComponent<BoxCollider>();

        GenerateRays();

	}
	
	// Update is called once per frame
	void Update () {
        //UpdateRays();
	}

    void GenerateRays()
    {
        // Generate bottom Rays

        float deltaX = collider.bounds.size.x / (botRayNumber - 1);

        for (int i = 0; i < botRayNumber; i++)
        {
            BotRayOrigin.Add(new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2 + deltaX * i , -collider.bounds.size.y / 2 - skinSize, 0), Vector3.down));
        }

        deltaX = collider.bounds.size.x / (topRayNumber - 1);

        for (int i = 0; i < topRayNumber; i++)
        {
            TopRayOrigin.Add(new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2 + deltaX * i , collider.bounds.size.y / 2 + skinSize, 0), Vector3.up));
        }

        float deltaY = collider.bounds.size.y / (sideRayNumber - 1);

        for (int i = 0; i < sideRayNumber; i++)
        {
            LeftRayOrigin.Add(new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2 - skinSize, -collider.bounds.size.y / 2 + deltaY * i , 0), Vector3.left));
            Debug.DrawRay(LeftRayOrigin[i].origin, LeftRayOrigin[i].direction);
        }

        for (int i = 0; i < sideRayNumber; i++)
        {
            RightRayOrigin.Add(new Ray(collider.bounds.center + new Vector3(collider.bounds.size.x / 2 + skinSize, -collider.bounds.size.y / 2 + deltaY * i , 0), Vector3.right));
            Debug.DrawRay(RightRayOrigin[i].origin, RightRayOrigin[i].direction);
        }

        for(int i =0; i < 4; i ++)
        {
            DiagonalRays.Add(new Ray());
        }

        DiagonalRays[(int)DiagonalDirections.TopLeft] = new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2, collider.bounds.size.y / 2, 0), new Vector3(-1,1,0));
        DiagonalRays[(int)DiagonalDirections.TopRight] = new Ray(collider.bounds.center + new Vector3(collider.bounds.size.x / 2, collider.bounds.size.y / 2, 0), new Vector3(1, 1, 0));
        DiagonalRays[(int)DiagonalDirections.BotLeft] = new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2, -collider.bounds.size.y / 2, 0), new Vector3(-1, -1, 0));
        DiagonalRays[(int)DiagonalDirections.BotRight] = new Ray(collider.bounds.center + new Vector3(collider.bounds.size.x / 2, -collider.bounds.size.y / 2, 0), new Vector3(1, -1, 0));
    }

    public void UpdateRays()
    {
        // Generate bottom Rays

        float deltaX = collider.bounds.size.x / (botRayNumber - 1);
        
        for (int i = 0; i < botRayNumber; i++)
        {
            BotRayOrigin[i] = (new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2 + deltaX * i, -collider.bounds.size.y / 2 /*- skinSize*/, 0), Vector3.down));
            Debug.DrawRay(BotRayOrigin[i].origin, BotRayOrigin[i].direction);
        }

        deltaX = collider.bounds.size.x / (topRayNumber - 1);

        for (int i = 0; i < topRayNumber; i++)
        {
            TopRayOrigin[i] = (new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2 + deltaX * i, collider.bounds.size.y / 2 /*+ skinSize*/, 0), Vector3.up));
            Debug.DrawRay(TopRayOrigin[i].origin, TopRayOrigin[i].direction);
        }

        float deltaY = collider.bounds.size.y / (sideRayNumber - 1);

        for (int i = 0; i < sideRayNumber; i++)
        {
            LeftRayOrigin[i] = (new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2 - skinSize, -collider.bounds.size.y / 2 + deltaY * i, 0), Vector3.left));
            Debug.DrawRay(LeftRayOrigin[i].origin, LeftRayOrigin[i].direction);
        }

        for (int i = 0; i < sideRayNumber; i++)
        {
            RightRayOrigin[i] = (new Ray(collider.bounds.center + new Vector3(collider.bounds.size.x / 2 + skinSize, -collider.bounds.size.y / 2 + deltaY * i, 0), Vector3.right));
            Debug.DrawRay(RightRayOrigin[i].origin, RightRayOrigin[i].direction);
        }

        DiagonalRays[(int)DiagonalDirections.TopLeft] = new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2, collider.bounds.size.y / 2, 0), new Vector3(-1, 1, 0));
        DiagonalRays[(int)DiagonalDirections.TopRight] = new Ray(collider.bounds.center + new Vector3(collider.bounds.size.x / 2, collider.bounds.size.y / 2, 0), new Vector3(1, 1, 0));
        DiagonalRays[(int)DiagonalDirections.BotLeft] = new Ray(collider.bounds.center + new Vector3(-collider.bounds.size.x / 2, -collider.bounds.size.y / 2, 0), new Vector3(-1, -1, 0));
        DiagonalRays[(int)DiagonalDirections.BotRight] = new Ray(collider.bounds.center + new Vector3(collider.bounds.size.x / 2, -collider.bounds.size.y / 2, 0), new Vector3(1, -1, 0));

        for(int i = 0 ; i < 4 ; i ++)
        {
            Debug.DrawRay(DiagonalRays[i].origin, DiagonalRays[i].direction);
        }

    }

    #region CheckFunctions

    public bool CheckBotCollision()
    {
        RaycastHit hit;
        for (int i = 0; i < botRayNumber; i++)
        {
            if (Physics.Raycast(BotRayOrigin[i], out hit, 3.0f, groundCollisionLayer.value))
            {
                if (hit.distance < 0.05f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckTopCollision()
    {
        RaycastHit hit;
        for (int i = 0; i < topRayNumber; i++)
        {
            if (Physics.Raycast(TopRayOrigin[i], out hit, 3.0f, groundCollisionLayer.value))
            {
                if (hit.distance < 0.05f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckLeftCollision()
    {
        RaycastHit hit;
        for (int i = 0; i < sideRayNumber; i++)
        {
            if (Physics.Raycast(LeftRayOrigin[i], out hit, 3.0f, groundCollisionLayer.value))
            {
                if (hit.distance < 0.05f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckRightCollision()
    {
        RaycastHit hit;
        for (int i = 0; i < sideRayNumber; i++)
        {
            if (Physics.Raycast(RightRayOrigin[i], out hit, 3.0f, groundCollisionLayer.value))
            {
                if (hit.distance < 0.05f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion
    #region DistanceFunctions


    public float ReturnBottomDistance()
    {
        float distanceToGround = 10.0f;
        RaycastHit hit;
        for (int i = 0; i < botRayNumber; i ++ )
        {
            if(Physics.Raycast(BotRayOrigin[i], out hit, 3.0f, groundCollisionLayer.value))
            {
                if(hit.distance < distanceToGround)
                {
                    distanceToGround = hit.distance;
                }
            }
        }
        
        return distanceToGround;
    }

    public float ReturnTopDistance()
    {
        float distanceToTop = 3.0f;
        RaycastHit hit;
        for (int i = 0; i < topRayNumber; i++)
        {
            if (Physics.Raycast(TopRayOrigin[i], out hit, 3.0f, groundCollisionLayer.value))
            {
                if (hit.distance < distanceToTop)
                {
                    distanceToTop = hit.distance;
                }
            }
        }

        return distanceToTop;
    }

    public float ReturnLeftDistance()
    {
        float distanceToLeft = 3.0f;
        RaycastHit hit;
        for (int i = 0; i < sideRayNumber; i++)
        {
            if (Physics.Raycast(LeftRayOrigin[i], out hit, 3.0f, groundCollisionLayer.value))
            {
                if (hit.distance < distanceToLeft)
                {
                    distanceToLeft = hit.distance;
                }
            }
        }

        return distanceToLeft;
    }

    public float ReturnRightDistance()
    {
        float distanceToRight = 3.0f;
        RaycastHit hit;
        for (int i = 0; i < sideRayNumber; i++)
        {
            if (Physics.Raycast(RightRayOrigin[i], out hit, 3.0f, groundCollisionLayer.value))
            {
                if (hit.distance < distanceToRight)
                {
                    distanceToRight = hit.distance;
                }
            }
        }

        return distanceToRight;
    }
    
    public float CheckDiagonals(DiagonalDirections d_dir)
    {
        float diagDistance = Mathf.Sqrt(10.0f);
        RaycastHit hit;

        if(Physics.Raycast(DiagonalRays[(int) d_dir], out hit, Mathf.Sqrt(10.0f), groundCollisionLayer.value))
        {
            if(hit.distance < diagDistance)
            {

                diagDistance = hit.distance;
            }
        }
        
        return diagDistance;
    }
    
    #endregion
}
public enum DiagonalDirections
{
    TopRight,
    TopLeft,
    BotRight,
    BotLeft
}
