using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserPointer : MonoBehaviour
{
    [Header("LaserAttributes")]
    public Material glowMaterial;
    public LineRenderer laser;
    public Transform emissionPoint;

    [Header("Missile Attributes")]
    public Transform missileSpawnPoint;
    public GameObject missile;
    public float reloadTime = 10.0f;
    //[Tooltip("Make sure text is 1st child of canvas")]
    public Text reloadDisplay;

    private bool active = false;
    private GameObject target = null;
    private Color c;
    private float reload;


    // Start is called before the first frame update
    void Start()
    {
        reloadDisplay.CrossFadeAlpha(0f, 0f, true);
        Color c = glowMaterial.color;
        laser.positionCount = 2;
        laser.SetPosition(0, emissionPoint.position);
        laser.SetPosition(1, emissionPoint.position + emissionPoint.transform.forward * 1000);
    }

    private void Update()
    {
        if(reload > 0)
        {
            reload -= Time.deltaTime;

            if(reload <= 0)
            {
                reload = 0;
                //fade out the text once reload completes
                reloadDisplay.CrossFadeAlpha(0f, 1f, false);
            }
            reloadDisplay.text = "Reload:\n" + (System.Math.Truncate(reload * 100) / 100f);

        }

        if (active)
        {
            laser.SetPosition(0, emissionPoint.position);

            //If raycast hits something, draw second endpoint of line renderer there
            //Otherwise draw it at 1000 units in front
            RaycastHit hit;
            if(Physics.Raycast(emissionPoint.position, emissionPoint.transform.forward, out hit, 1000))
            {
                laser.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag.Equals("Target") && (target == null)){
                    lockOn(hit.collider.gameObject);
                }
            }
            else
            {
                laser.SetPosition(1, emissionPoint.position + emissionPoint.transform.forward * 1000);
                if (target != null)
                {
                    unlock();
                }
            }

            
            
        }
    }

    public void launchMissile()
    {
        if(target != null && reload <= 0.0f)
        {
            Debug.Log("missile away!");
            GameObject missileObj = Instantiate(missile, missileSpawnPoint.position, missileSpawnPoint.rotation);
            missileObj.GetComponent<Missile>().setTarget(target);
            reload = reloadTime;

            //make reload timer visible
            reloadDisplay.CrossFadeAlpha(1f, 0f, true);
        }

    }


    //lock on after pointing the beam at an appropriate target
    public void lockOn(GameObject obj)
    {
        glowMaterial.EnableKeyword("_EMISSION");
        target = obj;
    }

    public void unlock()
    {
        glowMaterial.DisableKeyword("_EMISSION");
        target = null;
    }


    //beam turns on when you pick up the laser
    public void beamOn()
    {
        laser.enabled = true;
        active = true;

        if(reload > 0)
        {
            reloadDisplay.CrossFadeAlpha(1f, 0f, true);
        }
        
    }

    public void beamOff()
    {
        laser.enabled = false;
        active = false;
        reloadDisplay.CrossFadeAlpha(0f, 0f, true);

    }
}
