package com.company;


import javax.swing.*;
import javax.swing.border.BevelBorder;
import javax.swing.border.Border;
import java.awt.*;
import java.awt.datatransfer.DataFlavor;
import java.awt.datatransfer.Transferable;
import java.awt.dnd.*;
import java.awt.event.*;
import java.util.ArrayList;


public class DecDragAndDrop extends JComponent implements
        DragGestureListener, // For recognizing the start of drags
        DragSourceListener,  // For processing drag source events
        DropTargetListener,  // For processing drop target events
        MouseListener,      // For processing mouse clicks
        MouseMotionListener // For processing mouse drags
{
    private int param = 200;

    private static final long serialVersionUID = 1L;
    private ArrayList<AstroidShape> shapes = new ArrayList();
    // A list of Scribbles to draw
    private AstroidShape currentScribble; // The scribble in progress
    private AstroidShape beingDragged; // The scribble being dragged
    private DragSource dragSource; // A central DnD object
    private boolean dragMode; // Are we dragging or scribbling?

    // These are some constants we use
    private static final int LINE_WIDTH = 2;
    private static final BasicStroke LINE_STYLE = new BasicStroke(LINE_WIDTH);
    private static final Border NORMAL_BORDER = new BevelBorder(BevelBorder.LOWERED);
    private static final Border DROP_BORDER = new BevelBorder(BevelBorder.RAISED);

    /**
     * The constructor: set up drag-and-drop stuff
     */
    private DecDragAndDrop() {
        // Give ourselves a nice default border.
        // We'll change this border during drag-and-drop.
        setBorder(NORMAL_BORDER);

        // Register listeners to handle drawing
        addMouseListener(this);
        addMouseMotionListener(this);

        // Create a DragSource and DragGestureRecognizer to listen for drags
        // The DragGestureRecognizer will notify the DragGestureListener
        // when the user tries to drag an object
        dragSource = DragSource.getDefaultDragSource();
        dragSource.createDefaultDragGestureRecognizer(this, // What component
                DnDConstants.ACTION_COPY_OR_MOVE, // What drag types?
                this);// the listener

        // Create and set up a DropTarget that will listen for drags and
        // drops over this component, and will notify the DropTargetListener
        DropTarget dropTarget = new DropTarget(this, // component to monitor
                this); // listener to notify
        this.setDropTarget(dropTarget); // Tell the component about it.
    }

    /**
     * The component draws itself by drawing each of the Scribble objects.
     **/
    public void paintComponent(Graphics g) {
        super.paintComponent(g);
        Graphics2D g2 = (Graphics2D) g;
        g2.setStroke(new MyStroke()); // Specify wide lines

        for (AstroidShape shape : shapes) {
            g2.draw(shape); // Draw the scribble
        }
        g2.setStroke(LINE_STYLE);
    }

    private void setDragMode(boolean dragMode) {
        this.dragMode = dragMode;
    }

    /**
     * This method, and the following four methods are from the MouseListener
     * interface. If we're in drawing mode, this method handles mouse down
     * events and starts a new scribble.
     **/
    public void mousePressed(MouseEvent e) {
        if (dragMode) {
            return;
        }

        currentScribble = new AstroidShape( e.getX(), e.getY());
        shapes.add(currentScribble);

        repaint();
    }

    public void mouseReleased(MouseEvent e) {
    }

    public void mouseClicked(MouseEvent e) {
    }

    public void mouseEntered(MouseEvent e) {
    }

    public void mouseExited(MouseEvent e) {
    }

    /**
     * This method and mouseMoved() below are from the MouseMotionListener
     * interface. If we're in drawing mode, this method adds a new point to the
     * current scribble and requests a redraw
     **/
    public void mouseDragged(MouseEvent e) {
    }

    public void mouseMoved(MouseEvent e) {
    }

    /**
     * This method implements the DragGestureListener interface. It will be
     * invoked when the DragGestureRecognizer thinks that the user has initiated
     * a drag. If we're not in drawing mode, then this method will try to figure
     * out which Scribble object is being dragged, and will initiate a drag on
     * that object.
     **/
    public void dragGestureRecognized(DragGestureEvent e) {
        // Don't drag if we're not in drag mode
        if (!dragMode) {
            return;
        }
System.out.println("Drag gesture recongnision");
        // Figure out where the drag started
        MouseEvent inputEvent = (MouseEvent) e.getTriggerEvent();
        int x = inputEvent.getX();
        int y = inputEvent.getY();

        // Figure out which scribble was clicked on, if any by creating a
        // small rectangle around the point and testing for intersection.
        Rectangle rectangle = new Rectangle(x - LINE_WIDTH, y - LINE_WIDTH, LINE_WIDTH * 2, LINE_WIDTH * 2);

        for (AstroidShape witch : shapes) { // Loop through the scribbles
            if (witch.intersects(rectangle)) {
                // The user started the drag on top of this scribble, so
                // start to drag it.

                // First, remember which scribble is being dragged, so we can
                // delete it later (if this is a move rather than a copy)
                beingDragged = witch;

                // Next, create a copy that will be the one dragged
                AstroidShape dragScribble = (AstroidShape) witch.clone();
                // Adjust the origin to the point the user clicked on.

//               dragScribble.translate(-x, -y);


                // Choose a cursor based on the type of drag the user initiated
                Cursor cursor;
                switch (e.getDragAction()) {
                    case DnDConstants.ACTION_COPY:
                        cursor = DragSource.DefaultCopyDrop;
                        break;
                    case DnDConstants.ACTION_MOVE:
                        cursor = DragSource.DefaultMoveDrop;
                        break;
                    default:
                        return; // We only support move and copes
                }
                e.startDrag(cursor, dragScribble, this);
                return;
            }
        }
    }

    /**
     * This method, and the four unused methods that follow it implement the
     * DragSourceListener interface. dragDropEnd() is invoked when the user
     * drops the scribble she was dragging. If the drop was successful, and if
     * the user did a "move" rather than a "copy", then we delete the dragged
     * scribble from the list of scribbles to draw.
     **/
    public void dragDropEnd(DragSourceDropEvent e) {
        System.out.println("Drag and drop end...");
        if (!e.getDropSuccess())
            return;
        System.out.println("Drag and drop end succefully...");
        int action = e.getDropAction();
        if (action == DnDConstants.ACTION_MOVE) {
            shapes.remove(beingDragged);
            beingDragged = null;
            repaint();
        }
    }

    // These methods are also part of DragSourceListener.
    // They are invoked at interesting points during the drag, and can be
    // used to perform "drag over" effects, such as changing the drag cursor
    // or drag image.
    public void dragEnter(DragSourceDragEvent e) {
    }

    public void dragExit(DragSourceEvent e) {
    }

    public void dropActionChanged(DragSourceDragEvent e) {
    }

    public void dragOver(DragSourceDragEvent e) {
    }

    // The next five methods implement DropTargetListener

    /**
     * This method is invoked when the user first drags something over us. If we
     * understand the data type being dragged, then call acceptDrag() to tell
     * the system that we're receptive. Also, we change our border as a
     * "drag under" effect to signal that we can accept the drop.
     **/
    public void dragEnter(DropTargetDragEvent e) {
        if (e.isDataFlavorSupported(AstroidShape.decDataFlavor) || e.isDataFlavorSupported(DataFlavor.stringFlavor)) {
            e.acceptDrag(DnDConstants.ACTION_COPY_OR_MOVE);
            this.setBorder(DROP_BORDER);
        }
    }

    /**
     * The user is no longer dragging over us, so restore the border
     */
    public void dragExit(DropTargetEvent e) {
        this.setBorder(NORMAL_BORDER);
    }

    /**
     * This is the key method of DropTargetListener. It is invoked when the user
     * drops something on us.
     **/
    public void drop(DropTargetDropEvent e) {
        this.setBorder(NORMAL_BORDER); // Restore the default border

        // First, check whether we understand the data that was dropped.
        // If we supports our data flavors, accept the drop, otherwise reject.
        if (e.isDataFlavorSupported(AstroidShape.decDataFlavor) || e.isDataFlavorSupported(DataFlavor.stringFlavor)) {
            e.acceptDrop(DnDConstants.ACTION_COPY_OR_MOVE);
        } else {
            e.rejectDrop();
            return;
        }

        // We've accepted the drop, so now we attempt to get the dropped data
        // from the Transferable object.
        Transferable t = e.getTransferable(); // Holds the dropped data
        AstroidShape droppedScribble; // This will hold the Scribble object

        // First, try to get the data directly as a scribble object
        try {
            droppedScribble = (AstroidShape) t.getTransferData(AstroidShape.decDataFlavor);
        } catch (Exception ex) { // unsupported flavor, IO exception, etc.
            // If that doesn't work, try to get it as a String and parse it
            try {
                String s = (String) t.getTransferData(DataFlavor.stringFlavor);
                droppedScribble = AstroidShape.getFromString(s);
            } catch (Exception ex2) {
                // If we still couldn't get the data, tell the system we failed
                e.dropComplete(false);
                return;
            }
        }

        System.out.println("Dropping!");
        // If we get here, we've got the Scribble object
        Point p = e.getLocation(); // Where did the drop happen?
        // Move it there
        droppedScribble.centerX=(int) p.getX();
        droppedScribble.centerY=(int) p.getY();
        shapes.add(droppedScribble); // add to display list
        repaint(); // ask for redraw
        e.dropComplete(true); // signal success!
    }

    // These are unused DropTargetListener methods
    public void dragOver(DropTargetDragEvent e) {
    }

    public void dropActionChanged(DropTargetDragEvent e) {
    }




    /**
     * The main method. Creates a simple application using this class. Note the
     * buttons for switching between draw mode and drag mode.
     **/


    public static void main(String[] args) {

        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (ClassNotFoundException | UnsupportedLookAndFeelException | IllegalAccessException | InstantiationException e) {
            System.err.println(e.getMessage());
        }

        // Create a frame and put a scribble pane in it
        JFrame frame = new JFrame("ScribbleDragAndDrop");
        frame.addWindowListener(new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                System.exit(0);
            }
        });
        final DecDragAndDrop scribblePane = new DecDragAndDrop();
        frame.getContentPane().add(scribblePane, BorderLayout.CENTER);

        // Create two buttons for switching modes
        JToolBar toolbar = new JToolBar();
        ButtonGroup group = new ButtonGroup();
        JToggleButton draw = new JToggleButton("Draw");
        JToggleButton drag = new JToggleButton("Drag");


        draw.addActionListener(e -> scribblePane.setDragMode(false));
        drag.addActionListener(e -> scribblePane.setDragMode(true));

        group.add(draw);
        group.add(drag);
        toolbar.add(draw);
        toolbar.add(drag);

        frame.getContentPane().add(toolbar, BorderLayout.NORTH);

        // Start off in drawing mode
        draw.setSelected(true);
        scribblePane.setDragMode(false);

        // Pop up the window
        frame.setSize(700, 700);
        frame.setVisible(true);
    }
}
