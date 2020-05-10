/* пример: извлечение информации из запроса клиента : InfoFromRequest.java */
package test.com;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.Enumeration;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class InfoFromRequest extends HttpServlet { 
    public void doGet( HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException {
      performTask(req, resp);
    }
    public void doPost( HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException {
      performTask(req, resp);
    }
    public void performTask(HttpServletRequest req, HttpServletResponse resp) {
      try {
        String name, value;
        PrintWriter out = resp.getWriter();
        resp.setContentType("text/html; charset=CP1251");
        out.println("<HTML><HEAD>");
        out.println("<TITLE>InfoFromRequest</TITLE>");
        out.println("</HEAD><BODY><BR>");
        out.println("<TABLE BORDER=3><TR>");
        out.println("<TD>NAME</TD><TD>VALUE</TD>");
        out.println("</TR>");
        
        Enumeration names = req.getParameterNames();
        while (names.hasMoreElements()) {
            name = (String) names.nextElement();
            /* name = new String( name.getBytes("ISO-8859-1"), "CP1251"); */
            value = req.getParameterValues(name)[0];
            /* value = new String( value.getBytes("ISO-8859-1"), "CP1251"); */
            out.println("<TR>");
            out.println("<TD>" + name + "</TD>");
            out.println("<TD>" + value + "</TD>");
            out.println("</TR>");
        }
        out.println("</TABLE></BODY></HTML>");
        out.close();
      } catch (Throwable theException) {
        theException.printStackTrace();
      }
    }
}
