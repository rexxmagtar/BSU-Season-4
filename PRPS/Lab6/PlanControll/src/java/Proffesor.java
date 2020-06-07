
public class Proffesor implements User{
    
    private String name, surname, id;
    
    public Proffesor(String name, String surname, String id){
        this.name = name;
        this.surname = surname;
        this.id = id;
    }
    
    public String getName(){
        return name;
    }
    
    @Override
    public String getId(){
        return id;
    }
    
    
    public String getSurname(){
        return surname;
    }
}