using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSheet : MonoBehaviour
{
    
    public IDictionary<TMP_Text,int> Stats = new Dictionary<TMP_Text, int>();
    [SerializeField] private TMP_Text[] statText;
    [SerializeField] int baseStat = 5;
    // Start is called before the first frame update
    void Start()
    {
        foreach(TMP_Text text in statText)
        {
            int i = 0;
            statText[i].SetText(baseStat.ToString());
            Stats.Add(statText[i],baseStat);
            i++;
        }
    }
    public void posIncrement(TMP_Text test)
    {
        if(Stats.ContainsKey(test))
        {
            Stats[test]++;
            test.SetText(Stats[test].ToString());
            
        }
    }
    public void negIncrement(TMP_Text test)
    {
        if(Stats.ContainsKey(test))
        {
            Stats[test]--;
            test.SetText(Stats[test].ToString());
            
        }
    }
}
