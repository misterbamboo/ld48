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
    [SerializeField] public Sprite hookUpgradeImage;
    [SerializeField] public Sprite hullUpgradeImage;
    
    private void Awake()
    {
        instance = this;
    }
    
    
}
