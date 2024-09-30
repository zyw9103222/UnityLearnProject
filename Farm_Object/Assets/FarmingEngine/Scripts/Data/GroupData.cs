﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingEngine
{

    /// <summary>
    /// 本身并不执行任何操作，但它用于将对象作为一个组引用，而不是逐个引用它们。
    /// 例如：所有能够砍伐树木的工具可以归为“CutTree”组，而树木可能有一个需要玩家持有“CutTree”物品的需求。
    /// 这样做可以避免每次创建新斧头类型时都需要将新物品引用到每棵树上。只需将新斧头添加到已经附加到每棵现有树上的组即可。
    /// </summary>

    [CreateAssetMenu(fileName = "GroupData", menuName = "FarmingEngine/GroupData", order = 1)]
    public class GroupData : ScriptableObject
    {
        public string group_id; // 组标识符
        public string title; // 标题
        public Sprite icon; // 图标
    }

}