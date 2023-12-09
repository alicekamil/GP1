using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GP1.Gameplay
{
    [Serializable]
    public class ObjectPoolItem
    {
        public LevelObjectSO Object;
        public int Weight;

        public ObjectPoolItem(LevelObjectSO obj, int weight)
        {
            Object = obj;
            Weight = weight;
        }
    }
    
    [CreateAssetMenu(menuName = "Level/Object Pool")]
    public class LevelObjectPoolSO : ScriptableObject
    {
        public LevelObjectSO GetRandom()
        {
            return GetWeightedRandom(_objects);
        }
        
        public LevelObjectSO GetNext(LevelObjectSO previous)
        {
            if (previous == null)
            {
                return GetRandom();
            }
            
            // This is very inefficient, but should be enough for now
            List<ObjectPoolItem> allowedItems = new(_objects);
            foreach (var ignore in previous.IgnoreList.Append(previous))
            {
                int idx = allowedItems.FindIndex(o => o.Object == ignore);
                if (idx >= 0)
                    allowedItems.RemoveAt(idx);
            }

            return GetWeightedRandom(allowedItems);
        }

        private LevelObjectSO GetWeightedRandom(List<ObjectPoolItem> items)
        {
            int[] weights = items.Select(o => o.Weight).ToArray();
            int randomWeight = Random.Range(0, weights.Sum());
            for (int i = 0; i < weights.Length; i++)
            {
                randomWeight -= weights[i];
                if (randomWeight < 0)
                {
                    return items[i].Object;
                }
            }
            return null;
        }

        [SerializeField]
        private List<ObjectPoolItem> _objects;
    }
}