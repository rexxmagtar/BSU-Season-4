import java.awt.*;
import java.awt.geom.*;

public class MyShape implements Shape {


    @Override
    public Rectangle getBounds() {
        return null;
    }

    @Override
    public Rectangle2D getBounds2D() {
        return null;
    }

    @Override
    public boolean contains(double x, double y) {
        return false;
    }

    @Override
    public boolean contains(Point2D p) {
        return false;
    }

    @Override
    public boolean intersects(double x, double y, double w, double h) {
        return false;
    }

    @Override
    public boolean intersects(Rectangle2D r) {
        return false;
    }

    @Override
    public boolean contains(double x, double y, double w, double h) {
        return false;
    }

    @Override
    public boolean contains(Rectangle2D r) {
        return false;
    }

    @Override
    public PathIterator getPathIterator(AffineTransform at) {
        return null;
    }

    @Override
    public PathIterator getPathIterator(AffineTransform at, double flatness) {
        return null;
    }

    public static void DrawShape(Graphics2D graphics2D,Shape MyCircle,Font MyFont){
        graphics2D.setStroke(new BasicStroke(30));

        graphics2D.setColor(Color.WHITE);
        graphics2D.fillRect(0,0,700,350);
        Rectangle2D bounds =MyCircle.getBounds2D();
        GradientPaint gradientPaint = new GradientPaint((float)bounds.getMaxX(),(float)bounds.getY(),Color.GRAY,
                (float)bounds.getY(),(float)bounds.getX(),Color.white);
        graphics2D.setPaint(gradientPaint);
        graphics2D.fill(MyCircle);

        graphics2D.setColor(Color.red);
        graphics2D.draw(MyCircle);


        graphics2D.setFont(MyFont);

        float textWidth= graphics2D.getFontMetrics().stringWidth("80");
        graphics2D.drawString("80",(float)MyCircle.getBounds().getCenterX()-textWidth/2,(float) MyCircle.getBounds().getCenterY()+15);
    }
}
