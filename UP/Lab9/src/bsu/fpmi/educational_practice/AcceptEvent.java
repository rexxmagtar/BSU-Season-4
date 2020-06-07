package bsu.fpmi.educational_practice;


public class AcceptEvent extends java.util.EventObject {
    public static final int MESS = 0;   // Button constants
    protected int id;           		// Which button was pressed?
    
    public AcceptEvent(Object source, int id) {
    	super(source);
		this.id = id;
    }
	    public int getID() { return id; }             // Return the button
}
