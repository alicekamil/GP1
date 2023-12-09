using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using GP1.UI;

namespace GP1.UI
{
    public class LeaderboardEntryVisual : MonoBehaviour
    {
        public void SetData(string name, float time)
        {
            _nameText.text = name;
            _timeText.text = Timer.FormatTime(time);
        }

        [SerializeField]
        private TMP_Text _nameText;
        [SerializeField]
        private TMP_Text _timeText;

    }
}