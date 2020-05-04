/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package bsu.fpmi.educational_practice;

import java.awt.Color;
import java.beans.PropertyEditorSupport;
import javax.swing.JLabel;


/**
 *
 * @author Ivan Kalinchuk,
 * Famcs 2020
 */
public class LabelPropertyEditor extends PropertyEditorSupport {
        
        public static final String L_GREEN = "labGreen", 
                                    L_BLACK = "labBlack", 
                                    L_WHITE = "labWhite";
        
        public static JLabel getColorLabel(String tag) {
            JLabel label = new JLabel();
            switch(tag) {
                    case L_GREEN:
                        label.setForeground(Color.GREEN);
                        break;
                    case L_BLACK:
                        label.setForeground(Color.BLACK);
                        break;
                    case L_WHITE:
                        label.setForeground(Color.WHITE);
                        break;
            }
            
            return label;
        }
    
	@Override
	public String[] getTags() {
		return new String[] {L_GREEN, L_BLACK, L_WHITE};
	}
	@Override
	public void setAsText(String s) {
            setValue(getColorLabel(s));
	}

	@Override 
	public String getAsText() {
		return ((JLabel)getValue()).getText();
	}

	@Override
	public String getJavaInitializationString() {
            Object o = getValue();
            if (o == getColorLabel(L_GREEN))
                return "bsu.fpmi.educational_practice.LabelPropertyEditor.GREEN";
            else if (o == getColorLabel(L_BLACK))
                return "bsu.fpmi.educational_practice.LabelPropertyEditor.BLACK";
            else if (o == getColorLabel(L_WHITE))
                return "bsu.fpmi.educational_practice.LabelPropertyEditor.WHITE";
            else 
                return null;	
        }
}
