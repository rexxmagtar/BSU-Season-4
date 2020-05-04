/** This class defines an enumerated type with three values */
public class Alignment {
    /** This private constructor prevents anyone from instantiating us */
    private Alignment() {};
    // The following three constants are the only instances of this class
    public static final Alignment LEFT = new Alignment();
    public static final Alignment CENTER = new Alignment();
    public static final Alignment RIGHT = new Alignment();
}
