package com.company;

import java.awt.*;
import java.awt.geom.GeneralPath;
import java.awt.geom.Line2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;

public class MyStroke implements Stroke {



    @Override
    public Shape createStrokedShape(Shape p) {

        AstroidShape s = (AstroidShape) p;
        ArrayList<Point> points = s.GetPoints();

        for (int i=0;i<points.size();i+=3){

            if(i+5>=points.size()){
                break;
            }
            int vX = points.get(i+2).x-points.get(i).x;
            int vY = points.get(i+2).y-points.get(i).y;
            int nvX =-vY;
            int nvY =vX;

                nvY=-vX;
                nvX=vY;


            int vLength =(int) Math.sqrt( nvX*nvX+nvY*nvY);

            if (vLength==0){
                vLength = 1;
            }

            points.get(i+1).x+=10*nvX/vLength;
            points.get(i+1).y+=10*nvY/vLength;
        }

        Polygon myPolygon = new Polygon();
        for (int i=0;i<points.size();i++){


            myPolygon.addPoint(points.get(i).x,points.get(i).y);
        }

        Shape  outLine = new BasicStroke().createStrokedShape(myPolygon);


        return outLine;
    }
}
