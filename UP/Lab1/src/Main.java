import javax.swing.*;
import java.awt.*;
import java.awt.geom.AffineTransform;

public class Main {

    public static void main(String[] args) {
	// write your code here
        Rectangle rectangle=new Rectangle(300,300,100,60);
        MyFrame myFrame = new MyFrame(rectangle,(Color.decode( args[0])),Color.decode(args[1]),Float.parseFloat(args[2]));
        myFrame.setSize(700,700);

        myFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        myFrame.setVisible(true);
    }
}
