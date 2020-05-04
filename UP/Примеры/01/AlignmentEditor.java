import java.beans.*;
import java.awt.*;

/**
 * This PropertyEditor defines the enumerated values of the alignment property
 * so that a bean box or IDE can present those values to the user for selection
 **/
public class AlignmentEditor extends PropertyEditorSupport {
    /** Return the list of value names for the enumerated type. */
    public String[] getTags() {
	return new String[] { "left", "center", "right" };
    }
    
    /** Convert each of those value names into the actual value. */
    public void setAsText(String s) {
	if (s.equals("left")) setValue(Alignment.LEFT);
	else if (s.equals("center")) setValue(Alignment.CENTER);
	else if (s.equals("right")) setValue(Alignment.RIGHT);
	else throw new IllegalArgumentException(s);
    }
    
    /** This is an important method for code generation. */
    public String getJavaInitializationString() {
	Object o = getValue();
	if (o == Alignment.LEFT)
	    return "com.davidflanagan.examples.beans.Alignment.LEFT";
	if (o == Alignment.CENTER)
	    return "com.davidflanagan.examples.beans.Alignment.CENTER";
	if (o == Alignment.RIGHT)
	    return "com.davidflanagan.examples.beans.Alignment.RIGHT";
	return null;
    }
}
