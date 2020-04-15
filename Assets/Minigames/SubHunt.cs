using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubHunt : MonoBehaviour
{

    

    //subs spawn underneath certain red buoys
    public List<Transform> buoys;
    //list that tracks whch spawns have yet to be used this round
    private List<Transform> spawns;

    public GameObject sub;
    public Text info;

    private int subsLeft = 0;
    private int maxSubs;
    private bool isActive = false;
    private float time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        maxSubs = buoys.Count;
        resetSpawns();
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }


    private void nextWave()
    {
        resetSpawns();
        while(subsLeft < maxSubs && spawns.Count > 0)
        {
            subsLeft++;
            int index = Random.Range(0, spawns.Count - 1);
            Transform currSpawn = spawns[index];
            spawns.Remove(currSpawn);

            Vector3 pos = currSpawn.position;
            GameObject obj = Instantiate(sub, new Vector3(pos.x + Random.Range(-2f, 2f), -1, pos.z + Random.Range(-2f, 2f)), Quaternion.identity);

            //subsribe to the sub destroyed event
            obj.GetComponent<Target>().onDestroyed += subDestroyed;
        }
    }

    //subsribe this to sub's onDestroy events
    public void subDestroyed(Target tar)
    {
        tar.onDestroyed -= subDestroyed;
        subsLeft--;
        updateText();

        if(subsLeft <= 0)
        {
            StopAllCoroutines();
            info.text = "Finish!\nTime: " + System.Math.Truncate(time * 100)/100 + "s";
            isActive = false;
        }

    }

    public void begin()
    {
        if (!isActive)
        {
            time = 0f;
            isActive = true;
            nextWave();
            updateText();
        }
        
        
    }

    private void updateText()
    {
        info.text = "Subs Left: " + subsLeft;
    }

    private void resetSpawns()
    {
        spawns = new List<Transform>();
        foreach (Transform t in buoys)
        {
            spawns.Add(t);
        }
    }

 


}
