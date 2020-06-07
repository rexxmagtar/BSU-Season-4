import javax.swing.*;
import java.awt.*;
import java.awt.geom.Point2D;
import java.util.ArrayList;
import java.util.Comparator;



public class MyFrame extends JFrame {


    @Override
    public void paint(Graphics g) {

        Graphics2D graphics=(Graphics2D)g;
       AstroidShape astroidShape = new AstroidShape(this.getWidth(),this.getHeight());
       graphics.setStroke(new MyStroke());
       graphics.draw(astroidShape);

    }



}
