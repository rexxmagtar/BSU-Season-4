import javax.swing.*;
import java.awt.*;
import java.awt.geom.Ellipse2D;
import java.awt.geom.Rectangle2D;
import java.awt.image.*;
import java.util.logging.Filter;

public class MyFrame extends JFrame {

    Shape MyCircle ;
    Font MyFont;

    FontMetrics MyMetrics;

    public  MyFrame(){
        MyCircle=new Ellipse2D.Float( 350-100,100,200,200);
        MyFont = new Font("TimesRoman", Font.PLAIN, 50);

    }

    @Override
    public void paint(Graphics g) {

        BufferedImage image =                   // Create an off-screen image
                new BufferedImage(700, 350, BufferedImage.TYPE_INT_RGB);
        Graphics2D ig = image.createGraphics(); // Get its Graphics for drawing


        Graphics2D graphics2D = ig;

        MyShape.DrawShape(ig,MyCircle,MyFont);



        g.drawImage(image,0,0,null );
        g.drawImage(processImage( image),0,350,null );
    }

    public BufferedImage processImage(BufferedImage image) {
        float[] brightonMatrix = { 0, -1.0f, 0.0f, -1.0f, 5.0f, -1.0f, 0.0f, -1.0f, 0.0f };
        BufferedImageOp brightenFilter = new ConvolveOp(new Kernel(3, 3, brightonMatrix),
                ConvolveOp.EDGE_NO_OP, null);
        return brightenFilter.filter(image, null);
    }



}
