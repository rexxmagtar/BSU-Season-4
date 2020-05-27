/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package bsu.fpmi.educational_practice;
import java.beans.*;
import java.lang.reflect.*;
import java.awt.*;

public class BeanInfo extends SimpleBeanInfo {
   static PropertyDescriptor prop(String name, String description) {
	try {
	    PropertyDescriptor p =
		new PropertyDescriptor(name, Message.class);
	    p.setShortDescription(description);
	    return p;
	}
	catch(IntrospectionException e) { return null; } 
    }
    
    static PropertyDescriptor[] props = {
	prop("jlabel1", "The message text that appears in the bean body"),
	prop("jradioButton1", "test radioButton1"),
			prop("jradioButton2", "test radioButton2"),
			prop("jButton1", "test normal button"),
    };
    
    public PropertyDescriptor[] getPropertyDescriptors() { return props; }
    
    public int getDefaultPropertyIndex() { return 0; }
}
