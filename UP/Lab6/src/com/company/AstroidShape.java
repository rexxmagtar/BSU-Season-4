package com.company;

import java.awt.*;
import java.awt.datatransfer.DataFlavor;
import java.awt.datatransfer.Transferable;
import java.awt.datatransfer.UnsupportedFlavorException;
import java.awt.geom.AffineTransform;
import java.awt.geom.PathIterator;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.io.IOException;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.Comparator;

public class AstroidShape implements Shape, Serializable, Cloneable, Transferable {
    float Tmin = -500;
    float Tmax = 500;
    float jump = 1;
    int centerX;
    int centerY;

public AstroidShape(int centerX,int centerY){
    this.centerX=centerX;
    this.centerY=centerY;
    UpdateList();
}
    int currentIt=0;
    ArrayList<Point> result;
    ArrayList<Point> point2DS=new ArrayList<Point>();

    public  ArrayList<Point> GetPoints(){
        return  result;
    }
    @Override
    public Rectangle getBounds() {
        int xMin=1000000;
        int yMin=1000000;
        int xMax=-1000000;
        int yMax=-1000000;

        for (int i = 0; i < result.size(); i++) {
xMin=Integer.min(xMin,result.get(i).x);
            yMin=Integer.min(yMin,result.get(i).y);
            xMax=Integer.max(xMax,result.get(i).x);
            yMax=Integer.max(yMax,result.get(i).y);

        }
      return   new Rectangle(xMin,yMin , xMax-xMin, yMax-yMin);
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

    public boolean intersects(double x, double y, double w, double h) {
        return getBounds().intersects(x, y, w, h);
    }

    public boolean intersects(Rectangle2D r) {
        return getBounds().contains(r.getX(), r.getY(), r.getWidth(), r.getHeight());
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
        for (float t=Tmin;t<Tmax;t+=jump){
            int x = GetX(t);
            int y = GetY(t);
            g.fillRect(x,y,2,2);
            point2DS.add(new Point(x,y));
        }

        point2DS.sort(comparator);

        int yMin = point2DS.get(0).y;

        result=new ArrayList<Point>();


        for(int i=0;i<point2DS.size() ;i++){
            if(point2DS.get(i).y<=yMin) {
                result.add(point2DS.get(i));
            }
        }
        point2DS.clear();
        int prevSize =result.size();
        for(int i =prevSize-1;i>=0;i--){

            result.add(new Point(result.get(i).x,2*yMin- result.get(i).y));
        }

    }

    public  void UpdateList(){
        for (float t=Tmin;t<Tmax;t+=jump){
            int x = GetX(t);
            int y = GetY(t);
            point2DS.add(new Point(x,y));
        }

        point2DS.sort(comparator);

        int yMin = point2DS.get(0).y;

        result=new ArrayList<Point>();


        for(int i=0;i<point2DS.size() ;i++){
            if(point2DS.get(i).y<=yMin) {
                result.add(point2DS.get(i));
            }
        }
        point2DS.clear();
        int prevSize =result.size();
        for(int i =prevSize-1;i>=0;i--){

            result.add(new Point(result.get(i).x,2*yMin- result.get(i).y));
        }
    }
    Comparator<Point> comparator =new Comparator<Point>() {
        @Override
        public int compare(Point o1, Point o2) {
            if(o1.x>o2.x){
                return 1;
            }

            if(o1.x<o2.x){
                return -1;
            }
            return 0;
        }
    };

    public  int GetX(float t){

        return  centerX+ (int) Math.round(((200*Math.pow(Math.sin(t),3))));
    }

    public  int    GetY(float t){
        return centerY+  (int) Math.round((200*Math.pow(Math.cos(t),3)));
    }


    static DataFlavor decDataFlavor = new DataFlavor(AstroidShape.class, "Shapes");

    // This is a list of the flavors we know how to work with
    private static DataFlavor[] supportedFlavors = {decDataFlavor, DataFlavor.stringFlavor};

    @Override
    public DataFlavor[] getTransferDataFlavors() {
        return supportedFlavors.clone();
    }

    @Override
    public boolean isDataFlavorSupported(DataFlavor dataFlavor) {
        return (dataFlavor.equals(decDataFlavor) || dataFlavor.equals(DataFlavor.stringFlavor));
    }

    @Override
    public Object getTransferData(DataFlavor dataFlavor) throws UnsupportedFlavorException, IOException {
        if (dataFlavor.equals(decDataFlavor)) {
            return this;
        } else if (dataFlavor.equals(DataFlavor.stringFlavor)) {
            return toString();
        } else
            throw new UnsupportedFlavorException(dataFlavor);
    }

    @Override
    public Object clone() {
        try {
            return super.clone();
        } catch (CloneNotSupportedException e) {
            System.out.println("Couldnt clone");// This should never happen
            return this;
        }
    }

    void translate(int x, int y) {
        centerX += x;
        centerY += y;
    }

    @Override
    public String toString() {
        return  this.centerX + " " + this.centerY ;
    }


    static AstroidShape getFromString(String line) {
        String[] arr = line.split(" ");
        return new AstroidShape(Integer.parseInt(arr[0]), Integer.parseInt(arr[1]));
    }



}
