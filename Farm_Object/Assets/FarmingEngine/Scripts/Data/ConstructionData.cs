﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingEngine
{
    /// <summary>
    /// 建筑数据文件
    /// </summary>

    [CreateAssetMenu(fileName = "ConstructionData", menuName = "FarmingEngine/ConstructionData", order = 4)]
    public class ConstructionData : CraftData
    {
        [Header("--- ConstructionData ------------------")]

        public GameObject construction_prefab; // 构建建筑时生成的预制体

        [Header("引用数据")]
        public ItemData take_item_data; // 可获取的物品数据，用于可以拾取的建筑（例如陷阱、诱饵）

        [Header("属性")]
        public DurabilityType durability_type; // 耐久类型
        public float durability; // 耐久度

        private static List<ConstructionData> construction_data = new List<ConstructionData>(); // 建筑数据列表

        public bool HasDurability()
        {
            return durability_type != DurabilityType.None && durability >= 0.1f;
        }

        public static new void Load(string folder = "")
        {
            construction_data.Clear();
            construction_data.AddRange(Resources.LoadAll<ConstructionData>(folder));
        }

        public new static ConstructionData Get(string construction_id)
        {
            foreach (ConstructionData item in construction_data)
            {
                if (item.id == construction_id)
                    return item;
            }
            return null;
        }

        public new static List<ConstructionData> GetAll()
        {
            return construction_data;
        }
    }

}