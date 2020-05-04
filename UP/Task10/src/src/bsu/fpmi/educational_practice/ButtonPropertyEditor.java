/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package bsu.fpmi.educational_practice;

import java.awt.Color;
import java.beans.PropertyEditorSupport;
import javax.swing.JButton;


/**
 *
 * @author Dmitry Rogozenko,
 * Famcs 2020
 */
public class ButtonPropertyEditor extends PropertyEditorSupport{ 
    	
        public static final String BUT_BLUE = "BUT_BLUE",
                                   BUT_GREEN = "BUT_GREEN",
                                   BUT_WHITE = "BUT_WHITE";
	@Override
	public String[] getTags() {
		return new String[] {BUT_BLUE, BUT_GREEN, BUT_WHITE};
        }
        
        public static JButton getColoredButton(String tag){
            JButton but = new JButton();
            switch (tag){
                case BUT_BLUE:
                    but.setBackground(Color.BLUE);
                    break;
                case BUT_GREEN:
                    but.setBackground(Color.GREEN);
                    break;
                case BUT_WHITE:
                    but.setBackground(Color.WHITE);
                    break;
            }
            
            return but;
        }
	
	@Override
	public void setAsText(String s) {
		setValue(new JButton(s));
	}

	@Override 
	public String getAsText() {
		return ((JButton)getValue()).getText();
	}

	@Override
	public String getJavaInitializationString() {
            Object o = getValue();
            if (o == getColoredButton(BUT_BLUE))
                return "bsu.fpmi.educational_practice.ButtonPropertyEditor.BLUE";
            else if (o == getColoredButton(BUT_GREEN))
                return "bsu.fpmi.educational_practice.ButtonPropertyEditor.GREEN";
            else if (o == getColoredButton(BUT_WHITE))
                return "bsu.fpmi.educational_practice.ButtonPropertyEditor.WHITE";
            else 
                return null;
	}	
}
