/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package bsu.fpmi.educational_practice;

import java.awt.BasicStroke;
import java.awt.Canvas;
import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Shape;
import java.awt.geom.Arc2D;
import java.awt.geom.Line2D;
import java.awt.geom.Rectangle2D;
import java.beans.*;
import java.io.Serializable;

/**
 *
 * @author Иван
 */
public class InfoSign extends Canvas implements Serializable {
    
    public static final String PROP_SAMPLE_PROPERTY = "sampleProperty";
    
    private String sampleProperty;
    
    private PropertyChangeSupport propertySupport;
    
    public InfoSign() {
        propertySupport = new PropertyChangeSupport(this);
    }
    
    public String getSampleProperty() {
        return sampleProperty;
    }
    
    public void setSampleProperty(String value) {
        String oldValue = sampleProperty;
        sampleProperty = value;
        propertySupport.firePropertyChange(PROP_SAMPLE_PROPERTY, oldValue, sampleProperty);
    }
    
    public void addPropertyChangeListener(PropertyChangeListener listener) {
        propertySupport.addPropertyChangeListener(listener);
    }
    
    public void removePropertyChangeListener(PropertyChangeListener listener) {
        propertySupport.removePropertyChangeListener(listener);
    }
    
     private float diam;
    private Color iColor = null;
    private Color bColor = null;
    private float x , y;
    
    
    public Color getIColor() {return this.iColor;}
    public Color getBColor() {return this.bColor;}
    public float getDiam() {return this.diam;}
    
    public void setIColor(Color tmp){
        this.iColor = tmp;
    }
    public void setBColor(Color tmp){
        this.bColor = tmp;
    }
    public void setDiam(float tmp){
        this.diam = tmp;
    }
    
    public void setX(int x){this.x = x;}
    public void setY(int y){this.y = y;}
    
    public InfoSign(Color i, Color b, int diam){
        setIColor(i);
        setBColor(b);
        setDiam(diam);

        
    }
    
    @Override
    public void paint(Graphics g){
        Graphics2D g2d = (Graphics2D)g;
        g.setColor(bColor);
        ((Graphics2D) g).fill(new Rectangle2D.Float(0,0,1000,1000));
        Shape circle = new Arc2D.Float(x,y,diam,diam,-90,240, Arc2D.OPEN);
        g.setColor(iColor);
        ((Graphics2D) g).setStroke(new BasicStroke(15));
      ((Graphics2D) g).draw(circle);
float smallDiam=diam*0.2f;
        Shape bottomCircle = new Arc2D.Float(x+diam/2- smallDiam,y+diam,smallDiam*2,smallDiam*4,90,90,Arc2D.OPEN);
        ((Graphics2D) g).draw(bottomCircle);

        Shape rect = new Line2D.Float(x+diam/2 - smallDiam,y+diam+smallDiam*2.5f + 15,x+diam/2- smallDiam,y+diam+smallDiam*2.5f +15+smallDiam/10);

    ((Graphics2D) g).draw(rect);

    }
    
}
