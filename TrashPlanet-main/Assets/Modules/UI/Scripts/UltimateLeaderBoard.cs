using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UltimateLeaderBoard : MonoBehaviour
{
    public TMP_Text EntryText; //name, time

    private List<Entry> _entryList;

    public struct Entry
    {
        public string Name;
        public float Time;
    }

    public void AddEntry(string name, float time)
    {
        Entry newEntry = new Entry { Name = name, Time = time };
        _entryList.Add(newEntry);
        _entryList = _entryList.OrderBy(e => e.Time).ToList(); //order by time, put it in a list again, assign it to _entryLis
    }

    private void Start()
    {
        _entryList = new();
        AddEntry("Alicja", 50f);
        AddEntry("Arseny", 56600f);
        AddEntry("Arrkrog", 1f);
        AddEntry("Arggseny", 5000f);
        Refresh();
    }
    
    // Update text
    private string FormatTime(float time)
    {
        int minutes = (int) time / 60;
        int seconds = (int) time % 60;

        return $"{minutes:D2}:{seconds:D2}";
    }
    
    private void Refresh()
    {
        EntryText.text = "";
        foreach (Entry entry in _entryList)
        {
            string entryName = entry.Name + " " + FormatTime(entry.Time) + "\n";
            EntryText.text += entryName;
        }
    }
}