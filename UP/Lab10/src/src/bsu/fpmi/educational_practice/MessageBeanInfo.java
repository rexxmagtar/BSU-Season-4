/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package src.bsu.fpmi.educational_practice;
import java.beans.*;
import java.lang.reflect.*;
import java.awt.*;

/**
 *
 * @author Dmitry Rogozenko,
 * Famcs 2020
 */
public class MessageBeanInfo extends SimpleBeanInfo {
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
	prop("messageText", "The message text that appears in the bean body"),
	prop("HelloLabel", "The label for the Hello button"),
    };
    
    public PropertyDescriptor[] getPropertyDescriptors() { return props; }
    
    public int getDefaultPropertyIndex() { return 0; }
}
