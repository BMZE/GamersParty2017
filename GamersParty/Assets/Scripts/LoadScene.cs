using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	[SerializeField]
    [Tooltip("Nombre de la escena que se va a cargar")]
    string sceneName;

	void OnTriggerEnter2D(Collider2D coll)
	{
        if (coll.tag == "Player")
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {                
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

               
            }
            


        }
        



        //Application.LoadLevelAdditive (level);
    }




}
