import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.*;
import java.awt.print.*;
import java.awt.geom.*;
import java.awt.font.*;

public class CustomStrokes implements GraphSample {
    static final int WIDTH = 1000, HEIGHT = 1000;        // Size of our example
    public String getName() {return "Custom Strokes";} // From GraphSample
    public int getWidth() { return WIDTH; }            // From GraphSample
    public int getHeight() { return HEIGHT; }          // From GraphSample

    Stroke[] strokes = new Stroke[] {
	new ControlPointsStroke(2.0f),  // Shows the vertices & control points
    };

    public void draw(Graphics2D g, Component c) {
	Font f = new Font("Serif", Font.BOLD, 200);
	GlyphVector gv = f.createGlyphVector(g.getFontRenderContext(), "B");

	g.setColor(Color.black);
	g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, 
			   RenderingHints.VALUE_ANTIALIAS_ON);
	g.translate(10, 175);

	// Draw the shape once with each stroke
	    g.setStroke(strokes[0]);   // set the stroke


		AstroidShape a = new AstroidShape(WIDTH,HEIGHT);
		a.UpdateList();
		Polygon pol = a.Draw();

		Figure b = new Figure();
b.draw(g);
		g.draw((Shape) pol);
//	    g.translate(140,0);        // move to the right
    }
}
/**
 * This Stroke implementation strokes the shape using a thin line, and
 * also displays the end points and Bezier curve control points of all
 * the line and curve segments that make up the shape.  The radius
 * argument to the constructor specifies the size of the control point
 * markers. Note the use of PathIterator to break the shape down into
 * its segments, and of GeneralPath to build up the stroked shape.
 **/
class ControlPointsStroke implements Stroke {
    float radius;  // how big the control point markers should be
    public ControlPointsStroke(float radius) { this.radius = radius; }

    public Shape createStrokedShape(Shape shape) {
	// Start off by stroking the shape with a thin line.  Store the
	// resulting shape in a GeneralPath object so we can add to it.
	GeneralPath strokedShape = 
	    new GeneralPath(new BasicStroke(2f).createStrokedShape(shape));

	// Use a PathIterator object to iterate through each of the line and
	// curve segments of the shape.  For each one, mark the endpoint and
	// control points (if any) by adding a rectangle to the GeneralPath
	float[] coords = new float[6];

	PathIterator j=shape.getPathIterator(null);
	j.currentSegment(coords);
		coords[4]=coords[2];
		coords[5]=coords[3];
		coords[2] = coords[0];
		coords[3] = coords[1];

	for(PathIterator i=shape.getPathIterator(null); !i.isDone();i.next(),i.next(),i.next()) {
	    int type = i.currentSegment(coords);
		coords[4]=coords[2];
		coords[5]=coords[3];
		coords[2] = coords[0];
		coords[3] = coords[1];
	    Shape s = null, s2 = null, s3 = null;
	    switch(type) {
	    case PathIterator.SEG_LINETO:
		markPoint(strokedShape, coords[2], coords[3], coords[0], coords[1],coords[4],coords[5]); // falls through
			coords[4]=coords[2];
			coords[5]=coords[3];
			coords[2] = coords[0];
			coords[3] = coords[1];

	    case PathIterator.SEG_CLOSE:
		break;
	    }
	}

	return strokedShape;
    }

    /** Add a small square centered at (x,y) to the specified path */
    void markPoint(GeneralPath path, float x, float y, float x1, float y1,float x2,float y2) {

			double stepY = (y1 - y) * 0.25;
			double stepX = (x1 - x) * 0.25;
			path.moveTo(x, y);  // Begin a new sub-path

if(Math.abs( x-x2)<1){
return;
}
			double vX = x2-x;
			double vY = y2-y;
			double nvX =-vY;
			double nvY =vX;

			nvY=-vX;
			nvX=vY;


			float vLength =(float) Math.sqrt( nvX*nvX+nvY*nvY);

			if (vLength==0){
				vLength = 1;
			}
if(Math.abs( x1-x2)>10){
	return;
}
			x1+=-10*nvX/vLength;
			y1+=-10*nvY/vLength;
			path.lineTo(x1, y1);
			path.lineTo(x2, y2);


			path.lineTo(x1, y1 - 1);
			path.lineTo(x, y);



    }
}
////////////////////////////////////////////////////////////////////////////
// Frame
////////////////////////////////////////////////////////////////////////////
class GraphSampleFrame extends JFrame {
    // The class name of the requested example
    static final String classname = "CustomStrokes";
    public GraphSampleFrame(final GraphSample[] examples) {
	super("GraphSampleFrame");

	Container cpane = getContentPane();   // Set up the frame 
	cpane.setLayout(new BorderLayout());
	final JTabbedPane tpane = new JTabbedPane(); // And the tabbed pane 
	cpane.add(tpane, BorderLayout.CENTER);

	// Add a menubar
	JMenuBar menubar = new JMenuBar();         // Create the menubar
	this.setJMenuBar(menubar);                 // Add it to the frame
	JMenu filemenu = new JMenu("File");        // Create a File menu
	menubar.add(filemenu);                     // Add to the menubar
	JMenuItem quit = new JMenuItem("Quit");    // Create a Quit item
	filemenu.add(quit);                        // Add it to the menu

	// Tell the Quit menu item what to do when selected
	quit.addActionListener(new ActionListener() {
		public void actionPerformed(ActionEvent e) { System.exit(0); }
	    });

	// In addition to the Quit menu item, also handle window close events
	this.addWindowListener(new WindowAdapter() {
		public void windowClosing(WindowEvent e) { System.exit(0); }
	    });

	// Insert each of the example objects into the tabbed pane
	for(int i = 0; i < examples.length; i++) {
	    GraphSample e = examples[i];
	    tpane.addTab(e.getName(), new GraphSamplePane(e));
	}
    }

    /**
     * This inner class is a custom Swing component that displays
     * a GraphSample object.
     */
    public class GraphSamplePane extends JComponent {
	GraphSample example;  // The example to display
	Dimension size;           // How much space it requires
	
	public GraphSamplePane(GraphSample example) {
	    this.example = example;
	    size = new Dimension(example.getWidth(), example.getHeight());
            setMaximumSize( size );
	}

	/** Draw the component and the example it contains */
	public void paintComponent(Graphics g) {
	    g.setColor(Color.white);                    // set the background
	    g.fillRect(0, 0, size.width, size.height);  // to white
	    g.setColor(Color.black);             // set a default drawing color
	    example.draw((Graphics2D) g, this);  // ask example to draw itself
	}

	// These methods specify how big the component must be
	public Dimension getPreferredSize() { return size; }
	public Dimension getMinimumSize() { return size; }
    }

    public static void main(String[] args) {
	GraphSample[] examples = new GraphSample[1];

	    // Try to instantiate the named GraphSample class
	    try {
		Class exampleClass = Class.forName(classname);
		examples[0] = (GraphSample) exampleClass.newInstance();
	    }
	    catch (ClassNotFoundException e) {  // unknown class
		System.err.println("Couldn't find example: "  + classname);
		System.exit(1);
	    }
	    catch (ClassCastException e) {      // wrong type of class
		System.err.println("Class " + classname + 
				   " is not a GraphSample");
		System.exit(1);
	    }
	    catch (Exception e) {  // class doesn't have a public constructor
		// catch InstantiationException, IllegalAccessException
		System.err.println("Couldn't instantiate example: " +
				   classname);
		System.exit(1);
	    }

	// Now create a window to display the examples in, and make it visible
	GraphSampleFrame f = new GraphSampleFrame(examples);
	f.pack();
	f.setVisible(true);
    }
}
////////////////////////////////////////////////////////////////////////////
// interface GraphSample
////////////////////////////////////////////////////////////////////////////
/**
 * This interface defines the methods that must be implemented by an
 * object that is to be displayed by the GraphSampleFrame object
 */
interface GraphSample {
    public String getName();                      // Return the example name
    public int getWidth();                        // Return its width
    public int getHeight();                       // Return its height
    public void draw(Graphics2D g, Component c);  // Draw the example
}

