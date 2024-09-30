﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FarmingEngine
{
    /// <summary>
    /// 显示当前装备的物品
    /// </summary>
    public class EquipPanel : ItemSlotPanel
    {
        private static List<EquipPanel> panel_list = new List<EquipPanel>(); // 存储所有 EquipPanel 实例的列表

        protected override void Awake()
        {
            base.Awake();
            panel_list.Add(this); // 将当前实例添加到列表中
            unfocus_when_out = true; // 当面板外部获得焦点时取消焦点

            Hide(true); // 立即隐藏面板
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            panel_list.Remove(this); // 从列表中移除当前实例
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void InitPanel()
        {
            base.InitPanel();

            if (!IsInventorySet())
            {
                PlayerCharacter player = GetPlayer(); // 获取玩家角色
                if (player != null)
                {
                    bool has_inventory = PlayerData.Get().HasInventory(player.player_id); // 检查玩家是否有背包
                    if (has_inventory)
                    {
                        // 设置装备背包和玩家
                        SetInventory(InventoryType.Equipment, player.EquipData.uid, player.EquipData.size);
                        SetPlayer(player);
                        Show(true); // 显示面板
                    }
                }
            }
        }

        protected override void RefreshPanel()
        {
            InventoryData inventory = GetInventory(); // 获取背包数据

            if (inventory != null)
            {
                // 遍历所有槽位
                for (int i = 0; i < slots.Length; i++)
                {
                    EquipSlotUI slot = (EquipSlotUI)slots[i]; // 获取装备槽位
                    if (slot != null)
                    {
                        InventoryItemData invdata = inventory.GetInventoryItem((int)slot.equip_slot); // 获取槽位中的物品数据
                        ItemData idata = ItemData.Get(invdata?.item_id); // 获取物品数据

                        if (invdata != null && idata != null)
                        {
                            // 设置槽位数据、耐久度和过滤器
                            slot.SetSlot(idata, invdata.quantity, selected_slot == slot.index || selected_right_slot == slot.index);
                            slot.SetDurability(idata.GetDurabilityPercent(invdata.durability), ShouldShowDurability(idata, invdata.durability));
                            slot.SetFilter(GetFilterLevel(idata, invdata.durability));
                        }
                        else
                        {
                            slot.SetSlot(null, 0, false); // 如果没有物品数据，清空槽位
                        }
                    }
                }
            }
        }

        public static EquipPanel Get(int player_id = 0)
        {
            foreach (EquipPanel panel in panel_list)
            {
                PlayerCharacter player = panel.GetPlayer(); // 获取面板上的玩家角色
                if (player != null && player.player_id == player_id)
                    return panel; // 返回对应玩家 ID 的面板
            }
            return null; // 如果没有找到则返回 null
        }

        public static new List<EquipPanel> GetAll()
        {
            return panel_list; // 返回所有 EquipPanel 实例的列表
        }
    }
}
