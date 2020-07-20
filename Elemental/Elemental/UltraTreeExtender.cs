using Infragistics.Win.UltraWinTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Elemental
{
    public static class UltraTreeExtender
    {
        public static string  OriginalStateColumnKey = "OriginalState";
        private enum ChangesAction { AcceptChanges, RejectChanges }
        public static void AcceptChanges(this UltraTree tree)
        {
            UltraTreeExtender.ApplyAction(tree.Nodes, ChangesAction.AcceptChanges);
        }

        public static void RejectChanges(this UltraTree tree)
        {
            UltraTreeExtender.ApplyAction(tree.Nodes, ChangesAction.RejectChanges);
        }

        private static void ApplyAction(TreeNodesCollection nodes, ChangesAction action )
        {
            if (nodes == null || nodes.Count == 0)
                return; 
            
            var columnSet = nodes.ColumnSetResolved;
            if (columnSet != null && !columnSet.Columns.Exists(UltraTreeExtender.OriginalStateColumnKey))
                return; 
            
            foreach(var node in nodes)
            {
                switch(action)
                {
                    case ChangesAction.AcceptChanges:
                        node.Cells[UltraTreeExtender.OriginalStateColumnKey].Value = node.CheckedState;
                        break;
                    case ChangesAction.RejectChanges:
                        if (node.Cells[UltraTreeExtender.OriginalStateColumnKey].Value != null)
                            node.CheckedState = (CheckState)node.Cells[UltraTreeExtender.OriginalStateColumnKey].Value;
                        break;
                }
                ApplyAction(node.Nodes, action);
            }
        }

    }
}
