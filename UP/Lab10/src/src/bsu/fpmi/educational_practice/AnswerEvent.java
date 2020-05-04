package src.bsu.fpmi.educational_practice;


/**
 *
 * @author Ivan Kalinchuk,
 * Famcs 2020
 */
public class AnswerEvent extends java.util.EventObject {
    public static final int MESS = 0;   // Button constants
    protected int id;           		// Which button was pressed?
    
    public AnswerEvent(Object source, int id) {
    	super(source);
		this.id = id;
    }
	    public int getID() { return id; }             // Return the button
}
