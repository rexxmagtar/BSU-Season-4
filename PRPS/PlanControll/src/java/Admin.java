
public class Admin implements User{
    
    private String  id;
    
    public Admin( String id){

        this.id = id;
    }
    
    
    @Override
    public String getId(){
        return id;
    }
    
}