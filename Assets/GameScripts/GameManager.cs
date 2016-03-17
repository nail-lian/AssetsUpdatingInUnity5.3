﻿using System;
using System.Collections.Generic;
using UnityEngine;
namespace LOM
{
    public class GameManager : IDataListDataProvider
    {
        #region interfaces
        public List<MetaDataForWidget> GetData(int dataCategory)
        {
            List<MetaDataForWidget> meteDataList = new List<MetaDataForWidget>();
            switch (dataCategory)
            {
                case 0: //end game rewards
                    
                    
                    for (int testI = 0; testI < 20; ++testI)
                    {
                        MetaDataForWidget testitem = new MetaDataForWidget();
                        testitem.dataStr1 = "wow"+ testI.ToString();
                        testitem.dataInt1 = testI;
                        meteDataList.Add(testitem);
                    }
                    


                    break;
                default:
                    break;
            }
            return meteDataList;
        }
        #endregion

        #region Singleton
        // Manager is a singleton throughout the game lifecycle.
        private static GameManager _instance = null;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        private GameManager()
        {
        }
        # endregion
       
 
        public void RequestUpdateGame()
        {
            //TODO read option to get client ver
            NetworkManager.Instance.RequestUpdateGameAsync(1, UpdateGame);
        }

        public void UpdateGame(string error, AssetBundleUpdateInfo updateInfo)
        {
            EventManager.RegisterEvent("AssetBundlesDownloaded", GameUpdated, true);
            if (string.IsNullOrEmpty(error))
            {
                EventManager.TriggerEvent(new EventObj("UpdateGame", updateInfo.url, updateInfo.ver));
            }
            else
            {
                GlobalBehaviors.instance.AddAMessageBox("Opps~", "You can't reach the server right now :/ do you want to retry?", MessageBoxType.YESNO,
                       "Retry", "Offline", null, () =>
                       {
                           RequestUpdateGame();
                       }, () =>
                       {
                           //todo read saved last time updateInfo can be an empty manifest application floder
                           //save current gameplay as Offline Mode
                           EventManager.TriggerEvent(new EventObj("OfflineGame", "your saved url", 1));
                       }, null);
            }
        }


        public void GameUpdated(EventObj eo)
        {
            //if (eo.paramInt == 3/*serverVer*/)
            LoadMainMenu();
        }
        private void LoadMainMenu()
        {
            EventManager.RemoveEvent("AssetBundlesDownloaded", GameUpdated);

            var foundAsset = AssetsLoader.Instance.GetAssetEntry("ID_Sc_MainMenu");
            AssetsLoader.Instance.LoadLevel(ref foundAsset);

        }

        public void LogInToFacebook()
        {

        }

        public void PostFBLogin(int successCode)
        {
        }

        public void PostPlayerLogin(string error, Byte[] playerDataBytes)
        {

        }
    }
}