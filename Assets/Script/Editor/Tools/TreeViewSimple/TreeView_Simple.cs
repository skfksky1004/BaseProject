using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// 에디터 툴관련 이걸보고 진행(트리뷰)
/// https://docs.unity.cn/kr/2018.4/Manual/TreeViewAPI.html
/// </summary>

public class TreeView_Simple : TreeView
{
    public enum ContextID
    {
        Create,
        AddChild,
        Delete,
        DeleteAll,
        InScript,
        
        Max
    };

    private static TreeViewItem root = new TreeViewItem() {id = 0, depth = -1, displayName = "Root"};
    private static List<TreeViewItem> items = new List<TreeViewItem>();
    
    public bool IsSelection() => HasSelection();

    private int _accCount = 0; 
    
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
    
    public void OnTreeView_Func()
    {
        GenericMenu menu = new GenericMenu();

        if (IsSelection())
        {
            menu.AddItem(new GUIContent(ContextID.AddChild.ToString()), false, OnContextCallback, ContextID.AddChild);
            menu.AddItem(new GUIContent(ContextID.Delete.ToString()), false, OnContextCallback, ContextID.Delete);
            menu.AddItem(new GUIContent(ContextID.InScript.ToString()), false, OnContextCallback, ContextID.InScript);
        }
        else
        {
            menu.AddItem(new GUIContent(ContextID.Create.ToString()), false, OnContextCallback, ContextID.Create);
            menu.AddItem(new GUIContent(ContextID.DeleteAll.ToString()), false, OnContextCallback, ContextID.DeleteAll);
        }
        
        menu.ShowAsContext();
    }

    private void OnContextCallback(object obj)
    {
        var contextId = (ContextID) obj;
        switch (contextId)
        {
            case ContextID.Create:     //  뎁스 0 생성
            {
                AddTreeViewItem($"Test{++_accCount}", 0);
                break;
            }
            case ContextID.AddChild:    //  하위에 추가
            {
                TreeViewItem parentItem = null;
                for (int i = 0; i < items.Count; i++)
                {
                    if (IsSelected(items[i].id))
                    {
                        parentItem = items[i];
                    }
                }

                if (parentItem != null)
                {
                    AddTreeViewItem($"Test{++_accCount}", parentItem.depth + 1);
                }
                
                break;
            }
            case ContextID.Delete:    //  개별 삭제
            case ContextID.DeleteAll: //  전체 삭제
            {
                List<int> arrID = new List<int>();
                for (int i = 0; i < items.Count; i++)
                {
                    if ((contextId == ContextID.Delete && IsSelected(items[i].id)) ||
                         contextId == ContextID.DeleteAll)
                    {
                        arrID.Add(items[i].id);
                    }
                }

                foreach (var id in arrID)
                {
                    DelTreaVieItem(id);
                }
                
                break;
            }
            case ContextID.InScript:    //  스크립트에 추가(작업 예정)
            {
                //  
                break;
            }
            default:
                break;
        }

        BuildRoot();
        
        Reload();
    }
}
