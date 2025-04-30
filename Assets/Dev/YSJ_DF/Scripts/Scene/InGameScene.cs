using Scripts.Interface;
using Scripts.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Scene
{
    public class InGameScene : SceneBase
    {
        public override SceneID SceneID => SceneID.InGame;

        public override void Initialize()
        {
            base.Initialize();
            GameObject[] SubscribeManagers = GameObject.FindGameObjectsWithTag("Manager");

            ManagerGroup.Instance.RegisterManager(SubscribeManagers);
            ManagerGroup.Instance.InitializeManagers();
        }
    }
}