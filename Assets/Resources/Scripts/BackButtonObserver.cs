using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ParkJunHo
{
    public class BackButtonObserver : MonoBehaviour
    {
        private bool mBackPressed = false;
        private BackText mBackText = null;

        // Use this for initialization
        void Start()
        {
            mBackText = FindObjectOfType<BackText>();
            DontDestroyOnLoad(this);
        }

        void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        void OnLevelFinishedLoading(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            mBackText = FindObjectOfType<BackText>();
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (mBackPressed)
                {
                    Debug.Log("Quit Game");
                    Application.Quit();
                }
                else
                {
                    StopAllCoroutines();
                    mBackPressed = true;
                    //스프라이트 띄우기
                    mBackText.ShowText();

                    StartCoroutine(RelieveBackButton());
                }
            }
        }

        private IEnumerator RelieveBackButton()
        {
            yield return new WaitForSeconds(1.0f);

            mBackPressed = false;
        }
    }
}
