package bsu.fpmi.educational_practice;/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
import javax.swing.*;
import java.awt.*;

public class test extends JFrame {

    public static void main(String[] args) {
        test myFrame=new test();
        myFrame.setSize(500,500);

        myFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        myFrame.setVisible(true);
    }
    
    @Override
    public void paint(Graphics g){
      Information  inf = new Information(new Color(4,250,0), new Color(0,250,250), 150);
      inf.setX(100);
      inf.setY(100);
      inf.paint(g);
    }
}
