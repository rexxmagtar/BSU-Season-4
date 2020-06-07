/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package bsu.fpmi.educational_practice;

import java.awt.*;
import java.beans.PropertyEditorSupport;
import java.util.logging.Level;
import java.util.logging.Logger;


public class ButtonPropertyEditor extends PropertyEditorSupport {
        
        public static final String BLACK = "BLACK",
                                   YELLOW = "YELLOW",
                                   WHITE = "WHITE";
	
        @Override
	public String[] getTags() {
            System.out.println("getting tags");
		return new String[] {"BLACK", "YELLOW", "WHITE"};
	}
                
        
        
        public static Color getColoredRadioButton(String tag) throws Exception{
            switch (tag){
                case BLACK:
                    return(Color.BLACK);

                case YELLOW:
                    return(Color.YELLOW);

                case WHITE:
                    return(Color.WHITE);

            }
            
            throw new Exception("Unknown color");
        }
	
	
@Override
	public void setAsText(String s) {
    
            try {
                setValue(new java.awt.Color( getColoredRadioButton(s).getRGB()));
            } catch (Exception ex) {
                Logger.getLogger(RadioPropertyEditor.class.getName()).log(Level.SEVERE, null, ex);
            }
                System.out.println("Setting text");
	}


        
@Override
	public String getJavaInitializationString(){
            System.out.println("getting java initialisation");
            
            Color o = (Color)getValue();
            try {
                if (o.getRGB() == getColoredRadioButton(BLACK).getRGB())
                    return "java.awt.Color.BLACK";
                else if (o.getRGB() == getColoredRadioButton(YELLOW).getRGB())
                    return "java.awt.Color.YELLOW";
                else if (o.getRGB() == getColoredRadioButton(WHITE).getRGB())
                    return "java.awt.Color.WHITE";
                else
                    return "java.awt.Color.BLACK";
            } catch (Exception ex) {
               return "java.awt.Color.YELLOW";
            }
            
            
	}
}
