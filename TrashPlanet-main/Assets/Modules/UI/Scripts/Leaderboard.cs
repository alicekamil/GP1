using System.Linq;
using TMPro;
using UnityEngine;

namespace GP1.UI
{
    public class Leaderboard : MonoBehaviour
    {
        public float GetBestTime() => _data.Entries.Count > 0 ? _data.Entries[0].Time : 0f; 
        
        public void AddEntry(string name, float time)
        {
            LeaderboardEntry entry = new LeaderboardEntry { Name = name, Time = time };
            _data.Entries.Add(entry);
            _data.Entries = _data.Entries.OrderBy(e => e.Time).ToList();
            SaveEntires();
            RefreshEntries();
        }

        public void Reset()
        {
            _data = new LeaderboardData();
            SaveEntires();
            RefreshEntries();
        }
        
        private void Start()
        {
            LoadEntries();
            RefreshEntries();
        }

        private void SaveEntires()
        {
            string json = JsonUtility.ToJson(_data);
            PlayerPrefs.SetString("leaderboard", json);
            PlayerPrefs.Save();
        }

        private void LoadEntries()
        {
            if (!PlayerPrefs.HasKey("leaderboard"))
            {
                _data = new LeaderboardData();
                return;
            }
            string json = PlayerPrefs.GetString("leaderboard");
            _data = JsonUtility.FromJson<LeaderboardData>(json);
        }

        private void RefreshEntries()
        {
            // Destroy all entries
            foreach (Transform entry in _entryContainer)
            {
                Destroy(entry.gameObject);
            }

            // Create new visual entry for each entry in the list
            for (int i = 0; i < Mathf.Min(_data.Entries.Count, 10); i++)
            {
                LeaderboardEntry entry = _data.Entries[i];
                var newVisualEntry = Instantiate(_entryPrefab, _entryContainer);
                newVisualEntry.SetData(entry.Name, entry.Time);
            }
        }

        private LeaderboardData _data;
        [SerializeField]
        private Transform _entryContainer;
        [SerializeField]
        private LeaderboardEntryVisual _entryPrefab;
    }
}