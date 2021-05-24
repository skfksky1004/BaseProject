using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// 에디터 툴관련 이걸보고 진행(트리뷰)
/// https://docs.unity.cn/kr/2018.4/Manual/TreeViewAPI.html
/// </summary>

public class TreeView_Simple : TreeView
{
    private static TreeViewItem root = new TreeViewItem() {id = 0, depth = -1, displayName = "Root"};
    private static List<TreeViewItem> items = new List<TreeViewItem>();
    
    public TreeView_Simple(TreeViewState state) : base(state)
    {
        Reload();
    }

    public TreeView_Simple(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader)
    {
        Reload();
    }

    protected override TreeViewItem BuildRoot()
    {
        SetupParentsAndChildrenFromDepths(root,items);

        return root;
    }

    
    public static void AddTreeViewItem(string displayName, int depth = 0)
    {
        int itemLength = items.Count;
        items.Add(new TreeViewItem(itemLength + 1, depth, displayName));
    }

    public static void DelTreaVieItem(string displayName)
    {
        int delIndex = items.FindIndex(x => x.displayName == displayName);
        items.RemoveAt(delIndex);
    }
    
    public static void DelTreaVieItem(int id)
    {
        int delIndex = items.FindIndex(x => x.id == id);
        items.RemoveAt(delIndex);
    }
}
