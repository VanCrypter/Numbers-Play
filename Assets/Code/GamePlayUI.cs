using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
   public class GamePlayUI : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _timersViewObject;
        [SerializeField] private GameObject _scoreViewObject;
        public void Show() 
        {
            _mainMenu.SetActive(false);
            _timersViewObject.SetActive(true);
            _scoreViewObject.SetActive(true);
        }

        public void Hide() 
        {
            _mainMenu.SetActive(true);
            _timersViewObject.SetActive(false);
            _scoreViewObject.SetActive(false);
        }
    }
}
