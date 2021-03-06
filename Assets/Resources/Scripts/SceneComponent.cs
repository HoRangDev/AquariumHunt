﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ParkJunHo
{
    public class SceneComponent : MonoBehaviour
    {
        static SceneComponent instance = null;
        private Dictionary<string, int> _SceneDic = new Dictionary<string, int>();

        void Awake()
        {
            if(_SceneDic.Count == 0)
            {
                _SceneDic.Add("LogoScene", 0);
                _SceneDic.Add("TitleScene", 1);
                _SceneDic.Add("InGameScene", 3);
                _SceneDic.Add("ResultScene", 2);
            }
        }

        public static SceneComponent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SceneComponent>();
                }

                if (instance == null)
                {
                    instance = new GameObject("SceneComponent", new System.Type[] { typeof(SceneComponent) }).GetComponent<SceneComponent>();
                    instance.Awake();
                }

                return instance;
            }
        }

        public void ScheduleLoadScene(float sec, string sceneName)
        {
            int level = 0;
            _SceneDic.TryGetValue(sceneName, out level);
            //Debug.Log(sceneName + " " + level);
            StartCoroutine(CSceneChange(sec, level));
        }

        private IEnumerator CSceneChange(float sec, int level)
        {
            yield return new WaitForSeconds(sec);

            SceneManager.LoadScene(level);
        }
    }
}
