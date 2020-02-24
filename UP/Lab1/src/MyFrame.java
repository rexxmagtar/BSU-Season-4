import javax.swing.*;
import java.awt.*;
import java.awt.geom.AffineTransform;
import java.awt.geom.Ellipse2D;
import java.awt.geom.Point2D;

public class MyFrame  extends JFrame implements  Runnable  {



    public Polygon rectangle;
    public AffineTransform affineTransform;
    public  AffineTransform ellipseAffinetransform;
    public  Shape shapeToDraw;
    public Color FigurebrushColor;
    public  Color EntireColor;
    public float LineWidth;
    Shape r1 ;

Thread thread;

    public  MyFrame(Rectangle rect, Color FigureColor,Color EntireC,float lineWidth){

        FigurebrushColor=FigureColor;
        EntireColor=EntireC;
        LineWidth=lineWidth;
        affineTransform=AffineTransform.getRotateInstance(0.1,rect.x+rect.width/2,rect.y+rect.height/2);
        ellipseAffinetransform= AffineTransform.getRotateInstance(-0.1,rect.x+rect.width/2,rect.y+rect.height/2);;
        rectangle=new Polygon();
        rectangle.addPoint(rect.x,rect.y);
        rectangle.addPoint(rect.x+rect.width,rect.y);
        rectangle.addPoint(rect.x+rect.width,rect.y+rect.height);
        rectangle.addPoint(rect.x,rect.y+rect.height);
        shapeToDraw=rectangle;
        float X=(float )(rect.width*2/Math.sqrt(2));
        float Y=(float )(rect.height*2/Math.sqrt(2));
        r1 = new Ellipse2D.Float(rect.x+rect.width/2-X/2,rect.y+rect.height/2-Y/2,X,Y);
        thread=new Thread(this::run);
thread.start();

    }
    @Override
    public void paint(Graphics g) {



        Graphics2D graphics2D = (Graphics2D)g;
        graphics2D.setStroke(new BasicStroke(LineWidth));
        graphics2D.setColor(Color.WHITE);
        graphics2D.draw(shapeToDraw);
        graphics2D.draw(r1);
        graphics2D.fill(r1);
        graphics2D.setColor(FigurebrushColor);
        r1=ellipseAffinetransform.createTransformedShape(r1);
        shapeToDraw = affineTransform.createTransformedShape(shapeToDraw);
        graphics2D.draw(r1);
        graphics2D.setColor(EntireColor);
        graphics2D.fill(r1);
        graphics2D.setColor(FigurebrushColor);
        graphics2D.draw(shapeToDraw);

        System.out.println("Repainting "+shapeToDraw);

    }



    @Override
    public void run() {
        while(true) {
            System.out.println("Running");

            repaint();
            try {
                Thread.sleep(120);
            } catch (Exception ex) {

            }
        }
    }
}
