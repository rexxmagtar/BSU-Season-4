/**
 * The YesNoPanel class fires an event of this type when the user clicks one
 * of its buttons.  The id field specifies which button the user pressed.
 **/
public class AnswerEvent extends java.util.EventObject {
    public static final int YES = 0, NO = 1, CANCEL = 2;  // Button constants
    protected int id;                             // Which button was pressed?
    public AnswerEvent(Object source, int id) {
	super(source);
	this.id = id;
    }
    public int getID() { return id; }             // Return the button
}
