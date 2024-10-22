/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package bsu.fpmi.educational_practice;

import java.beans.*;
import java.io.Serializable;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import javax.swing.JOptionPane;

/**
 *
 * @author Иван
 */
public class NewJPanel extends Panel implements Serializable {
    
    public static final String PROP_SAMPLE_PROPERTY = "sampleProperty";
    
    private String sampleProperty;
    
    private PropertyChangeSupport propertySupport;
    
    private String R1MSG,R2MSG,BMSG,LMSG;

    public String getR1MSG() {
        return R1MSG;
    }

    public void setR1MSG(String R1MSG) {
        this.R1MSG = R1MSG;
        jRadioButton1.setText(R1MSG);
    }

    public String getR2MSG() {
        return R2MSG;
    }

    public void setR2MSG(String R2MSG) {
        this.R2MSG = R2MSG;
        jRadioButton2.setText(R2MSG);
    }

    public String getBMSG() {
        return BMSG;
    }

    public void setBMSG(String BMSG) {
        this.BMSG = BMSG;
        jButton1.setText(BMSG);
        validate();
    }

    public String getLMSG() {
        return LMSG;
    }

    public void setLMSG(String LMSG) {
        this.LMSG = LMSG;
        jLabel1.setText(LMSG);
    }
    
    
    
    public NewJPanel() {
        propertySupport = new PropertyChangeSupport(this);
                initComponents();
//        setEnterKey("a");
        initEnterKey();
sampleProperty = "value";
setR1MSG("R1");
setR2MSG("R2");
setBMSG("Click");

        
    }
    
    public String getSampleProperty() {
        return sampleProperty;
    }
    
    public void setSampleProperty(String value) {
        String oldValue = sampleProperty;
        sampleProperty = value;
        propertySupport.firePropertyChange(PROP_SAMPLE_PROPERTY, oldValue, sampleProperty);
    }
    
    @Override
    public void addPropertyChangeListener(PropertyChangeListener listener) {
        propertySupport.addPropertyChangeListener(listener);
    }
    
     @Override
    public void removePropertyChangeListener(PropertyChangeListener listener) {
        propertySupport.removePropertyChangeListener(listener);
    }
    
    
    
    
    String STR = "Button is preesed,";

    String buttonStatus;
    String radioB1Status;
    String radioB2Status;

   private Color R1 ,R2,B,L;

    public Color getR1() {
        return R1;
       
    }

    public void setR1(java.awt.Color R1) {
        this.R1 = R1;
          jRadioButton1.setOpaque(true);
         jRadioButton1.setBackground(R1);
         validate();
    }

    public java.awt.Color getR2() {
        return R2;
    }

    public void setR2(java.awt.Color R2) {
        this.R2 = R2;
          jRadioButton2.setOpaque(true);
         jRadioButton2.setBackground(R2);
         validate();
    }

    public java.awt.Color getB() {
        return B;
    }

    public void setB(java.awt.Color B) {
        this.B = B;
        jButton1.setOpaque(true);
         jButton1.setBackground(B);
         validate();
    }

        public java.awt.Color getL() {
        return L;
    }

    public void setL(java.awt.Color L) {
        this.L = L;
         jLabel1.setBackground(L);
         validate();
    }
    
    
    
    String labelText = "", rb1Text, rb2Text, buttonText;
    String labelTag, rb1Tag, rb2Tag, butTag;

	ActionListener listener = new ActionListener()
    {
        @Override
        public void actionPerformed(ActionEvent ae) {

        }
    };

    private char KeyToPress;

    public char getKeyToPress() {
        return KeyToPress;
    }

    public void setKeyToPress(char KeyToPress) {
        this.KeyToPress = KeyToPress;
    }


    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */

    private void initComponents()
    {

        jLabel1 = new javax.swing.JLabel();
        jButton1 = new javax.swing.JButton();
        jRadioButton1 = new javax.swing.JRadioButton(); 
        
        
        jRadioButton1.addActionListener(listener);
        jRadioButton1.setSelected(false);

        jRadioButton2 = new javax.swing.JRadioButton();
        
        jRadioButton2.addActionListener(listener);
        jRadioButton2.setSelected(false);




        
        jButton1.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                jButton1ActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(this);
        this.setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel1)
                .addComponent(jRadioButton1)
                .addComponent(jRadioButton2)
                .addGap(18, 18, 18)
                .addComponent(jButton1)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel1)
                    .addComponent(jButton1)
                    .addComponent(jRadioButton1)
                    .addComponent(jRadioButton2)
                    )
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        
        
       
    }

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt)//GEN-FIRST:event_jButton1ActionPerformed
    {//GEN-HEADEREND:event_jButton1ActionPerformed
        ShowMessge(jButton1.getText());
    }//GEN-LAST:event_jButton1ActionPerformed

    
private void ShowMessge(String invokername){
    STR = invokername + " invoked event. \n";
    if(jRadioButton1.isSelected()){
        STR+=jRadioButton1.getText()+" is ON \n";
    }
    else{
        STR+=jRadioButton1.getText()+" is OFF \n";
    }

    if(jRadioButton2.isSelected()){
        STR+=jRadioButton2.getText()+" is ON \n";
    }
    else{
        STR+=jRadioButton2.getText()+" is OFF \n";
    }
    JOptionPane.showMessageDialog(this, STR);
    this.setFocusable(true);
}

    private javax.swing.JButton jButton1;
    private javax.swing.JRadioButton jRadioButton1;
    private javax.swing.JRadioButton jRadioButton2;
    private javax.swing.JLabel jLabel1;


    private void initEnterKey() {
        this.setFocusable(true);

        this.addKeyListener(new KeyListener() {

            @Override
            public void keyTyped(KeyEvent e) {
                System.out.println("Clicked! "+e.getKeyChar());
                if (e.getKeyChar() == KeyToPress) {
                    ShowMessge("Key "+e.getKeyChar());
                }
            }

            @Override
            public void keyPressed(KeyEvent e) {
            }

            @Override
            public void keyReleased(KeyEvent e) {
            }
        });
    }

    
}
