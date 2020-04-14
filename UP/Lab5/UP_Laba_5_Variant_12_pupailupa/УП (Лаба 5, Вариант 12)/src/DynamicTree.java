import java.awt.GridLayout;
import java.awt.Toolkit;
import java.io.File;

import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTree;
import javax.swing.event.TreeModelEvent;
import javax.swing.event.TreeModelListener;
import javax.swing.event.TreeSelectionListener;
import javax.swing.tree.DefaultMutableTreeNode;
import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.MutableTreeNode;
import javax.swing.tree.TreePath;
import javax.swing.tree.TreeSelectionModel;

import peripherals.*;

@SuppressWarnings("serial")
public class DynamicTree extends JPanel
{
    protected DefaultMutableTreeNode rootNode;
    protected DefaultTreeModel treeModel;
    protected JTree tree;
    private Toolkit toolkit = Toolkit.getDefaultToolkit();

    public DynamicTree()
    {
        super(new GridLayout(1, 0));

        rootNode = new DefaultMutableTreeNode("peripherals");
        treeModel = new DefaultTreeModel(rootNode);
        treeModel.addTreeModelListener(new MyTreeModelListener());
        tree = new JTree(treeModel);
        tree.setEditable(true);
        tree.getSelectionModel().setSelectionMode(TreeSelectionModel.SINGLE_TREE_SELECTION);
        tree.setShowsRootHandles(true);

        JScrollPane scrollPane = new JScrollPane(tree);
        add(scrollPane);
        
    }

    public Object getLastSelectedPathComponent()
    {
        return tree.getLastSelectedPathComponent();
    }

    public void addTreeSelectionListener(TreeSelectionListener treeSelectionListener)
    {
        tree.addTreeSelectionListener(treeSelectionListener);
    }

    public static DefaultMutableTreeNode p1, p2, p3, p4, p5, c1_1, c1_2, c2_1, c2_2,
            p3_1, c3_1_1, c3_1_2, c3_2, c4_1, c4_2;

   
	public void processFilesFromFolder(String path, DefaultMutableTreeNode p)
	{
		File folder = new File(path);
	    File[] folderEntries = folder.listFiles();
	    for (File entry : folderEntries)
	        if (entry.isFile()){
	        	this.addObject(p, entry.getName().substring(0, entry.getName().length() - 4));
	        }
	}
    
   public void populateTree()
    {
 
        String p1Name = "Classical";
        String p2Name = "Gaming";
        String p3Name = "Ultrabooks";
        String p4Name = "Workstation";

        String c1_1Name = "ACER";
        String c1_2Name = "ASUS";
        
        String c2_1Name = "ASUS";
        String c2_2Name = "MSI";

        String p3_1Name = "Apple";
        String c3_1_1Name = "AirSeries";
        String c3_1_2Name = "ProSeries";
        String c3_2Name = "ASUS";

        String c4_1Name = "ASUS";
        String c4_2Name = "MSI";


        p1 = this.addObject(null, p1Name);
        p2 = this.addObject(null, p2Name);
        p3 = this.addObject(null, p3Name);
        p3_1 = this.addObject(p3, p3_1Name);
        p4 = this.addObject(null, p4Name);
        
        c1_1 = this.addObject(p1, c1_1Name);
        c1_2 = this.addObject(p1, c1_2Name);
        processFilesFromFolder("notebooks\\Classical\\ACER\\", c1_1);
        processFilesFromFolder("notebooks\\Classical\\ASUS\\", c1_2);

        c2_1 = this.addObject(p2, c2_1Name);
        c2_2 = this.addObject(p2, c2_2Name);
        processFilesFromFolder("notebooks\\Gaming\\ASUS\\", c2_1);
        processFilesFromFolder("notebooks\\Gaming\\MSI\\", c2_2);

        c3_1_1 = this.addObject(p3_1, c3_1_1Name);
        c3_1_2 = this.addObject(p3_1, c3_1_2Name);
        c3_2 = this.addObject(p3, c3_2Name);
        processFilesFromFolder("notebooks\\Ultrabooks\\Apple\\AirSeries\\", c3_1_1);
        processFilesFromFolder("notebooks\\Ultrabooks\\Apple\\AirSeries\\", c3_1_2);
        processFilesFromFolder("notebooks\\Ultrabooks\\ASUS\\", c3_2);

        c4_1 = this.addObject(p4, c4_1Name);
        c4_2 = this.addObject(p4, c4_2Name);
        processFilesFromFolder("notebooks\\Workstation\\ASUS\\", c4_1);
        processFilesFromFolder("notebooks\\Workstation\\MSI\\", c4_2);

    }

    /** Remove all nodes except the root node. */
    public void clear()
    {
        rootNode.removeAllChildren();
        treeModel.reload();
    }

    /** Remove the currently selected node. */
    public void removeCurrentNode()
    {
        TreePath currentSelection = tree.getSelectionPath();
        if (currentSelection != null)
        {
            DefaultMutableTreeNode currentNode = (DefaultMutableTreeNode) (currentSelection.getLastPathComponent());
            MutableTreeNode parent = (MutableTreeNode) (currentNode.getParent());
            if (parent != null)
            {
                treeModel.removeNodeFromParent(currentNode);
                return;
            }
        }

        // Either there was no selection, or the root was selected.
        toolkit.beep();
    }

    /** Add child to the currently selected node. */
    public DefaultMutableTreeNode addObject(Object child)
    {
        DefaultMutableTreeNode parentNode = null;
        TreePath parentPath = tree.getSelectionPath();

        if (parentPath == null)
        {
            parentNode = rootNode;
        } else
        {
            parentNode = (DefaultMutableTreeNode) (parentPath.getLastPathComponent());
        }

        return addObject(parentNode, child, true);
    }

    public DefaultMutableTreeNode addObject(DefaultMutableTreeNode parent, Object child)
    {
        return addObject(parent, child, false);
    }

    public DefaultMutableTreeNode addObject(DefaultMutableTreeNode parent, Object child, boolean shouldBeVisible)
    {
        DefaultMutableTreeNode childNode = new DefaultMutableTreeNode(child);

        if (parent == null)
        {
            parent = rootNode;
        }

        // It is key to invoke this on the TreeModel, and NOT
        // DefaultMutableTreeNode
        treeModel.insertNodeInto(childNode, parent, parent.getChildCount());

        // Make sure the user can see the lovely new node.
        if (shouldBeVisible)
        {
            tree.scrollPathToVisible(new TreePath(childNode.getPath()));
        }
        return childNode;
    }

    class MyTreeModelListener implements TreeModelListener
    {
        public void treeNodesChanged(TreeModelEvent e)
        {
            DefaultMutableTreeNode node;
            node = (DefaultMutableTreeNode) (e.getTreePath().getLastPathComponent());

            /*
             * If the event lists children, then the changed node is the child
             * of the node we've already gotten. Otherwise, the changed node and
             * the specified node are the same.
             */

            int index = e.getChildIndices()[0];
            node = (DefaultMutableTreeNode) (node.getChildAt(index));

            System.out.println("The user has finished editing the node.");
            System.out.println("New value: " + node.getUserObject());
        }

        public void treeNodesInserted(TreeModelEvent e)
        {
        }

        public void treeNodesRemoved(TreeModelEvent e)
        {
        }

        public void treeStructureChanged(TreeModelEvent e)
        {
        }
    }
}
