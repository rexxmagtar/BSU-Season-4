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


public class ButtonTextPropertyEditor extends PropertyEditorSupport {
        
        public static final String V1 = "V1",
                                   V2 = "V2",
                                   V3 = "V3";
	
        @Override
	public String[] getTags() {
            System.out.println("getting tags");
		return new String[] {"V1", "V2", "V3"};
	}
                
        
	
	
@Override
	public void setAsText(String s) {
    
            try {
                setValue(s);
            } catch (Exception ex) {
                Logger.getLogger(RadioPropertyEditor.class.getName()).log(Level.SEVERE, null, ex);
            }
                System.out.println("Setting text");
	}


        
@Override
	public String getJavaInitializationString(){
            System.out.println("getting java initialisation");
            
            String o ="\""+ (String)getValue()+"\"";
          
            return o;
            
	}
}
