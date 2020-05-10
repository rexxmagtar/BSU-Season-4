/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

import java.io.IOException;
import java.io.PrintWriter;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.UUID;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;


public class RegistrationServlet extends HttpServlet {

    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
     * methods.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    private static ArrayList<RegUser> usersInBase = new ArrayList<>();
    private static final String[]  CAN_NOT_CREATE_USER = new String[]{
        "<HTML><HEAD>",
       "<TITLE>Can not create new user</TITLE>",
       "<h1>Can not create new user: user with the same name and surname already exists</h1>",
                   "</HEAD></HTML>"

    };
    private static final String[]  SUCCESSFULLY_CREATED_USER = new String[]{
        "<HTML><HEAD>",
       "<TITLE>Successfully created a user</TITLE>",
       "<h1>User successfully created</h1>",
       getCurrentTimeInTag("2"),
            "</HEAD></HTML>"
    };
    
    private static String getCurrentTimeInTag(String tagId){
           DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy/MM/dd HH:mm:ss");  
            LocalDateTime now = LocalDateTime.now();  
            String openTag = "<h" + tagId + ">";
            String closeTag = "</h" + tagId + ">";
            return openTag + "Created time: " + dtf.format(now) + closeTag;
    }
    
    private RegUser getUserFromParameters(HttpServletRequest request){
        UUID newId = UUID.randomUUID();
        RegUser userFromParameters = 
                new RegUser(newId,
                            request.getParameter("arg_name"),
                            request.getParameter("arg_surname"),
                            request.getParameter("arg_birthday"),
                            request.getParameter("arg_phone"),
                            request.getParameter("arg_city"),
                            request.getParameter("arg_address")
            );
        return userFromParameters;
    }
    
    private boolean alreadyRegisteredUser(RegUser user){
        for(RegUser registeredUser : usersInBase){
            if(registeredUser.getName().equals(user.getName())){
                return true;
            }
        }
        return false;
    }
    
    private void showPage(PrintWriter out, String[] htmlPage, RegUser user){
        for(int i = 0; i < htmlPage.length; i++){
            out.println(htmlPage[i]);
        }
    }
    
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        try {
          RegUser user = this.getUserFromParameters(request);
          PrintWriter out = response.getWriter();
          response.setContentType("text/html; charset=Cp1251");
          if (alreadyRegisteredUser(user)){
                showPage(out, CAN_NOT_CREATE_USER, user);
            }
          else{
              usersInBase.add(user);
              showPage(out, SUCCESSFULLY_CREATED_USER, user);
          }
          out.close();
        } catch (Throwable e) {
          e.printStackTrace();
        }
    }

    // <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /**
     * Handles the HTTP <code>GET</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Handles the HTTP <code>POST</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Returns a short description of the servlet.
     *
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>

}
