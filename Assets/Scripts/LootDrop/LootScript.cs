using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour {



    [System.Serializable]
    public class LootDrop
    {
        public string name;
        public GameObject item;
        public int DropRate;
    }



    public List<LootDrop> LootList = new List<LootDrop>();

    public int dropChance;

    public void CalculateDrop()
    {
      int calc_DropChance = Random.Range(0, 101);
        if(calc_DropChance > dropChance)
        {
            return;
        }
        if(calc_DropChance <= dropChance)
        {
            int itemWeight = 0;

            for(int i=0; i<LootList.Count; i++)
            {
                itemWeight += LootList[i].DropRate;
            }

            int randomvalue = Random.Range(0, itemWeight);

            for(int j=0; j<LootList.Count; j++)
            {
                if(randomvalue <= LootList[j].DropRate)
                {
                    
                    Instantiate(LootList[j].item, transform.position = new Vector3(transform.position.x, 0.034f, transform.position.z), Quaternion.identity);
                    return;
                }
                randomvalue -= LootList[j].DropRate; //If no item drops then generate a different value

            }


        }


    }



}
