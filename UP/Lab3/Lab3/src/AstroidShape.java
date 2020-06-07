import java.awt.*;
import java.awt.geom.AffineTransform;
import java.awt.geom.PathIterator;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;
import java.util.Comparator;

public class AstroidShape implements Shape{
    float Tmin = -500;
    float Tmax = 500;
    float jump = 1;

double screenWidth;
double screenHeight;
    double currentIt=0;

    ArrayList<Point.Double> result;
    ArrayList<Point.Double> point2DS=new ArrayList<Point.Double>();

    public AstroidShape(double screenWidth,double screenHeight){
        this.screenWidth=screenWidth;
        this.screenHeight=screenHeight;
        UpdateList();
    }
    public  ArrayList<Point.Double> GetPoints(){
        return  result;
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

    public  void DrawAstroid(Graphics g){
        point2DS.clear();
        g=(Graphics2D)g;
        for (float t=Tmin;t<Tmax;t+=jump){
            double x = GetX(t);
            double y = GetY(t);
            ((Graphics2D) g).draw(new Rectangle2D.Double(x,y,2,2));
            point2DS.add(new Point.Double(x,y));
        }

        point2DS.sort(comparator);

        double yMin = point2DS.get(0).y;

        result=new ArrayList<Point.Double>();


        for(int i=0;i<point2DS.size() ;i++){
            if(point2DS.get(i).y<=yMin) {
                result.add(point2DS.get(i));
            }
        }
        point2DS.clear();
        int prevSize =result.size();
        for(int i =prevSize-1;i>=0;i--){

            result.add(new Point.Double(result.get(i).x,2*yMin- result.get(i).y));
        }

    }

    public  void UpdateList(){
        for (float t=Tmin;t<Tmax;t+=jump){
            double x = GetX(t);
            double y = GetY(t);
            point2DS.add(new Point.Double(x,y));
        }

        point2DS.sort(comparator);

        double yMin = point2DS.get(0).y;

        result=new ArrayList<Point.Double>();


        for(int i=0;i<point2DS.size() ;i++){
            if(point2DS.get(i).y<=yMin) {
                result.add(point2DS.get(i));
            }
        }
        point2DS.clear();
        int prevSize =result.size();
        for(int i =prevSize-1;i>=0;i--){

            result.add(new Point.Double(result.get(i).x,2*yMin- result.get(i).y));
        }
    }
    Comparator<Point.Double> comparator =new Comparator<Point.Double>() {
        @Override
        public int compare(Point.Double o1, Point.Double o2) {
            if(o1.x>o2.x){
                return 1;
            }

            if(o1.x<o2.x){
                return -1;
            }
            return 0;
        }
    };

   public Polygon Draw(){

Polygon polygon = new Polygon();
for (int i=0;i<result.size();i++){
    polygon.addPoint((int) result.get(i).x,(int) result.get(i).y-300);
}

return  polygon;
    }
    public  double GetX(float t){

        return  (double) Math.round((screenWidth/2+ (300*Math.pow(Math.sin(t),3))));
    }

    public  double    GetY(float t){
        return  (double) Math.round((screenHeight/2+300*Math.pow(Math.cos(t),3)));
    }
}
