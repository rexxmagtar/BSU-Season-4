/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package src.bsu.fpmi.educational_practice;

import java.awt.Color;
import javax.swing.JRadioButton;
import java.beans.PropertyEditorSupport;
import javax.swing.JButton;



public class RadioPropertyEditor extends PropertyEditorSupport {
        
        public static final String RB_BLUE = "RB_BLUE",
                                   RB_GREEN = "RB_GREEN",
                                   RB_WHITE = "RB_WHITE";
	@Override
	public String[] getTags() {
		return new String[] {RB_BLUE, RB_GREEN, RB_WHITE};
	}
                
        public static JRadioButton getColoredRadioButton(String tag){
            JRadioButton but = new JRadioButton();
            switch (tag){
                case RB_BLUE:
                    but.setForeground(Color.BLUE);
                    break;
                case RB_GREEN:
                    but.setForeground(Color.GREEN);
                    break;
                case RB_WHITE:
                    but.setForeground(Color.WHITE);
                    break;
            }
            
            return but;
        }
	
	@Override
	public void setAsText(String s) {
		setValue(getColoredRadioButton(s));
	}

	@Override 
	public String getAsText() {
		return ((JRadioButton)getValue()).getText();
	}

	@Override
	public String getJavaInitializationString() {
            Object o = getValue();
            if (o == getColoredRadioButton(RB_BLUE))
                return "bsu.fpmi.educational_practice.ButtonPropertyEditor.BLUE";
            else if (o == getColoredRadioButton(RB_GREEN))
                return "bsu.fpmi.educational_practice.ButtonPropertyEditor.GREEN";
            else if (o == getColoredRadioButton(RB_WHITE))
                return "bsu.fpmi.educational_practice.RadioPropertyEditor.WHITE";
            else 
                return null;
	}
}
