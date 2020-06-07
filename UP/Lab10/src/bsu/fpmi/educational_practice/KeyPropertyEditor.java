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


public class KeyPropertyEditor extends PropertyEditorSupport {
        
        public static final String a = "a",
                                   b = "b",
                                   c = "c";
	
        @Override
	public String[] getTags() {
            System.out.println("getting tags");
		return new String[] {"a", "b", "c"};
	}
                
        
	
	
@Override
	public void setAsText(String s) {
    
            try {
                setValue(s.charAt(0));
            } catch (Exception ex) {
                setValue('f');
            }
                System.out.println("Setting text");
	}


        
@Override
	public String getJavaInitializationString(){
            System.out.println("getting java initialisation");
            
            char o = (char)getValue();
          String answer = "";
          answer+=o;
          
            return "\'"+answer+"\'";
            
	}
        
}
