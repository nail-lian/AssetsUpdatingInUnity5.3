﻿namespace LOM
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ButtonItemWidget : MonoBehaviour, IDataListWidget
    {
        public Text ItemNameText, ItemCountText;
        public Image ItemImg;

        public void SetData(MetaDataForWidget mdi)
        {
            ItemNameText.text = mdi.dataStr1;
            ItemCountText.text = mdi.dataInt1.ToString();
            if(mdi.dataImg != null)
                ItemImg = mdi.dataImg;
        }

    }
}
