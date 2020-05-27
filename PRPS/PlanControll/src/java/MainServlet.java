

import java.io.IOException;
import java.io.PrintWriter;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.servlet.http.HttpSession;
import javax.swing.JOptionPane;


public class MainServlet extends HttpServlet {
    
    
    
    enum ServerState{
        login,
        menu,
        showPlans,
        addPlan,
        completePlan
        
    }
    
    public ServerState currentState =ServerState.login;
    
    public  String currentLogin = "";
    
    private Connection connectionToDb = null;
    private static final String COLUMN_ID = "id", COLUMN_LOGIN = "login",
                                COLUMN_PASSWORD = "password",
                                COLUMN_NAME = "name",
                                COLUMN_SURNAME = "surname";
    
    private static final String HTML_KEY_NAME = "#htmlName#",
                                HTML_KEY_SURNAME = "#htmlSurname#",
                                HTML_KEY_ID = "#htmlId#";
    
    private static final String PATH_TO_TEACHER_PAGE = "C:/BSU-Season-4/PRPS/PlanControll/web/proffesorpage.html";
    private static final String PATH_TO_ADMIN_PAGE = "C:/BSU-Season-4/PRPS/PlanControll/web/adminpage.html";
    private static final String PATH_TO_LOGIN_PAGE = "C:/BSU-Season-4/PRPS/PlanControll/web/index.html";
    
    private Connection connectToDb() throws Exception{
        Class.forName("org.sqlite.JDBC");
        String url = "jdbc:sqlite:C:/BSU-Season-4/PRPS/PlanControll/src/java/db/UnivercityPlanControll.db";
        // create a connection to the database
        Connection conn = DriverManager.getConnection(url);
            
        System.out.println("Connection to SQLite has been established.");
        
        return conn;
    }
    
    private void printErrorPage(PrintWriter out, String message){
           File file = new File("C:/BSU-Season-4/PRPS/PlanControll/web/errorPage.html");
        
           try{
           Scanner scnr = new Scanner(file);
     
        //Reading each line of file using Scanner class
        while(scnr.hasNextLine()){
            String line = scnr.nextLine();
            
           line = line.replaceAll("ERRORMSG", message);
            out.println(line);
        }
           }
           catch(Exception ex){
               
           }
    }
    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
     * methods.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException, SQLException {
        response.setContentType("text/html;charset=UTF-8");

        connectionToDb = null;
        try {
            connectionToDb = connectToDb();
        }
        catch(Exception ex){
            try(PrintWriter out = response.getWriter()){
                printErrorPage(out, ex.toString());
            }
        }
        if(request.getParameter("login")!=null){
            
            currentState = ServerState.login;
        }
        
        if(request.getParameter("button")!=null){
            
            currentState = ServerState.menu;
        }
        
        if(request.getParameter("Start")!=null){
            
            currentState = ServerState.addPlan;
        }
        
        HttpSession session = request.getSession(true);
        
        currentLogin = (String)session.getAttribute("login");
        
        
        switch(currentState){
            
            case login:{
                
               if( ProcessLogin(request, response)){
                currentState = ServerState.menu;
               }
             
                break;
            }
            case menu:{
                ProcessMenu(request,response);
                break;
            }
            case showPlans:{
                
            }
            case addPlan:{
                ProcessAddPlan(request,response);
                break;
            }
            case completePlan :{
               ProcessCompletePlan(request,response);
               break;
            }
        }
        connectionToDb.close();
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
        try {
            processRequest(request, response);
        } catch (SQLException ex) {
            Logger.getLogger(MainServlet.class.getName()).log(Level.SEVERE, null, ex);
        }
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
        try {
            processRequest(request, response);
        } catch (SQLException ex) {
            Logger.getLogger(MainServlet.class.getName()).log(Level.SEVERE, null, ex);
        }
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

    
    private boolean  ProcessLogin(HttpServletRequest request, HttpServletResponse response) throws IOException{
        
        String login = request.getParameter("login");
        currentLogin = login;
        String password = request.getParameter("password");
        PrintWriter out = response.getWriter();
        //if successfully connected
        if (loggedAdmin(login, password)){
            try{
            loadAdminPage(out, login, password);
            }
            catch(Exception ex){
                printErrorPage(out, ex.toString());
            }
        }
        else if (loggedProffesor(login, password)){
            try{
                
                loadProffesorPage(out, login, password);
            }
            catch(Exception ex){
                printErrorPage(out, "Server Error: there is no page for proffesors");
               return false;
            }        
        }
        else{
            printErrorPage(out, "Error: you are neither proffesor nor admin");
            return false;
        }
        
        // get the session, add argument `true` to create a session if one is not yet created.
HttpSession session = request.getSession(true);

session.setAttribute("login", login);
        
        System.out.println("Did load user");
        return true;
    }
    
    private void ProcessCompletePlan(HttpServletRequest request, HttpServletResponse response) throws IOException{
    
        String updateRequest = "UPDATE Report SET Completed = \"YES\" WHERE ReportID = "+ request.getParameter("taskID") +" ";
        
        try{
        this.connectionToDb.createStatement().execute(updateRequest);
        }
        catch(Exception ex){
            
            printErrorPage(response.getWriter(),ex.toString());
            return;
        }
        
        loadProffesorPage(response.getWriter());
 currentState = ServerState.menu;
    }
    
    
    private void  ProcessMenu(HttpServletRequest request, HttpServletResponse response) throws IOException{
    
        PrintWriter out = response.getWriter();
        String option = request.getParameter("button");
        
        switch(option){
            case "Show all plans":{
                LoadProffInfo(out,currentLogin);
                break;
            }
            case "Add new plan":{
                LoadCreateReportForm(out);
                currentState = ServerState.addPlan;
                break;
            }
            case "Complete plan":{
                LoadCompletePlan(out);
                currentState = ServerState.completePlan;
                break;
            }
            case "Checksum":{
                LoadCheckSum( out, currentLogin);
                break;
            }
            case "Controll checksums":{
                LoadControllCheckSums(out);
                break;
            }
            case "Proffesors info":{
               LoadProfessorsInfo(out); 
               break;
            }
            case "Exit":{
                response.sendRedirect("http://localhost:8080/PlanControll/");
                currentState = ServerState.login;
            }
        }
    }
    
    private void LoadProfessorsInfo(PrintWriter out){
        
        String allProffsInfoRequest = "SELECT proffesor.name,surname,grade,Department.Name,addres,phone FROM proffesor INNER JOIN Department ON Department.Id = departmentId";
        
        try{
            
            ResultSet resultDone = this.connectionToDb.createStatement().executeQuery(allProffsInfoRequest);
            
            
            
            out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<head>\n" +
"<style>\n" +
                    "body {\n" +
"                 margin: 0;\n" +
"                background-repeat: no-repeat;\n" +
"                background-attachment: fixed;\n" +
"              background-size: 100% auto;\n" +
"                background-image: url('backWhite.jpg');\n" +
"            }\n" +
"            div{\n" +
"                margin: 20px;\n" +
"            }"+
"table {\n" +
"  font-family: arial, sans-serif;\n" +
"  border-collapse: collapse;\n" +
"  width: 100%;\n" +
"}\n" +
"\n" +
"td, th {\n" +
"  border: 1px solid #dddddd;\n" +
"  text-align: left;\n" +
"  padding: 8px;\n" +
"}\n" +
"\n" +
".trGreen {\n" +
"  background-color: #03fc7f;\n" +
"}\n" +
                    ".trRed {\n" +
"  background-color: #fc0341;\n" +
"}\n" +
"</style>\n" +
"</head>");
            out.println("<body>");
            out.println("<table>\n" +
                    "<caption>Professors</caption>"+
"  <tr>\n" +
"<th> Name </th>"+
"<th> Surname </th>"+
"<th> Grade </th>"+
"<th> Department </th>"+
"<th> Address </th>"+    
                    "<th> Phone number </th>"+ 
"  </tr>\n");

        
            try{
            // loop through the result set
            while (resultDone.next()) {
               
                

out.println("  <tr  >");
                

                
out.println("    <td>" + resultDone.getString(1) + "</td>\n" +
        "    <td>" + resultDone.getString(2) + "</td>\n" +
"    <td>" + resultDone.getString(3) + "</td>\n" +
        "    <td>" + resultDone.getString(4) + "</td>\n" +
                "    <td>" + resultDone.getString(5) + "</td>\n" +
                         "    <td>" + resultDone.getString(6) + "</td>\n" +
"  </tr>\n");

                
            }
            }
            catch(Exception ex){
                printErrorPage(out,ex.toString());
            }
            
            out.println("</table>");
            out.println("</body>");
            out.println("</html>");
            
            
        }
        catch(Exception ex){
            printErrorPage(out,ex.toString());
        }
    }
    
    private void LoadControllCheckSums(PrintWriter out){
        
        String countCheckSumRequest ="SELECT A*100.0/B AS dif,proffesor.name,surname,grade,Department.Name AS DepartmentName FROM ( (SELECT  SUM(Hours) AS A,ProfID AS ProfIDA,* FROM Report WHERE Completed = \"YES\" GROUP BY ProfIDA) INNER JOIN (SELECT SUM(Hours) AS B, ProfID AS ProfIDB FROM Report  GROUP BY ProfIDB) ON ProfIDA=ProfIDB INNER JOIN proffesor ON ProfIDB=proffesor.id INNER JOIN Department ON departmentId = Department.Id) ORDER BY dif DESC";
        
        ResultSet resultDone;
        
        
        
        try{
        resultDone = this.connectionToDb.createStatement().executeQuery(countCheckSumRequest);
        
       
        }
        catch(Exception ex){
            printErrorPage(out,ex.toString());
            return;
        }
        
         out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<head>\n" +
"<style>\n" +
                    "body {\n" +
"                 margin: 0;\n" +
"                background-repeat: no-repeat;\n" +
"                background-attachment: fixed;\n" +
"              background-size: 100% auto;\n" +
"                background-image: url('backWhite.jpg');\n" +
"            }\n" +
"            div{\n" +
"                margin: 20px;\n" +
"            }"+
"table {\n" +
"  font-family: arial, sans-serif;\n" +
"  border-collapse: collapse;\n" +
"  width: 100%;\n" +
"}\n" +
"\n" +
"td, th {\n" +
"  border: 1px solid #dddddd;\n" +
"  text-align: left;\n" +
"  padding: 8px;\n" +
"}\n" +
"\n" +
".trGreen {\n" +
"  background-color: #03fc7f;\n" +
"}\n" +
                    ".trRed {\n" +
"  background-color: #fc0341;\n" +
"}\n" +
"</style>\n" +
"</head>");
            out.println("<body>");
            out.println("<table>\n" +
                    "<caption> Plans statistic</caption>"+
"  <tr>\n" +
"<th> Percent complete </th>"+
"<th> Name </th>"+
"<th> Surname </th>"+
"<th> Grade </th>"+
"<th> Department </th>"+                    
"  </tr>\n");

        
            try{
            // loop through the result set
            while (resultDone.next()) {
               
                
                if(Float.parseFloat( resultDone.getString(1))>80){
out.println("  <tr class = \"trGreen\" >");
                }
                else{
                    if(Float.parseFloat( resultDone.getString(1))<40){
                    out.println("  <tr  class = \"trRed\" >");
                    }
                    else{
                        out.println("  <tr  >");
                    }
                }
                
out.println("    <td>" + resultDone.getString(1) + "</td>\n" +
        "    <td>" + resultDone.getString(2) + "</td>\n" +
"    <td>" + resultDone.getString(3) + "</td>\n" +
        "    <td>" + resultDone.getString(4) + "</td>\n" +
                "    <td>" + resultDone.getString(5) + "</td>\n" +
"  </tr>\n");

                
            }
            }
            catch(Exception ex){
                
            }
            
            out.println("</table>");
            out.println("</body>");
            out.println("</html>");
        
    }
    
    private void LoadCheckSum(PrintWriter out,String login){
        
       
        String countCheckSumDoneRequest ="SELECT SUM(Hours) FROM Report WHERE Completed = \"YES\" AND ProfID = (SELECT id FROM proffesor WHERE login = "+"\""+login+"\"" +" ) ";
        String countCheckSumTotalRequest ="SELECT SUM(Hours) FROM Report WHERE ProfID = (SELECT id FROM proffesor WHERE login = "+"\""+login+"\"" +" ) ";
        
        String resultDone ="";
         String resultTotal="";
        try{
        resultDone = this.connectionToDb.createStatement().executeQuery(countCheckSumDoneRequest).getString(1);
        resultTotal = this.connectionToDb.createStatement().executeQuery(countCheckSumTotalRequest).getString(1);
        }
        catch(Exception ex){
            printErrorPage(out,ex.toString());
            return;
        }
        
        out.println("<html>\n" +
"    <head>\n" +
"        <title>TODO supply a title</title>\n" +
"        <meta charset=\"UTF-8\">\n" +
"        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
"                   <style>\n" +
           "body {\n" +
"                 margin: 0;\n" +
"                background-repeat: no-repeat;\n" +
"                background-attachment: fixed;\n" +
"              background-size: 100% auto;\n" +
"                background-image: url('backWhite.jpg');\n" +
"            }\n" +
"            div{\n" +
"                margin: 20px;\n" +
"            }"+
"  </style>\n" +
"    </head>\n" +
          "<h3 align='center'> hours done: "+ resultDone +"</h3>"   +
                  "<h3 align='center'> hours total: "+ resultTotal +"</h3>"   +
"    <body>\n" +
"\n" +
"    </body>\n" +
"</html>");
        
    }
    
    private void LoadProffInfo(PrintWriter out, String login){
        
            out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<head>\n" +
"<style>\n" +
                    "body {\n" +
"                 margin: 0;\n" +
"                background-repeat: no-repeat;\n" +
"                background-attachment: fixed;\n" +
"              background-size: 100% auto;\n" +
"                background-image: url('backWhite.jpg');\n" +
"            }\n" +
"            div{\n" +
"                margin: 20px;\n" +
"            }"+
"table {\n" +
"  font-family: arial, sans-serif;\n" +
"  border-collapse: collapse;\n" +
"  width: 100%;\n" +
"}\n" +
"\n" +
"td, th {\n" +
"  border: 1px solid #dddddd;\n" +
"  text-align: left;\n" +
"  padding: 8px;\n" +
"}\n" +
"\n" +
".trGreen {\n" +
"  background-color: #03fc7f;\n" +
"}\n" +
"</style>\n" +
"</head>");
            out.println("<body>");;
            out.println("<table>\n" +
                    "<caption> Plans</caption>"+
"  <tr>\n" +
"    <th>Start date</th>\n" +
"    <th>End date</th>\n" +
"    <th>Hours</th>\n" +
"    <th>Description</th>\n" +
                    "    <th>Current status</th>\n" +
"  </tr>\n");
            String strToDisplay = "";
            
            String infoRequest = "SELECT * FROM Report WHERE ProfID = (SELECT id FROM proffesor WHERE login = "+"\""+login+"\"" +" )";
        try (Statement stmt  = this.connectionToDb.createStatement();
             ResultSet rs    = stmt.executeQuery(infoRequest)){
            // loop through the result set
            while (rs.next()) {
               
                if(rs.getString("Completed").equals("YES")){
out.println("  <tr class = \"trGreen\" >\n" +
"    <td>" + rs.getString(2) + "</td>\n" +
"    <td>" + rs.getString(3) + "</td>\n" +
"    <td>" + rs.getString(4) + "</td>\n" +
"    <td>" + rs.getString(5) + "</td>\n" +
        "    <td>" + "Done" + "</td>\n" +
"  </tr>\n");
                }
                else{
                    out.println("  <tr  >\n" +
"    <td>" + rs.getString(2) + "</td>\n" +
"    <td>" + rs.getString(3) + "</td>\n" +
"    <td>" + rs.getString(4) + "</td>\n" +
"    <td>" + rs.getString(5) + "</td>\n" +
        "    <td>" + "Not done" + "</td>\n" +
"  </tr>\n");
                }
            }
        }
        catch(Exception ex){
          
        }
            
            out.println("</table>");
            out.println("</body>");
            out.println("</html>");
        
    }
    
    
    
    
    private void LoadCompletePlan(PrintWriter out){
           out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<head>\n" +
"<style>\n" +
                   ".login h1 {\n" +
"  background-image: linear-gradient(top, #f1f3f3, #d4dae0);\n" +
"  border-bottom: 1px solid #a6abaf;\n" +
"  border-radius: 6px 6px 0 0;\n" +
"  box-sizing: border-box;\n" +
"  color: #727678;\n" +
"  display: block;\n" +
"  height: 43px;\n" +
"  font: 600 14px/1 'Open Sans', sans-serif;\n" +
"  padding-top: 14px;\n" +
"  margin: 0;\n" +
"  text-align: center;\n" +
"  text-shadow: 0 -1px 0 rgba(0,0,0,0.2), 0 1px 0 #fff;\n" +
"}\n" +
"\n" +
"input[type=\"submit\"]:hover {\n" +
"    background-color: #bcc2be;\n" +
"}\n" +
"input[type=\"password\"], input[type=\"text\"], input[type=\"number\"] {\n" +
"  background: url('http://i.minus.com/ibhqW9Buanohx2.png') center left no-repeat, linear-gradient(top, #d6d7d7, #dee0e0);\n" +
"  border: 1px solid #a1a3a3;\n" +
"  border-radius: 4px;\n" +
"  box-shadow: 0 1px #fff;\n" +
"  box-sizing: border-box;\n" +
"  color: #696969;\n" +
"  height: 39px;\n" +
"  margin: 31px 0 0 29px;\n" +
"  padding-left: 37px;\n" +
"  transition: box-shadow 0.3s;\n" +
"  width: 240px;\n" +
"}\n" +
"input[type=\"password\"]:focus, input[type=\"text\"]:focus, input[type=\"number\"]:focus {\n" +
"  box-shadow: 0 0 4px 1px rgba(55, 166, 155, 0.3);\n" +
"  outline: 0;\n" +
"}\n" +
".show-password {\n" +
"  display: block;\n" +
"  height: 16px;\n" +
"  margin: 26px 0 0 28px;\n" +
"  width: 87px;\n" +
"}\n" +
"input[type=\"checkbox\"] {\n" +
"  cursor: pointer;\n" +
"  height: 16px;\n" +
"  opacity: 0;\n" +
"  position: relative;\n" +
"  width: 64px;\n" +
"}\n" +
"input[type=\"checkbox\"]:checked {\n" +
"  left: 29px;\n" +
"  width: 58px;\n" +
"}\n" +
".toggle {\n" +
"  background: url(http://i.minus.com/ibitS19pe8PVX6.png) no-repeat;\n" +
"  display: block;\n" +
"  height: 16px;\n" +
"  margin-top: -20px;\n" +
"  width: 87px;\n" +
"  z-index: -1;\n" +
"}\n" +
"input[type=\"checkbox\"]:checked + .toggle { background-position: 0 -16px }\n" +
".forgot {\n" +
"  color: #7f7f7f;\n" +
"  display: inline-block;\n" +
"  float: right;\n" +
"  font: 12px/1 sans-serif;\n" +
"  left: -19px;\n" +
"  position: relative;\n" +
"  text-decoration: none;\n" +
"  top: 5px;\n" +
"  transition: color .4s;\n" +
"}\n" +
".forgot:hover { color: #3b3b3b }\n" +
"input[type=\"submit\"] {\n" +
"  width:240px;\n" +
"  height:35px;\n" +
"  display:block;\n" +
"  font-family:Arial, \"Helvetica\", sans-serif;\n" +
"  font-size:16px;\n" +
"  font-weight:bold;\n" +
"  color:#fff;\n" +
"  text-decoration:none;\n" +
"  text-transform:uppercase;\n" +
"  text-align:center;\n" +
"  text-shadow:1px 1px 0px #37a69b;\n" +
"  padding-top:6px;\n" +
"  margin: 29px 0 0 29px;\n" +
"  position:relative;\n" +
"  cursor:pointer;\n" +
"  border: none;  \n" +
"  background-color: #37a69b;\n" +
"  background-image: linear-gradient(top,#3db0a6,#3111);\n" +
"  border-top-left-radius: 5px;\n" +
"  border-top-right-radius: 5px;\n" +
"  border-bottom-right-radius: 5px;\n" +
"  border-bottom-left-radius:5px;\n" +
"  box-shadow: inset 0px 1px 0px #2ab7ec, 0px 5px 0px 0px #497a78, 0px 10px 5px #999;\n" +
"}\n" +
"\n" +
".shadow {\n" +
"  background: #000;\n" +
"  border-radius: 12px 12px 4px 4px;\n" +
"  box-shadow: 0 0 20px 10px #000;\n" +
"  height: 12px;\n" +
"  margin: 30px auto;\n" +
"  opacity: 0.2;\n" +
"  width: 270px;\n" +
"}\n" +
"\n" +
"\n" +
"input[type=\"submit\"]:active {\n" +
"  top:3px;\n" +
"  box-shadow: inset 0px 1px 0px #2ab7ec, 0px 2px 0px 0px #31524d, 0px 5px 3px #999;\n" +
"}" +
                    "body {\n" +
"                 margin: 0;\n" +
"                background-repeat: no-repeat;\n" +
"                background-attachment: fixed;\n" +
"              background-size: 100% auto;\n" +
"                background-image: url('backWhite.jpg');\n" +
"            }\n" +
"            div{\n" +
"                margin: 20px;\n" +
"            }"+
"table {\n" +
"  font-family: arial, sans-serif;\n" +
"  border-collapse: collapse;\n" +
"  width: 100%;\n" +
"}\n" +
"\n" +
"td, th {\n" +
"  border: 1px solid #dddddd;\n" +
"  text-align: left;\n" +
"  padding: 8px;\n" +
"}\n" +
"\n" +
"tr:nth-child(even) {\n" +
"  background-color: #dddddd;\n" +
"}\n" +
"</style>\n" +
"</head>");
            out.println("<body>");
            out.println("<table>\n" +
                    
                    "<caption> Plans info</caption>"+
"  <tr>\n" +
                    "    <th>Task id</th>\n" +
"    <th>Start date</th>\n" +
"    <th>End date</th>\n" +
"    <th>Hours</th>\n" +
"    <th>Description</th>\n" +
"  </tr>\n");
            
            String infoRequest = "SELECT * FROM Report WHERE ProfID = (SELECT id FROM proffesor WHERE login = "+"\""+currentLogin+"\"" +" ) AND Completed = \"NO\" ";
        try (Statement stmt  = this.connectionToDb.createStatement();
             ResultSet rs    = stmt.executeQuery(infoRequest)){
            // loop through the result set
            while (rs.next()) {
               
out.println("  <tr>\n" +
"    <td>" + rs.getString(1) + "</td>\n" +
"    <td>" + rs.getString(2) + "</td>\n" +
"    <td>" + rs.getString(3) + "</td>\n" +
"    <td>" + rs.getString(4) + "</td>\n" +
"    <td>" + rs.getString(5) + "</td>\n" +
"  </tr>\n");
            }
        }
        catch(Exception ex){
          
        }
            
            out.println("</table>");
            
            out.println("<form action=\"LoginServlet\">\n" +
"\n" +
"         <div align='center'><input type=\"number\" placeholder = \"task id\" min = \"0\" name=\"taskID\" value=\"\" required></div>\n" +
"        <div align='center'><input style=\"width:250px\" align='center' type=\"submit\" value=\"Enter\"></div>\n" +
"       </form>");
            
            out.println("</body>");
            out.println("</html>");
    }
    
    private void LoadCreateReportForm(PrintWriter out) throws FileNotFoundException{
        File file = new File("C:/BSU-Season-4/PRPS/PlanControll/web/CreteNewReportPage.html");
        
        Scanner scnr = new Scanner(file);
     
        //Reading each line of file using Scanner class
        while(scnr.hasNextLine()){
            out.println(scnr.nextLine());
        }
        
    }
    
    private void ProcessAddPlan(HttpServletRequest request, HttpServletResponse response) throws IOException, SQLException{
    //Implement
    PrintWriter out = response.getWriter();
    
    System.out.println("Trying to update login "+currentLogin);
    
 String addPlanRequest = "INSERT INTO Report ( StartDate, EndDate,Hours,Description, ProfID) values (\""+request.getParameter("Start")+"\", \""+request.getParameter("End")+"\",\""+request.getParameter("H")+"\",\""+request.getParameter("Description")+"\" , ((SELECT id FROM proffesor WHERE login ="+"\""+currentLogin+"\"" +" )))";
 
 try{
 this.connectionToDb.createStatement().execute(addPlanRequest);
 }
 catch(Exception ex){
     printErrorPage(out,ex.toString() +"\n\n "+addPlanRequest);
     return ;
 }
 
 loadProffesorPage(out);
 currentState = ServerState.menu;
    
    }
    
    private boolean loggedAdmin(String login, String password) {
        String adminRequest = "SELECT login, password FROM admin";
        try (Statement stmt  = this.connectionToDb.createStatement();
             ResultSet rs    = stmt.executeQuery(adminRequest)){
            // loop through the result set
            while (rs.next()) {
                if (rs.getString("login").equals(login) &&
                    rs.getString("password").equals(password)) {
                        return true;
                }

            }
        }
        catch(Exception ex){
            return false;
        }
        
        return false;
    }

    
    private User getUserWith(String login, String password) throws Exception{
        //try to find in proffesors
        String proffesorRequest = "SELECT id, name, surname, login, password FROM proffesor";
        try (Statement stmt  = this.connectionToDb.createStatement();
             ResultSet rs    = stmt.executeQuery(proffesorRequest)){
            // loop through the result set
            while (rs.next()) {
                if (rs.getString(COLUMN_LOGIN).equals(login) &&
                    rs.getString(COLUMN_PASSWORD).equals(password)) {
                        return new Proffesor(rs.getString(COLUMN_NAME), 
                                      rs.getString(COLUMN_SURNAME), 
                                      rs.getString(COLUMN_ID)); 
                }
            }
        }
        catch(Exception ex){
            //idk
        }
        String adminRequest = "SELECT id,login,password FROM admin";
        try (Statement stmt  = this.connectionToDb.createStatement();
             ResultSet rs    = stmt.executeQuery(adminRequest)){
            // loop through the result set
            while (rs.next()) {
                if (rs.getString(COLUMN_LOGIN).equals(login) &&
                    rs.getString(COLUMN_PASSWORD).equals(password)) {
                        return new Admin( rs.getString(COLUMN_ID)); 
                }

            }
        }
        throw new Exception("Error: there is no user with such login and password;");
    }
    
    private void loadAdminPage(PrintWriter out, String login, String password) throws Exception {
        Admin admin = (Admin)getUserWith(login, password);
        File file = new File(PATH_TO_ADMIN_PAGE);
        Scanner scnr = new Scanner(file);
     
        //Reading each line of file using Scanner class
        while(scnr.hasNextLine()){
            String line = scnr.nextLine();
            out.println(line);
        }
    }
    
    private void loadProffesorPage(PrintWriter out) throws FileNotFoundException{
        File file = new File(PATH_TO_TEACHER_PAGE);
        Scanner scnr = new Scanner(file);
     
        //Reading each line of file using Scanner class
        while(scnr.hasNextLine()){
            String line = scnr.nextLine();
            out.println(line);
        }
    }

    
    private void loadProffesorPage(PrintWriter out, String login, String password) throws Exception{
        Proffesor proffesor = (Proffesor)getUserWith(login, password);
        loadProffesorPage(out);
    }

    private boolean loggedProffesor(String login, String password) {
        String proffesorRequest = "SELECT login, password FROM proffesor";
        try (Statement stmt  = this.connectionToDb.createStatement();
             ResultSet rs    = stmt.executeQuery(proffesorRequest)){
            // loop through the result set
            while (rs.next()) {
                if (rs.getString(COLUMN_LOGIN).equals(login) &&
                    rs.getString(COLUMN_PASSWORD).equals(password)) {
                        return true;
                }
            }
        }
        catch(Exception ex){
            return false;
        }
       
        return false;
    }

}
