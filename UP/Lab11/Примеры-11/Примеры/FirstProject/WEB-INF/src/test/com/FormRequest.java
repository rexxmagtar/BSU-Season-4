/* пример: обработка запроса клиента: FormRequest.java */
package test.com;

import java.io.IOException;
import java.io.PrintWriter;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class FormRequest extends HttpServlet {
  public void doGet(HttpServletRequest req, HttpServletResponse resp)
      throws ServletException, IOException {
    performTask(req, resp);
  }
  public void performTask(HttpServletRequest req, HttpServletResponse resp) {
    try {
      String val[] = new String[3];
      int rest;
      for (int i = 0; i < val.length; i++)
          val[i] = req.getParameter("p" + i);
      int c = Integer.valueOf(val[1]).intValue();
      int p = Integer.valueOf(val[2]).intValue();
      rest = c - p;
      PrintWriter out = resp.getWriter();
      resp.setContentType("text/html; charset=Cp1251");
      out.println("<HTML><HEAD>");
      out.println("<TITLE>FormRequest</TITLE>");
      out.println("</HEAD><BODY><BR>");
      out.println("<TABLE BORDER=3><TR><TD>");
      out.println( "Name</TD><TD>Credit</TD><TD>Price</TD><TD> Rest ");
      out.println("</TD></TR><TR>");
      for (int i = 0; i < val.length; i++)
          out.println("<TD>" + val[i] + "</TD>");
      out.println("<TD>" + rest + "</TD></TR>");
      out.println("Adress:" + req.getParameter("Adress"));
      out.println("</TABLE></BODY></HTML>");
      out.close();
    } catch (Throwable e) {
      e.printStackTrace();
    }
  }
}

