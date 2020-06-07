
import java.awt.*;
import java.awt.geom.AffineTransform;
import java.awt.geom.PathIterator;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.lang.management.PlatformLoggingMXBean;

public class Figure implements Shape {
    double a = 10;
    int height = 600;

    Polygon draw(Graphics2D g)
    {
        Polygon c = new Polygon();
        for(double x = 0.001; x < 2*a; x+=0.001)
        {
            c.addPoint((int)(x*30),  - getY(x-a)*30+50);
        }
        return c;
    }

    private int getY(double x) {
        return (int)(x/Math.tan(Math.PI*x/(2*a)));
    }


    @Override
    public Rectangle getBounds() {
        return null;
    }

    @Override
    public Rectangle2D getBounds2D() {
        return null;
    }

    @Override
    public boolean contains(double v, double v1) {
        return false;
    }

    @Override
    public boolean contains(Point2D point2D) {
        return false;
    }

    @Override
    public boolean intersects(double v, double v1, double v2, double v3) {
        return false;
    }

    @Override
    public boolean intersects(Rectangle2D rectangle2D) {
        return false;
    }

    @Override
    public boolean contains(double v, double v1, double v2, double v3) {
        return false;
    }

    @Override
    public boolean contains(Rectangle2D rectangle2D) {
        return false;
    }

    @Override
    public PathIterator getPathIterator(AffineTransform affineTransform) {
        return null;
    }

    @Override
    public PathIterator getPathIterator(AffineTransform affineTransform, double v) {
        return null;
    }
}
