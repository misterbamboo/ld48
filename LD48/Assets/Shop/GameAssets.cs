using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance { get; private set; }

    [SerializeField] public Sprite healthUpgradeImage;
    [SerializeField] public Sprite lightUpgradeImage;
    [SerializeField] public Sprite oxygenUpgradeImage;
    [SerializeField] public Sprite speedUpgradeImage;
   
    
    private void Awake()
    {
        instance = this;
    }
    
    /*
    public static GameAssets getInstance
    {
        get
        {
            if (instance == null)
            {
                instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                
            }
            return instance;
        }
    }
    */
    
    /*
    public static GameAssets i
    {
        get
        {
            if (instance == null) instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return instance;
        }
    }
    */
}
