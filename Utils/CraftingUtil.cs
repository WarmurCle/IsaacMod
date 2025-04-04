using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace IsaacMod.Utils
{
    public class CraftingUtil
    {// 定义类型及其对应的价值
        enum ItemType
        {
            Heart = 35,
            Star = 25,
            CopperCoin = 7,
            SilverCoin = 19,
            GoldCoin = 33,
            PCoin = 67
        }

        static void ini(string[] args)
        {
            // 创建一个随机数生成器
            Random rand = new Random();

            // 示例：8个位置，每个位置随机选择一个ItemType
            List<ItemType> items = GenerateRandomItems(rand);

            // 计算总价值
            int totalValue = CalculateTotalValue(items);

            // 计算最终的随机数值，根据总价值
            int randomValue = GetRandomValue(rand, totalValue, 1, 100); // 假设1到100之间的随机数值

            // 打印结果
            Console.WriteLine("Items: " + string.Join(", ", items));
            Console.WriteLine("Total Value: " + totalValue);
            Console.WriteLine("Generated Random Value: " + randomValue);
        }

        // 生成随机类型
        static List<ItemType> GenerateRandomItems(Random rand)
        {
            List<ItemType> items = new List<ItemType>();
            Array itemTypes = Enum.GetValues(typeof(ItemType)); // 获取所有ItemType类型

            // 随机选择8个类型
            for (int i = 0; i < 8; i++)
            {
                ItemType randomItem = (ItemType)itemTypes.GetValue(rand.Next(itemTypes.Length));
                items.Add(randomItem);
            }

            return items;
        }

        // 计算总价值
        static int CalculateTotalValue(List<ItemType> items)
        {
            return items.Sum(item => (int)item); // 将每个ItemType的价值加起来
        }

        // 根据总价值计算随机数
        static int GetRandomValue(Random rand, int totalValue, int min, int max)
        {
            // 计算总价值比例
            double valueFactor = totalValue / 100.0; // 这里假设总价值影响的是1到100范围的随机数

            // 生成基于价值的加权随机数
            int randomValue = (int)(rand.NextDouble() * (max - min) * valueFactor) + min;

            // 保证随机数在有效范围内
            return Math.Clamp(randomValue, min, max);
        }
    }
}
