using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AdmobBanner.Instance.DestroyAd();
        AdmobBanner.Instance.RequestBanner();   
    }
}
