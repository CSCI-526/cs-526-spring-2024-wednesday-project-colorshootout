﻿using Unity.FPS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.FPS
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string SceneName = "";

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject
                && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }
        }

        public void LoadTargetScene()
        {
            SceneManager.LoadScene(SceneName);
        }

        public void LoadCurrLevelScene()
        {
            SceneManager.LoadScene(LevelManager.levels[LevelManager.currLevel]);
        }

        public void LoadNextLevelScene()
        {
            LevelManager.GoToNextLevel();
            SceneManager.LoadScene(LevelManager.levels[LevelManager.currLevel]);
        }

        public void StartScene()
        {
            LevelManager.currLevel = 0;
            SceneManager.LoadScene(LevelManager.levels[LevelManager.currLevel]);
        }
    }
}