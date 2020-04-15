using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetGallery : MonoBehaviour
{

    enum targetType {
        none = 0,
        occupied = 1
    }

    //array representation of the targets. Height of the target is the last value
    private int[,,] targets;
    private int size = 8;
    private bool isActive;

    private int targetsLeft = 0;
    private int currentWave = 0;

    [Header("Settings")]
    public int targetsPerWave = 3;
    public int waves = 5;
    public bool allowArmored;
    public Text infoDisplay;
    [Header("Targets")]
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        cleanArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //subscribe this event to onDestroyed
    public void targetDestroyed(Target origin)
    {

        //unsub from the destroyed target
        origin.onDestroyed -= targetDestroyed;
        if (isActive)
        {
            targetsLeft--;
            updateDisplay();

            if (targetsLeft <= 0)
            {
                

                if(currentWave < waves)
                {
                    currentWave++;
                    nextWave();
                }
                else
                {
                    finish();
                }
                
                
            }
            
        }
    }

    public void begin()
    {
        if (!isActive)
        {
            targetsLeft = 0;
            currentWave = 1;
            isActive = true;
            nextWave();

            updateDisplay();
        }
        
    }
    private void finish()
    {
        Debug.Log("Done!");
        cleanArray();
        isActive = false;
        infoDisplay.text = "You Win!";
    }

    private void nextWave()
    {
        cleanArray();
        while(targetsLeft < targetsPerWave)
        {
            Vector3 position = new Vector3(Random.Range(0, size), Random.Range(0, size), Random.Range(0, 4));
            if (targets[(int)position.x, (int)position.y, (int)position.z] == (int)targetType.none)
            {
                targetsLeft++;

                //offset the positions by this gameobject's position
                position += gameObject.transform.position;

                GameObject obj = Instantiate(target, position, transform.rotation);

                //subsribe to target destroyed event
                obj.GetComponent<Target>().onDestroyed += targetDestroyed;
            }
        }

        updateDisplay();
    }

    private void updateDisplay()
    {
        //write information to player's handheld display
        string str = "Targets: " + targetsLeft + "/" + targetsPerWave + "\n";
        str += "Wave: " + currentWave + "/" + waves + "\n";
        infoDisplay.text = str;
    }

    void cleanArray()
    {
        targets = new int[size, size, 4];
    }
}
