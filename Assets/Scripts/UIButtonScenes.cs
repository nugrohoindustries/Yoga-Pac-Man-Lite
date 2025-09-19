/*  This file is part of the "Simple IAP System" project by Rebound Games.
 *  You are only allowed to use these resources if you've bought them from the Unity Asset Store.
 * 	You shall not license, sublicense, sell, resell, transfer, assign, distribute or
 * 	otherwise make available to any third party the Service or the Content. */

using UnityEngine;
using UnityEngine.SceneManagement;

    /// <summary>
    /// simple script will load the assigned scene
    /// </summary>
    public class UIButtonScenes : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
		//	Advertisements.Instance.ShowInterstitial();
            if (!string.IsNullOrEmpty(sceneName))
		    {
			    
				    SceneManager.LoadScene(sceneName);
			   
		    }
        }
    }

