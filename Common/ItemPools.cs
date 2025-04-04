using IsaacMod.Content.Items.Collectibles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Utilities;

namespace IsaacMod.Common
{
    public class PoolItem<T>
    {
        public float Weight { get; }
        public T Value { get; }

        public PoolItem(float weight, T value)
        {
            if (weight <= 0)
                throw new ArgumentException("Weight must be greater than 0.");

            Weight = weight;
            Value = value;
        }
    }

    public class Pool<T>
    {
        private List<PoolItem<T>> _items = new List<PoolItem<T>>();
        private UnifiedRandom _random = Main.rand;
        private float _totalWeight;

        public void Add(PoolItem<T> item)
        {
            _items.Add(item);
            _totalWeight += item.Weight;
        }

        public PoolItem<T> Draw()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Pool is empty.");

            float randomValue = (float)_random.NextDouble() * _totalWeight;
            float cumulativeWeight = 0;

            foreach (var item in _items)
            {
                cumulativeWeight += item.Weight;
                if (randomValue <= cumulativeWeight)
                    return item;
            }

            // This should never happen if the weights are correctly calculated
            throw new InvalidOperationException("Failed to draw an item from the pool.");
        }

        public PoolItem<T> Draw(UnifiedRandom rand)
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Pool is empty.");

            float randomValue = (float)rand.NextDouble() * _totalWeight;
            float cumulativeWeight = 0;

            foreach (var item in _items)
            {
                cumulativeWeight += item.Weight;
                if (randomValue <= cumulativeWeight)
                    return item;
            }

            throw new InvalidOperationException("Failed to draw an item from the pool.");
        }

        public void Clear()
        {
            _items.Clear();
            _totalWeight = 0;
        }
    }


    public class ItemPools
    {
        public static Pool<int> NormalDrop;

        public static void SetUpPools()
        {
            NormalDrop = new Pool<int>();
            foreach(var item in Collectible.InitList)
            {
                if (item.getItemPools().Contains("normal")){
                    NormalDrop.Add(new PoolItem<int>(item.getWeight(), item.Type));
                }
            }
        }
        public static void Clear()
        {
            NormalDrop = null;
        }
    }
}
