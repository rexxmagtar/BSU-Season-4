
import java.util.UUID;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */


public class RegUser {
    private String name, surname, birthday, phone, city, address;
    private UUID id;
    public RegUser(UUID id,String  name,
            String surname,String birthday,String phone,String city,String address) {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.birthday = birthday;
        this.phone = phone;
        this.city = city;
        this.address = address;
    }
    
    
    public UUID getId(){
        return id;
    }
    
    public String getName(){
        return this.name;
    }
    
    public String getSurname(){
        return this.surname;
    }
    
    public String getBirthday(){
        return birthday;
    }
    
    public String getPhone(){
        return phone;
    }
    
    public String getCity(){
        return city;
    }
    
    public String getAddress(){
        return address;
    }
}
