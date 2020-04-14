package com.company;
import javax.swing.*;
import javax.swing.tree.TreePath;
import java.awt.*;
import java.util.ArrayList;
import java.util.Enumeration;

public class PhotoStoreFrame extends JFrame {

    private static final long serialVersionUID = 1L;
    public static PhotoStoreNode addResult = null;
    public static String path = null;
    JTable infoPanel = new JTable();
    JTree notebooksTree = new JTree();
    PhotoStoreTableModel tableModel = null;
    PhotoStoreTreeModel treeModel = null;

    public PhotoStoreFrame() throws HeadlessException {

        JButton addButton = new JButton("Add");
        addButton.addActionListener(e -> openAddDialog());

        JButton removeButton = new JButton("Remove");
        removeButton.addActionListener(e -> removeItem());

        tableModel = new PhotoStoreTableModel();
        infoPanel = new JTable(tableModel);
        treeModel = new PhotoStoreTreeModel(new PhotoStoreTreeNode("Directory"));
        notebooksTree = new JTree(treeModel);
        notebooksTree.addTreeSelectionListener(e -> {
            PhotoStoreTreeNode node = (PhotoStoreTreeNode) notebooksTree.getLastSelectedPathComponent();
            if (node == null) {
                return;
            }
            ArrayList<PhotoStoreNode> array = node.getAllLeaves();
            tableModel = new PhotoStoreTableModel(array);
            infoPanel.setModel(tableModel);
        });
        JSplitPane splitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT, true, new JScrollPane(notebooksTree), new JScrollPane(infoPanel));
        splitPane.setDividerLocation(300);

        //path = "input.txt";
        //readInfo(path);

        getContentPane().add(splitPane);
        getContentPane().add("North", addButton);
        getContentPane().add("South", removeButton);
        setBounds(100, 100, 600, 600);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setVisible(true);
    }

    private void openAddDialog() {
        AddForm addForm = new AddForm(this);
        addForm.setVisible(true);
    }

    void addNewItem() {
        PhotoStoreTreeNode temp, root = treeModel.getRoot(), insert, where;

        if (addResult != null) {
            try {
                if (findNode(root, addResult.getShopPart()) == null) {
                    // нет нужного отдела
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(addResult.getShopPart()), root, root.getChildCount(), false);
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(addResult.getGenre()), temp = (findNode(root, addResult.getShopPart())), temp.getChildCount(), false);
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(addResult.getName()), temp = (findNode(temp, addResult.getGenre())), temp.getChildCount(), false);
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(Integer.toString(addResult.getPrice()), true), temp = (findNode(temp, addResult.getName())), temp.getChildCount(), false);

                } else if (findNode(root, addResult.getGenre()) == null) { //отдел есть, жанра нет
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(addResult.getGenre()), temp = (findNode(root, addResult.getShopPart())), temp.getChildCount(), false);
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(addResult.getName()), temp = (findNode(root, addResult.getGenre())), temp.getChildCount(), false);
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(Integer.toString(addResult.getPrice()), true), temp = (findNode(root, addResult.getName())), temp.getChildCount(), false);
                } else if (findNode(root, addResult.getName()) == null) {
                    temp = findNode(root, addResult.getShopPart());
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(addResult.getName()), temp = (findNode(temp, addResult.getGenre())), temp.getChildCount(), false);
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(Integer.toString(addResult.getPrice()), true), temp = (findNode(temp, addResult.getName())), temp.getChildCount(), false);
                } else {
                    temp = findNode(root, addResult.getShopPart());
                    temp = findNode(temp, addResult.getGenre());
                    treeModel.insertNodeInto(new PhotoStoreTreeNode(Integer.toString(addResult.getPrice()), true), temp = (findNode(temp, addResult.getName())), temp.getChildCount(), false);
                }
            } catch (Exception e) {
                path = null;
                addResult = null;
                return;
            }
        }
        /*else
            readInfo(path);
        path = null;*/
        addResult = null;
    }

    public void removeItem() {
        TreePath currentSelection = notebooksTree.getSelectionPath();
        if (currentSelection != null) {
            PhotoStoreTreeNode currentNode = (PhotoStoreTreeNode) (currentSelection.getLastPathComponent());
            PhotoStoreTreeNode parent = (PhotoStoreTreeNode) (currentNode.getParent());
            if (parent != null) {
                treeModel.removeNodeFromParent(currentNode);
                parent.deleteNode(currentNode);
                ArrayList<PhotoStoreNode> array = parent.getAllLeaves();
                tableModel = new PhotoStoreTableModel(array);
                infoPanel.setModel(tableModel);
            }
        }
    }


    private PhotoStoreTreeNode findNode(PhotoStoreTreeNode root, String s) {
        Enumeration<PhotoStoreTreeNode> e = root.depthFirstEnumeration();
        while (e.hasMoreElements()) {
            PhotoStoreTreeNode node = e.nextElement();
            if (node.toString().equalsIgnoreCase(s)) {
                return node;
            }
        }
        return null;
    }

    public static void main(String[] args) {
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (ClassNotFoundException | InstantiationException | UnsupportedLookAndFeelException | IllegalAccessException e) {
            System.err.println(e.getMessage());
        }
        PhotoStoreFrame mainClass = new PhotoStoreFrame();
        mainClass.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        mainClass.setVisible(true);
    }
}