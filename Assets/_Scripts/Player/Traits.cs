// using System.Reflection;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Traits : MonoBehaviour
// {
//     public PlayerStats playerStats;
    //PlayerTraits TEMPLATE = new PlayerTraits("TEMPLATE",0,0,0,0,0,0,false);
    // public PlayerTraits scared = new PlayerTraits("Scared",-1,0,0,-1,1,0,false);
    // public PlayerTraits brave = new PlayerTraits("Brave",1,0,0,0,-1,1,false);
    // public PlayerTraits[] traitArray;
    //void Awake()
    //{   
        //int i = 0;
        //traitArray = GetVariablesOfType<PlayerTraits>();
        //foreach(PlayerTraits playerTraits in traitArray)
        // {
        //     if(!traitArray[i].traitBool) 
        //     {
        //         playerStats.Traits.Add(traitArray[i].traitName,traitArray[i]);
        //     }
        //     Debug.Log(playerStats.Traits.Count);
        //     Debug.Log(playerStats.Traits.Keys);
        //     Debug.Log(playerStats.Traits.Values);
        //     i++;
        // }
    //}
// public T[] GetVariablesOfType<T>()
// {
//     FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//     T[] variables = new T[fields.Length];
//     int count = 0;
//     foreach (FieldInfo field in fields)
//     {
//         if (field.FieldType == typeof(T))
//         {
//             variables[count] = (T)field.GetValue(this);
//             count++;
//         }
//     }
//     System.Array.Resize(ref variables, count);
//     return variables;
// }
// }
