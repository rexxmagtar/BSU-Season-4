import java.awt.*;
import java.awt.geom.GeneralPath;
import java.awt.geom.Line2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;

public class MyStroke implements Stroke {



    @Override
    public Shape createStrokedShape(Shape p) {

        AstroidShape s = (AstroidShape) p;
        Polygon2D myPolygon = new Polygon2D();
        ArrayList<Point.Double> points = s.GetPoints();

        for (int i=0;i<points.size();i+=3){

            if(i+5>=points.size()){
                break;
            }
            if(Math.abs( points.get(i).x-points.get(points.size()/4).x)<3.7
                  ){

                continue;
            }
            double vX = points.get(i+2).x-points.get(i).x;
            double vY = points.get(i+2).y-points.get(i).y;
            double nvX =-vY;
            double nvY =vX;

                nvY=-vX;
                nvX=vY;


            float vLength =(float) Math.sqrt( nvX*nvX+nvY*nvY);

            if (vLength==0){
                vLength = 1;
            }

            points.get(i+1).x+=10*nvX/vLength;
            points.get(i+1).y+=10*nvY/vLength;
        }


        for (int i=0;i<points.size();i++){



            myPolygon.addPoint( (float) points.get(i).x, (float)points.get(i).y);
        }

        Shape  outLine = new BasicStroke().createStrokedShape(myPolygon);


        return outLine;
    }
}
